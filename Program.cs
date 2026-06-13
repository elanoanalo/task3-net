using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/elano_95_mail_ru", (HttpContext context) =>
{
    // Безопасно вытягиваем x и y из запроса, игнорируя региональные стандарты (точки/запятые)
    string? xQuery = context.Request.Query["x"];
    string? yQuery = context.Request.Query["y"];

    if (double.TryParse(xQuery, CultureInfo.InvariantCulture, out double x) &&
        double.TryParse(yQuery, CultureInfo.InvariantCulture, out double y))
    {
        // Считаем НОК (Наименьшее общее кратное)
        double lcm = CalculateLCM(x, y);
        
        // Возвращаем результат в виде строки без лишних символов
        return context.Response.WriteAsync(lcm.ToString(CultureInfo.InvariantCulture));
    }

    // Если что-то пошло не так, возвращаем ошибку, чтобы сразу это увидеть
    context.Response.StatusCode = 400;
    return context.Response.WriteAsync("Ошибка: передайте числа x и y");
});

// Метод поиска НОД
static double CalculateGCD(double a, double b)
{
    while (Math.Abs(b) > 0.000001)
    {
        double temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}

// Метод поиска НОК
static double CalculateLCM(double a, double b)
{
    if (a == 0 || b == 0) return 0;
    return Math.Abs(a * b) / CalculateGCD(a, b);
}

app.Run();
