# Multi-stage Dockerfile for cinema.moviecatalog (ASP.NET Core minimal API)
# Build: docker build -t cinema.moviecatalog:latest .
# Run:  docker run -p 8080:80 --env ASPNETCORE_ENVIRONMENT=Production cinema.moviecatalog:latest

# Use buildkit syntax features if available
# syntax=docker/dockerfile:1.5

ARG DOTNET_VERSION=10.0

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS base
WORKDIR /app

# Listen on port 80 and enable container-friendly defaults
ENV ASPNETCORE_URLS="http://+:80" \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_TELEMETRY_OPTOUT=1
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

# Copy project file and restore dependencies (cache restore layer)
COPY ["src/Cinema.MovieCatalog.csproj", "./"]
RUN dotnet restore "Cinema.MovieCatalog.csproj"

# Copy remaining source and publish a trimmed, release build
COPY ./src .
RUN dotnet publish "Cinema.MovieCatalog.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish ./

# Use a non-root user if desired (uncomment to enable)
# RUN addgroup --system app && adduser --system --ingroup app app && chown -R app:app /app
# USER app

ENTRYPOINT ["dotnet", "Cinema.MovieCatalog.dll"]
