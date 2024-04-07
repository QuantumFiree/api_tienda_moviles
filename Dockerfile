COPY ./*.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publis -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /webapp
COPY --from=build /webapp/out .
