# Local Events App

The Local Events App is a simple console application that allows users to manage local events. Users can add, modify, view, and delete events through the command line interface. The application utilizes Entity Framework Core and a SQLite database for data storage.

## Features

- **Add Event:** Add a new event with details such as title, date, description, and location.
- **Display Events:** View a list of saved events.
- **Modify Event:** Update the details of an existing event.
- **Delete Event:** Remove an event from the database.
- **Export to CSV:** Export saved events to a CSV file.

## Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) must be installed on your machine.

## Getting Started

1. Clone the repository:

    ```bash
    git clone https://github.com/your-username/local-events-app.git
    cd local-events-app
    ```

2. Build and run the application:

    ```bash
    dotnet run
    ```

3. Follow the on-screen prompts to interact with the Local Events App.

## Configuration

The application uses a SQLite database. The database connection string is specified in the `appsettings.json` file.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=events.db"
  }
}

## Implemented Features
1. Unit Tests
The Local Events App incorporates a comprehensive unit testing suite with a focus on ensuring the reliability and correctness of the application.

2. Regular Expression (Regex) Implementation
A regular expression is employed to ensure that the event fields are entered consistently, stored, and displayed in the same format.

3. Dictionary Usage
The application utilizes a dictionary to store and retrieve various values.

4. Error Logging
The app features an error logging mechanism that records errors, invalid inputs, or other significant events and writes them to a text file for future reference.