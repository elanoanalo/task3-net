var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Server is running");

app.MapGet("/elano_95_mail_ru", (long? x, long? y) =>
{
    if (x == null || y == null)
        return Results.Text("NaN");

    if (x <= 0 || y <= 0)
        return Results.Text("NaN");

    long lcm = CalculateLCM(x.Value, y.Value);

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
