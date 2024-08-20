# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiar csproj y restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar todo y construir
COPY . ./
RUN dotnet publish -c Release -o out DeployGame.csproj

# Etapa de producción
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Establecer el puerto de escucha
ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "DeployGame.dll"]