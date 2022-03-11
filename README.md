# Pre-Warm-App
A solution to warming up an app before adding it to the load balancer

## Build and run docker container
```
docker build -f ops/docker/Dockerfile . -t docker_app_tag
docker run -d -p 8080:80 --name docker_app_name docker_app_tag
