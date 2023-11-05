import os

SETTINGS = {
    'DATABASE_URL':os.environ.get('DATABASE_URL',''),
    'DATABASE_KEY':os.environ.get('DATABASE_KEY',''),
    'DATABASE_NAME':os.environ.get('DATABASE_NAME'),
    'CONTAINER_NAME':os.environ.get('CONTAINER_NAME','')
}