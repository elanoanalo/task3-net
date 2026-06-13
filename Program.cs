var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Сделали параметры x? и y? необязательными (добавили знак вопроса)
app.MapGet("/elano_95_mail_ru", (double? x, double? y) =>
{
    // Если Render или бот ломится без параметров, просто отдаем 0 и статус 200, чтобы сервер не падал
    if (x == null || y == null)
    {
        return Results.Text("0", "text/plain", System.Text.Encoding.UTF8);
    }

    double lcm = CalculateLCM(x.Value, y.Value);
    return Results.Text(lcm.ToString(), "text/plain", System.Text.Encoding.UTF8);
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
