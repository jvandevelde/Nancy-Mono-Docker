# Nancy-Mono-Docker
Self-hosted NancyFx web application sample running on Mono via Docker

# Getting started
To build/run application in Docker

1. Run **./build.cmd**
  - Will build the application in Release using MSBuild. Requires .NET 4.6 or Visual Studio 2015
2. Run **./build-docker.cmd**
  - Will build a docker image locally from the Release output generated above. Requires Docker to be installed
  - Image will be named `docker-nancy-mono`
  - View image locally using `docker images`
3. Run **./run-docker.cmd**
  - Will launch a container from the image created above and bind to http://{docker-ip}:8081
  - Container will be named `nancyfx-mono`
  - Use `docker ps -all` to view running images
  
Navigate to http://{docker-ip}:8081 in a browser to see the demo page which will display OS + some environment 
variables that were passed into the `docker run` command in ./run-docker.cmd

If using Docker for Windows {docker-ip} may be accesible via localhost. Otherwise you might need to find the IP 
of the VM docker is running inside of (in VirtualBox)
