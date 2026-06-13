using System.Numerics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("{*url}", (HttpContext context) =>
{
    // 1. Более надежная проверка пути (игнорируем слэш в конце)
    var path = context.Request.Path.Value?.TrimEnd('/') ?? "";
    if (!path.EndsWith("elano_95_mail_ru", StringComparison.OrdinalIgnoreCase))
    {
        return Results.NotFound();
    }

    // 2. Читаем параметры
    string? xStr = context.Request.Query["x"];
    string? yStr = context.Request.Query["y"];

    // 3. Используем BigInteger для исключения потери точности и переполнения
    // BigInteger.TryParse не пропустит дробные числа (например, 1.5), что нам и нужно
    if (BigInteger.TryParse(xStr, out var x) && 
        BigInteger.TryParse(yStr, out var y) && 
        x > 0 && y > 0)
    {
        // НОК(x, y) = (x * y) / НОД(x, y)
        // Чтобы избежать лишних огромных чисел при умножении, сначала делим
        var gcd = BigInteger.GreatestCommonDivisor(x, y);
        var lcm = (x / gcd) * y;

        // 4. Results.Text автоматически ставит Content-Type: text/plain; charset=utf-8
        return Results.Text(lcm.ToString(), "text/plain");
    }

    // Если не натуральное число (0, отрицательное, дробное или текст)
    return Results.Text("NaN", "text/plain");
});

app.Run();
