using KartuliAPI1;
using KartuliAPI1.Data.Repositories;
using KartuliAPI1.Data.Repositories.KartuliAPI1.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<IRecipesRepository, RecipesRepository>();
builder.Services.AddTransient<IWinesRepository, WinesRepository>();
builder.Services.AddTransient<JwtService>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new JwtService(
        configuration["Jwt:Issuer"],
        configuration["Jwt:Audience"],
        configuration["Jwt:SecretKey"]
    );
});

builder.Services.AddControllers();

builder.Services.AddTransient<JwtService>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new JwtService(
        configuration["JwtRecipes:Issuer"],
        configuration["JwtRecipes:Audience"],
        configuration["JwtRecipes:SecretKey"]
    );
});

builder.Services.AddControllers();

builder.Services.AddTransient<JwtService>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new JwtService(
        configuration["JwtWines:Issuer"],
        configuration["JwtWines:Audience"],
        configuration["JwtWines:SecretKey"]
    );
});
;



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})






.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:5052", 
        ValidAudience = "https://localhost:5052/api/users",  
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("S9La1gi7O1Ijn9L8paC4OYvVx1U0VMt8")) 

};
   /* options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:5052",
        ValidAudience = "https://localhost:5052/api/recipes", 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("S9La1gi7O1Ijn9L8paC4OYvVx1U0VMt8"))
    };

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:5052",
        ValidAudience = "https://localhost:5052/api/wines", 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("S9La1gi7O1Ijn9L8paC4OYvVx1U0VMt8"))
    };*/
});

var app = builder.Build();

app.UseHttpsRedirection();


app.UseRouting();


app.UseAuthentication();


app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
