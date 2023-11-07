import json
import logging
import random

import azure.cosmos as cosmo
import azure.functions as func
import config

app = func.FunctionApp()


@app.service_bus_queue_trigger(arg_name="message", queue_name="copied_items",
                               connection="copied_items_bus_conn_str")
def copyextension_queue_trigger(message: func.ServiceBusMessage):
    PACKET_STR = message.get_body().decode('utf-8')
    data = dict(json.loads(PACKET_STR))

    # Add data to azure cosmos database
    URL = config.SETTINGS['DATABASE_URL']
    KEY = config.SETTINGS['DATABASE_KEY']
    DATABASE_NAME = config.SETTINGS['DATABASE_NAME']
    DB_CONTAINER_NAME = config.SETTINGS['DB_CONTAINER_NAME']

    # Register database & container
    # Assumes the database & containers exist
    # To create a new database use: client.create_database()
    # To create a new container use: database.create_container_if_not_exists()
    client = cosmo.CosmosClient(URL, credential=KEY)
    database = client.get_database_client(DATABASE_NAME)
    container = database.get_container_client(DB_CONTAINER_NAME)

    logging.info('Successfully connected to database/container')

    # Add or update document to container [CONTAINER_NAME]
    id = generate_unique_id(container)
    data.setdefault('id', id)
    container.upsert_item(data)

    logging.info('Successfully inserted a new record')


# Generate a random ID
# Throws an exception if max_attempts exceeded
def generate_unique_id(container):
    max_attempts = 5
    for i in range(max_attempts):
        generated_id = str(random.randint(10000000, 99999999))
        existing_items = container.query_items(
            query='select * from items x where x.id = @id',
            parameters=[
                dict(name='@id', value=generated_id)
            ],
            enable_cross_partition_query=True
        )
        if len(list(existing_items)) == 0:
            return generated_id

    raise Exception("Failed to generate a unique ID after {} attempts"
                    .format(max_attempts))
