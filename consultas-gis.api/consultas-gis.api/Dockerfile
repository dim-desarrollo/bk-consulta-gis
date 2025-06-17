FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY consultas-gis.api.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app


# runs it using aspnet runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "consultas-gis.api.dll"]