import os

SETTINGS = {
    "SERVICE_BUS_CONNECTION_STR": os.environ.get(
        "SERVICE_BUS_CONNECTION_STR",
        "",
    ),
    "SERVICE_BUS_QUEUE_NAME": os.environ.get("SERVICE_BUS_QUEUE_NAME", ""),
    "CALL_AZURE": True,
}
