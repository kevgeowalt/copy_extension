import os

SETTINGS = {
    'DATABASE_URL':os.environ.get('DATABASE_URL',''),
    'DATABASE_KEY':os.environ.get('DATABASE_KEY',''),
    'DATABASE_NAME':os.environ.get('DATABASE_NAME',''),
    'DB_CONTAINER_NAME':os.environ.get('DB_CONTAINER_NAME','')
}
