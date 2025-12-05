# 1. Usamos la imagen oficial de .NET SDK para compilar el proyecto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos el archivo de proyecto (.csproj) y restauramos dependencias
COPY ["EntregaAlex.csproj", "./"]
RUN dotnet restore "./EntregaAlex.csproj"

# Copiamos todo el resto del código y compilamos
COPY . .
WORKDIR "/src/."
RUN dotnet build "EntregaAlex.csproj" -c Release -o /app/build

# Publicamos la aplicación en una carpeta final
FROM build AS publish
RUN dotnet publish "EntregaAlex.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 2. Usamos la imagen base de ASP.NET para ejecutar la aplicación (más ligera)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Exponemos el puerto interno (8080 es el estándar en .NET 8)
EXPOSE 8080

# Comando para arrancar la API al iniciar el contenedor
ENTRYPOINT ["dotnet", "EntregaAlex.dll"]