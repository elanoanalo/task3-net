using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// ТВОЙ ЭНДПОИНТ (ВЕБ-МЕТОД)
// ЗАМЕНИ "имя_фамилия_домен_com" на свой email, где точки и собачка заменены на '_'
app.MapGet("/elano_95_mail_ru", ([FromQuery] string? x, [FromQuery] string? y) =>
{
    // 1. Проверяем, пришли ли вообще числа и можно ли их распарсить
    if (!long.TryParse(x, out long numX) || !long.TryParse(y, out long numY))
    {
        return Results.Text("NaN");
    }

    // 2. Проверяем, натуральные ли они (строго больше нуля)
    if (numX <= 0 || numY <= 0)
    {
        return Results.Text("NaN");
    }

    // 3. Считаем НОК через функцию ниже
    long lcm = CalculateLCM(numX, numY);

    // 4. Возвращаем чистый текст, как просит задание
    return Results.Text(lcm.ToString());
});

app.Run();

// --- ТВОЯ МАТЕМАТИКА (ПРОСТО ФУНКЦИИ) ---

// Функция для поиска Наибольшего Общего Делителя (Алгоритм Евклида)
long CalculateGCD(long a, long b)
{
    while (b != 0)
    {
        long temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}

// Функция для поиска Наименьшего Общего Кратного
long CalculateLCM(long a, long b)
{
    return (a / CalculateGCD(a, b)) * b;
}