#!/bin/sh

(dotnet ./warmup/WeatherForecast.WarmUp.dll) &

dotnet WeatherForecast.Web.Api.dll