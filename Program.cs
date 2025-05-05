using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentMangementSystemC8.Database;
using StudentMangementSystemC8.Database.Entities;
using StudentMangementSystemC8.Settings;
using System.Runtime;
using Microsoft.IdentityModel.Tokens;
using StudentMangementSystemC8.Models;
using System.Text;
using StudentMangementSystemC8.Service;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Other Swagger config (title, version, etc.)
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Add JWT Authentication support in Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Make Swagger require a token for authorized endpoints
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.Configure<UseAI>(builder.Configuration.GetSection("UseAI"));

builder.Services.Configure<JwtTokenInfo>(builder.Configuration.GetSection("jwt"));

//registering JwtService in DI Container for dependancy injection
builder.Services.AddScoped<JwtService>();
//builder.Services.AddScoped<JwtTokenInfo>();

builder.Services.AddDbContext<AppDbContext>(
    
    (options) =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")).LogTo(Console.WriteLine);
            
    }

    );

builder.Services.AddIdentity<User, IdentityRole<long>>()
    .AddEntityFrameworkStores<AppDbContext>();



//getting jwttokeninfo object from appsettings.
JwtTokenInfo? jwtTokenInfo = builder.Configuration.GetSection("jwt").Get<JwtTokenInfo>();

//Jwt registration
builder.Services.AddAuthentication(
        (options) =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    ).AddJwtBearer(
    
        (options) => {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = jwtTokenInfo!.Issuer,

                ValidateAudience = true,
                ValidAudience = jwtTokenInfo!.Audience,

                ValidateLifetime = true,

                //ClockSkew = TimeSpan.Zero, // Strict expiry validation
                

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenInfo.Key))
            };

        }
    );


builder.Services.AddAuthorization(); 



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
