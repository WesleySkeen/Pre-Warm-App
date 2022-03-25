# Pre-Warm-App
A solution to warming up an app before adding it to the load balancer

## WeatherForecast.WarmUp
A console app that does the following
1. Check if the API within the container is available to start receiving requests.
2. Hits the API locally in the container several times to warm it up. The API is very simple and does not have many dependancies that need warming up. Your API may be different.
3. Creates a file to symbolise that the API has been warmed. You may or may not want to use this technique to state that the API is warmed.

## WeatherForecast.Web.Api
The Web API checks contains a hosted task that checks if the file was created by the warm up console app. If the file exists then an internal warm up status (`WarmUpStatus.IsWarmedUp`) is set to true. the health endpoint checks this status which signifies if the app is healthy and warmed up.

## Build and run docker container
```
docker build -f ops/docker/Dockerfile . -t docker_app_tag
docker run -d -p 8080:80 --name docker_app_name docker_app_tag
```

## entrypoint.sh

This file first runs the WarmUp app in a background thread and then starts the Web API. 
