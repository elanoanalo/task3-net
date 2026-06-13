using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Настраиваем роут-шаблон, который примет любой вложенный путь, лишь бы в конце было elano_95_mail_ru
app.MapGet("{*url}", (HttpContext context) =>
{
    var path = context.Request.Path.Value ?? "";
    
    // Проверяем, заканчивается ли вызванный URL на твою почту
    if (!path.EndsWith("elano_95_mail_ru", StringComparison.OrdinalIgnoreCase))
    {
        context.Response.StatusCode = 404;
        return Task.CompletedTask;
    }

    string? x = context.Request.Query["x"];
    string? y = context.Request.Query["y"];

    if (string.IsNullOrEmpty(x) || string.IsNullOrEmpty(y) ||
        !double.TryParse(x, CultureInfo.InvariantCulture, out double numX) ||
        !double.TryParse(y, CultureInfo.InvariantCulture, out double numY) ||
        numX <= 0 || numY <= 0 || numX % 1 != 0 || numY % 1 != 0)
    {
        return context.Response.WriteAsync("NaN");
    }

    long a = (long)numX;
    long b = (long)numY;

    long gcd(long n1, long n2)
    {
        while (n2 != 0)
        {
            var t = n2;
            n2 = n1 % n2;
            n1 = t;
        }
        return n1;
    }

    long lcm = (a / gcd(a, b)) * b;

    // Возвращаем строго строку, без лишних оберток фреймворка
    return context.Response.WriteAsync(lcm.ToString());
});

app.Run();
