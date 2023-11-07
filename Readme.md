# QuickPaste v1.0

## Overview

This project experiments with a way to synchronize copied content across different machines. It comprises several key components:

- **.NET 6 Web API**: This component serves as the backbone of the synchronization system, providing a robust API to handle data transmission from Azure Cosmos DB service to the chrome extension.

- **Chrome Extension**: The extension is designed to interact with the web API, allowing users to effortlessly view copied content on their browsers (NOT AVAILABLE IN THE CHROME STORE AS YET)

- **Python Keyboard Listener**: A Python script that listens for keyboard events and triggers the synchronization process when a specific key combination (Ctrl + C + /) is detected.

- **Azure Function (Service Bus Trigger)**: This serverless function, written in Python, responds to incoming messages from the Listener on a Service Bus queue, ensuring reliable message processing.

## How it Works

1. Start the Python script on your PC. You can configure it how you want to run in the background, monitoring keyboard events.

2. When you press Ctrl + C + /, the script captures the copied content.

3. The content is then sent to the Azure Function via a Service Bus message.

4. The Azure Function processes the message and saves to a Cosmos DB service (Azure)

5. The Chrome Extension retrieves the content from the Web API and makes it available for use.

## Usage

1. Clone the repository to your local machine.

2. Set up and configure each component as per the provided documentation.

3. Run the Python script on your PC.

4. Install the Chrome Extension (NOT AVAILABLE IN THE CHROME STORE AS YET)

5. Begin synchronizing copied content across your PC devices effortlessly. (Not available for mobile)

## Development and DevOps

The project leverages GitHub Actions for continuous integration and deployment. Workflows are defined to automate the building,  and deployment processes.

## License

This project is licensed under the [MIT License](LICENSE). Feel free to use, modify, and distribute it as needed.

---

**Disclaimer**: This project is provided as-is, without warranty or support. Use at your own discretion.
