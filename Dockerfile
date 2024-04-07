# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# WORKDIR /webapp

# EXPOSE 80
# EXPOSE 5001


# COPY ./*.csproj .
# RUN dotnet restore

# COPY . .
# RUN dotnet publish -c Release -o out

# FROM mcr.microsoft.com/dotnet/sdk:6.0
# WORKDIR /webapp
# COPY --from=build /webapp/out .
# ENTRYPOINT [ "dotnet", "api_tienda_moviles.dll" ]
#####################################################
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY ./ ./

RUN dotnet publish "./api_tienda_moviles.csproj" -c Release -o ./out/

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
EXPOSE 80
WORKDIR /app
COPY --from=build /app/out/ .
RUN apt-get update && apt-get install -y curl
ENTRYPOINT ["dotnet", "api_tienda_moviles.dll"]