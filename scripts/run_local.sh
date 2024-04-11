#!/bin/bash

# Start DB.
echo "Starting DB..."
docker-compose up db -d

if [ $? -ne 0 ]; then
    echo "DB failed to start, exiting script."
    exit 1
fi

echo "DB started."

# Start API.
echo "Navigating to src/CLup.API..."
cd src/CLup.API
if [ $? -ne 0 ]; then
    echo "Failed to navigate to src/CLup.API, exiting script."
    echo "Make sure you run this script from the project root."
    exit 1
fi

echo "Starting API..."
nohup dotnet run &

if [ $? -ne 0 ]; then
    echo "API failed to start, exiting script."
    exit 1
fi

echo "API started."

# Navigate back to the starting point
cd - > /dev/null

# Start UI.
echo "Navigating to src/CLup.WebUI..."
cd src/CLup.WebUI
if [ $? -ne 0 ]; then
    echo "Failed to navigate to src/CLup.WebUI, exiting script."
    exit 1
fi

echo "Running 'npm run start'..."
npm run start

echo "UI is listening on localhost:3000."