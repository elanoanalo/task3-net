FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Копируем все файлы проекта
COPY . .

# Восстанавливаем зависимости и собираем проект
RUN dotnet restore && \
    dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Запуск приложения (убедись, что регистр букв в task3.dll совпадает с твоим проектом)
ENTRYPOINT ["dotnet", "task3.dll"]
