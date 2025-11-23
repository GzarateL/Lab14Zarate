# 1. Usar la imagen del SDK de .NET 9 para compilar
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 2. Copiar los archivos de proyecto (csproj) y restaurar dependencias
COPY ["EjercicioGaelZarate/EjercicioGaelZarate.csproj", "EjercicioGaelZarate/"]
COPY ["EjercicioGaelZarate.Application/EjercicioGaelZarate.Application.csproj", "EjercicioGaelZarate.Application/"]
COPY ["EjercicioGaelZarate.Domain/EjercicioGaelZarate.Domain.csproj", "EjercicioGaelZarate.Domain/"]
COPY ["EjercicioGaelZarate.Infrastructure/EjercicioGaelZarate.Infrastructure.csproj", "EjercicioGaelZarate.Infrastructure/"]

RUN dotnet restore "EjercicioGaelZarate/EjercicioGaelZarate.csproj"

# 3. Copiar el resto del código fuente
COPY . .

# 4. Compilar la aplicación
WORKDIR "/src/EjercicioGaelZarate"
RUN dotnet build "EjercicioGaelZarate.csproj" -c Release -o /app/build

# 5. Publicar la aplicación
FROM build AS publish
RUN dotnet publish "EjercicioGaelZarate.csproj" -c Release -o /app/publish

# 6. Configurar la imagen final para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Render asigna un puerto dinámico, pero .NET 8/9 usa el 8080 por defecto.
# Exponemos el puerto para documentación.
EXPOSE 8080

ENTRYPOINT ["dotnet", "EjercicioGaelZarate.dll"]