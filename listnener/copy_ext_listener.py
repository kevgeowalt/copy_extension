from pynput import keyboard
from pyperclip import copy, paste
import os
import platform
import socket
import json
import sys
from PIL import ImageGrab

import config
# Azure imports
from azure.servicebus import ServiceBusClient, ServiceBusMessage

# Initialization
SERVICE_BUS_CONNECTION_STR = config.SETTINGS['SERVICE_BUS_CONNECTION_STR']
SERVICE_BUS_QUEUE_NAME = config.SETTINGS['SERVICE_BUS_QUEUE_NAME']
CALL_AZURE = config.SETTINGS['CALL_AZURE']

def on_user_copy():
    computer_name = get_computer_name()
    copied_txt = paste()
    PACKET = {'computerName':computer_name, 'system':platform.system(), 'text':copied_txt}

    copied_image = ImageGrab.grabclipboard()
    if copied_image is not None:
        copied_image.show()

    azure_queue_msg(PACKET)

def azure_queue_msg(packet):
    if CALL_AZURE:
        with ServiceBusClient.from_connection_string(SERVICE_BUS_CONNECTION_STR) as client:
            with client.get_queue_sender(SERVICE_BUS_QUEUE_NAME) as sender:
                # Queue packet for processing
                json_str = json.dumps(packet)
                msg = ServiceBusMessage(json_str)
                sender.send_messages(msg)
    else:
        print(packet)

def for_canonical(f):
    return lambda k: f(l.canonical(k))

def get_computer_name():
    name_f_platform = platform.node()
    name_f_socket = socket.gethostname()
    name_f_os = os.environ['COMPUTERNAME']

    if name_f_platform == name_f_socket or name_f_platform == name_f_os:
        return name_f_platform
    elif name_f_socket == name_f_os:
        return name_f_socket
    else:
        return 'Unknown [Different]'

hotkey = keyboard.HotKey(keyboard.HotKey.parse('<ctrl>+c+/'),on_user_copy)

with keyboard.Listener(on_press=for_canonical(hotkey.press),on_release=for_canonical(hotkey.release)) as l:
    l.join()
