# The Slayden Api
Hello! My name is Riley Slayden. 

This is the RESTful API I have developed to power my personal portfolio and blog, replacing my original legacy portfolio website. It is a way for me to manage all of my own content easily, as well as explore and experiment with different .NET API design patterns and best practices.

## API

I've designed this API to return helpful validation errors rather than using an exception flow. Exceptions should be for _exceptional_ issues, not as a way of handling the expected errors an API user will likely occur. As such, I've utilized the ErrorOr type, which allows me to return a list of errors to the API user OR the expected return type if there are no errors. 

## Data

This application is currently backed by a NoSQL Azure CosmosDB, which I currently use to store my blog `Post` objects.

## Build & Deployment
This API is successfully building its Docker image in GitHub actions (click the "Action" tabs of this repo to view) and publishing the image to DockerHub. I will eventually deploy this Docker image somewhere, and hook up a shiny new frontend.
