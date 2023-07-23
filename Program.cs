/*
💡 Referencias para o código deste projeto:

➡️ https://www.youtube.com/watch?v=vAUXU0YIWlU
➡️ https://www.youtube.com/watch?v=t5iumvSNbgM
➡️ https://www.youtube.com/watch?v=v7q3pEK1EA0

➡️ https://balta.io/artigos/aspnetcore-3-autenticacao-autorizacao-bearer-jwt
➡️ https://medium.com/@mmoshikoo/jwt-authentication-using-c-54e0c71f21b0
➡️ https://renatogroffe.medium.com/net-6-asp-net-core-jwt-swagger-implementando-a-utiliza%C3%A7%C3%A3o-de-tokens-5d04cda20fa8
➡️ https://dev.to/mgpaixao/criando-api-com-jwt-autorizacao-e-autenticacao-modulo-1-20b8

🔹 O objetivo é aprender a utilizar o JWT com .NET 6.0.

🌐 https://jwt.io/
*/

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JTW Tutorial", Version = "v1" });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(Options =>
    {
        Options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWT Tutorial v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
