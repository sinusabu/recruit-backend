FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
RUN apt-get update

COPY ./HappyHour.Api/HappyHour.Api.csproj .
COPY ./HappyHour.Business/HappyHour.Business.csproj .
COPY ./HappyHour.DataAccess/HappyHour.DataAccess.csproj .
COPY ./HappyHour.Tests/HappyHour.Tests.csproj .

RUN dotnet restore HappyHour.Tests.csproj
RUN dotnet restore HappyHour.DataAccess.csproj
RUN dotnet restore HappyHour.Business.csproj
RUN dotnet restore HappyHour.Api.csproj

COPY . ./
RUN dotnet build ./HappyHour.Api/HappyHour.Api.csproj -c Release -o output
RUN dotnet build ./HappyHour.Business/HappyHour.Business.csproj -c Release -o output
RUN dotnet build ./HappyHour.DataAccess/HappyHour.DataAccess.csproj -c Release -o output
RUN dotnet build ./HappyHour.Tests/HappyHour.Tests.csproj -c Release -o output

FROM build AS publish
RUN dotnet publish ./HappyHour.Api/HappyHour.Api.csproj -c Release -o out
RUN dotnet publish ./HappyHour.Business/HappyHour.Business.csproj -c Release -o out
RUN dotnet publish ./HappyHour.DataAccess/HappyHour.DataAccess.csproj -c Release -o out

FROM base as deploy
COPY --from=publish /src/output .
ENTRYPOINT ["dotnet", "HappyHour.Api.dll"]
