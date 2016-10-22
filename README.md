# Nancy-Mono-Docker
Self-hosted NancyFx web application sample running on Mono via Docker

# Getting started
There are a couple ways you can run the demo using Docker. The easiest is via
Docker Compose, which will automatically start the demo app as well as an Elasticsearch
instance to log requests to.

You can also manually start each container using the second method below to build/run the 
demo application in Docker

# 1 - Using Docker Compose to orchestrate multiple containers
1. Navigate to the root of this project
2. Run **./build.cmd**
  - We need to first build the Release target of the demo application before Docker can build an image
    from those outputs
  - This script will build the application in Release using MSBuild. 
  - Requires .NET 4.6 or Visual Studio 2015
3. Run `docker-compose up`
  - Will first build the Docker image for the demo Nancy application
  - Will then launch a container for both the demo application and an Elasticsearch
    instance to log any requests to
4. When you see the container logs stop in Docker Compose the application is ready
5. Navigate to the demo page at http://{docker-ip}:8081
  - This will display some basic information about the environment in the request
  - Each page view Will also log the request made to the Elasticsearch container
6. View logged page requests via Elasticsearch using
  - http://localhost:9200/_search?q=*
  - You can see if the index was created (after first demo page visit) at 
    - http://localhost:9200/_cat/indices?v
7. Use `docker-compose down` to stop & remove the containers locally

NOTE: Elasticsearch will not currently store requests on your local machine between
compose up/down requests. There are no persistent volumes defined. 

# 2 - Running containers manually via Docker
1. Navigate to the root of this project
2. Run **./build.cmd**
  - Will build the application in Release using MSBuild. Requires .NET 4.6 or Visual Studio 2015
3. Run **./build-docker.cmd**
  - Will build a docker image locally from the Release output generated above. Requires Docker to be installed
  - Image will be named `docker-nancy-mono`
  - View image locally using `docker images`
4. Run **./run-docker.cmd**
  - Will launch a container from the image created above and bind to http://{docker-ip}:8081
  - Container will be named `nancyfx-mono`
  - Use `docker ps -all` to view running images
  
Navigate to http://{docker-ip}:8081 in a browser to see the demo page which will display OS + some environment 
variables that were passed into the `docker run` command in ./run-docker.cmd

If using Docker for Windows {docker-ip} may be accesible via localhost. Otherwise you might need to find the IP 
of the VM docker is running inside of (in VirtualBox)

NOTE: This method doesn't automatically start an elasticsearch container. Use Docker Compose to see the integration
between multiple containers