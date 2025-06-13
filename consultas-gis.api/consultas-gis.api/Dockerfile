# Imagen base para ejecutar .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiamos la publicación hecha por Jenkins
COPY out/ .

EXPOSE 8080
ENTRYPOINT ["dotnet", "TuProyecto.WebApi.dll"]
