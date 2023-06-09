using System.Text;
using DB;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OperationalAdministrator.Middlewares;
using OperationalAdministrator.Services;
using OperationalAdministrator.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ExeptionMiddleware>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
        });
});
// Add services to the container.
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IOperationalService, OperationalService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Opreational Administrator", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt authorizarion",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                }
            }, 
            new string[] {}
        }
    });

});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWD:Issuer"],
            ValidAudience = builder.Configuration["JWD:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };

    }
);

var dbHost = "";

var dbName = "";

var dbPassword = "";

var connectionString = $"Data Source={dbHost}; Initial Catalog={dbName}; User ID=sa;Password={dbPassword}";

builder.Services.AddDbContext<OperationalAdministratorContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("OperationalAdministratorConnection"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));
    //options.UseSqlServer(connectionString);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<OperationalAdministratorContext>();
     context.Database.Migrate();
}
app.ConfigureExeptionMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
if (app.Environment.IsDevelopment())
{
    app.Run();
}
else
{
    app.Run();
}
