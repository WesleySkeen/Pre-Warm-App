FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

WORKDIR /app

COPY ./WeatherForecast.Web.Api ./app/WeatherForecast.Web.Api
COPY ./WeatherForecast.WarmUp ./app/WeatherForecast.WarmUp

RUN dotnet restore ./app/WeatherForecast.Web.Api/WeatherForecast.Web.Api.csproj
RUN dotnet restore ./app/WeatherForecast.WarmUp/WeatherForecast.WarmUp.csproj

RUN dotnet build ./app/WeatherForecast.Web.Api/WeatherForecast.Web.Api.csproj -c Release 
RUN dotnet build ./app/WeatherForecast.WarmUp/WeatherForecast.WarmUp.csproj -c Release

RUN dotnet publish --no-restore ./app/WeatherForecast.Web.Api/WeatherForecast.Web.Api.csproj -c Release -o /app
RUN dotnet publish --no-restore ./app/WeatherForecast.WarmUp/WeatherForecast.WarmUp.csproj -c Release -o /app/warmup

COPY ./ops/docker/entrypoint.sh .
RUN chmod +x ./entrypoint.sh

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app .

ENTRYPOINT ["./entrypoint.sh"]