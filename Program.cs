var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Server is running");

app.MapGet("/elano_95_mail_ru", (string? x, string? y) =>
{
    if (!long.TryParse(x, out long a) ||
        !long.TryParse(y, out long b) ||
        a <= 0 ||
        b <= 0)
    {
        return Results.Text("NaN");
    }

    long lcm = CalculateLCM(a, b);
    return Results.Text(lcm.ToString());
});

static long CalculateGCD(long a, long b)
{
    while (b != 0)
    {
        long temp = b;
        b = a % b;
        a = temp;
    }

    return a;
}

static long CalculateLCM(long a, long b)
{
    return a / CalculateGCD(a, b) * b;
}

app.Run();
