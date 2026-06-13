FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем всё
COPY . .

# Собираем
RUN dotnet restore && \
    dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Запуск. Если в Visual Studio проект называется Task3 (с большой буквы), то измени на Task3.dll
ENTRYPOINT ["dotnet", "task3.dll"]
