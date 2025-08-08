using System.Text;
using Booking.Infrastructure;
using Booking.Infrastructure.AuthService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

//databaza
builder.Services.AddDbContext<BookingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookingConnectionString")));

builder.Services.AddHttpContextAccessor();


// JWT
var jwtSection = builder.Configuration.GetSection("Jwt");
var key = jwtSection["Key"] ?? throw new InvalidOperationException("Jwt:Key not configured");
var issuer = jwtSection["Issuer"] ?? "BookingApi";
var audience = jwtSection["Audience"] ?? "BookingApiClients";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            RoleClaimType = "role"
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OwnerOnly", policy => policy.RequireRole("Owner"));
});

// Infra/Application wiring
builder.Services.ConfigureInfrastructureServices(builder.Configuration);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(o =>
    {
        o.InvalidModelStateResponseFactory = context =>
        {
            var problem = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation failed"
            };
            return new BadRequestObjectResult(problem);
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

