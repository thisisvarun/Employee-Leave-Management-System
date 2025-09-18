using System.Text;
using System.Text.Json;
using backend.Common;
using backend.DTOs;

public class Authentication
{
    private readonly RequestDelegate _next;
    public Authentication(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()!;
        // if (string.IsNullOrEmpty(token))
        // {
        //     context.Response.StatusCode = 401;
        //     await context.Response.WriteAsync("Unauthorized");
        //     return;
        // }
        Console.WriteLine("I'm working on authentication! " + context.Request.Headers["Authorization"]);

        using var reader = new StreamReader(
            context.Request.Body,
            encoding: Encoding.UTF8,
            detectEncodingFromByteOrderMarks: false,
            bufferSize: 1024,
            leaveOpen: true); 

        var body = await reader.ReadToEndAsync();
        Console.WriteLine("[Request Body]" + body);
        LoginDTO deserializedBody = JsonSerializer.Deserialize<LoginDTO>(body)!;
        Console.WriteLine($"[Deserialized Request Body] {deserializedBody.Email} Email here");
        await _next(context);
    }
}

public static class AuthenticationExtension
{
    public static IApplicationBuilder UseJwtAuthentication(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<Authentication>();
    }
}