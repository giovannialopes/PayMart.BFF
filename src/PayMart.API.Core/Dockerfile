FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY ./PayMart.Domain.Core/PayMart.Domain.Core.csproj ./PayMart.Domain.Core/
COPY ./PayMart.Infrastructure.Core/PayMart.Infrastructure.Core.csproj ./PayMart.Infrastructure.Core/
COPY ./PayMart.API.Core/PayMart.API.Core.csproj ./PayMart.API.Core/
RUN dotnet restore ./PayMart.API.Core/PayMart.API.Core.csproj

COPY ./PayMart.Domain.Core ./PayMart.Domain.Core/
COPY ./PayMart.Infrastructure.Core ./PayMart.Infrastructure.Core/
COPY ./PayMart.API.Core ./PayMart.API.Core/

WORKDIR /app/PayMart.API.Core
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/PayMart.API.Core/out .

EXPOSE 4999

ENTRYPOINT ["dotnet", "PayMart.API.Core.dll"]
