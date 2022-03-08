FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /app

COPY ./WeatherForecast.Api/WeatherForecast.Api.csproj ./app/WeatherForecast.Api/WeatherForecast.Api.csproj
COPY ./WeatherForecast.WarmUp/WeatherForecast.WarmUp.csproj ./app/WeatherForecast.WarmUp/WeatherForecast.WarmUp.csproj

RUN dotnet restore ./app/WeatherForecast.Api/WeatherForecast.Api.csproj
RUN dotnet restore ./app/WeatherForecast.WarmUp/WeatherForecast.WarmUp.csproj

RUN dotnet build ./app/WeatherForecast.Api/WeatherForecast.Api.csproj -c Release -o /app
RUN dotnet build ./app/WeatherForecast.WarmUp/WeatherForecast.WarmUp.csproj -c Release -o /app/warmup

RUN dotnet publish --no-restore ./app/WeatherForecast.Api/WeatherForecast.Api.csproj -c Release -o /app
RUN dotnet publish --no-restore ./app/WeatherForecast.WarmUp/WeatherForecast.WarmUp.csproj -c Release -o /app/warmup

COPY ./ops/docker/entrypoint.sh .
RUN chmod +x ./entrypoint.sh

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app .

#ENTRYPOINT ["./entrypoint.sh"]