# build the docker image
docker build -t cinema.showtimes:latest .

# run the docker container
docker run -p 8080:80 --env ASPNETCORE_ENVIRONMENT=Production cinema.showtimes:latest