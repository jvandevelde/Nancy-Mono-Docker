FROM mono:4.6.1.3

MAINTAINER Jules Van de Velde <julesvandevelde@gmail.com

RUN mkdir /app
COPY ./NancyMonoDemo/bin/Debug /app
EXPOSE 8082
CMD ["mono", "/app/NancyMonoDemo.exe", "-d"]