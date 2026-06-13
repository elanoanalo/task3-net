var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// .NET сам автоматически возьмет x и y из ссылки и переведет в double
app.MapGet("/elano_95_mail_ru", (double x, double y) =>
{
    double lcm = CalculateLCM(x, y);
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
