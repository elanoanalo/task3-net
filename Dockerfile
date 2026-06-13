FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем абсолютно все файлы проекта внутрь
COPY . .

# Сами находим файл проекта и собираем его, имя вводить не нужно!
RUN dotnet restore && \
    dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Запуск приложения
ENTRYPOINT ["dotnet", "task3.dll"]
