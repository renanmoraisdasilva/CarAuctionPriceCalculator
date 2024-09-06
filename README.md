# Car Auction Price Calculator

This project consists of a .NET backend and a Vue frontend. Follow the instructions below to get started with both parts of the application.

## Backend Setup

The backend is built with .NET and uses a local database by default.

### Prerequisites

- Visual Studio 2022 or later

### Steps

1. Open the solution file `CarAuctionPriceCalculator.sln` located in the `backend` directory with Visual Studio.
2. Ensure that the default project is set to `CarAuctionPriceCalculator.API`.
3. Run the application by pressing `F5` or clicking on the "Start" button in Visual Studio.

The backend will use a local database (`(localdb)\mssqllocaldb`) as specified in the `appsettings.Development.json` file.

### Running Tests

1. Open the Test Explorer in Visual Studio (`Test` > `Test Explorer`).
2. Click on "Run All" to execute all the tests.

## Frontend Setup

The frontend is built with Vue 3 and Vite.

### Prerequisites

- Node.js (version 14 or later)

### Steps

1. Navigate to the `frontend/CarAuctionPriceCalculator.UI` directory.
2. Install the required Node.js modules:

    ```sh
    npm install
    ```

3. Start the development server:

    ```sh
    npm run dev
    ```

The frontend will be available at `http://localhost:5173`.

### Running Tests

1. Navigate to the `frontend/CarAuctionPriceCalculator.UI` directory.
2. Run the tests using the following command:

    ```sh
    npm run test:unit
    ```
