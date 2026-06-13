var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/elano_95_mail_ru", (string? x, string? y) =>
{
    if (!long.TryParse(x, out var a) ||
        !long.TryParse(y, out var b) ||
        a <= 0 || b <= 0)
    {
        // Возвращаем NaN как чистый текст
        return Results.Text("NaN", "text/plain", System.Text.Encoding.UTF8);
    }

    long gcd(long a, long b)
    {
        while (b != 0)
        {
            var t = b;
            b = a % b;
            a = t;
        }
        return a;
    }

    long lcm = (a / gcd(a, b)) * b;

    // Возвращаем число как чистый текст, чтобы бот не видел лишних кавычек или пробелов
    return Results.Text(lcm.ToString(), "text/plain", System.Text.Encoding.UTF8);
});

app.Run();
