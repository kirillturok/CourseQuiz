using CourseQuiz.API;
using CourseQuiz.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

AuthOptions.EmailLogin = args[0];
AuthOptions.EmailPassword = args[1];

string conStr = "Server=localhost;Database=CourseQuiz;Trusted_Connection=True;MultipleActiveResultSets=true";
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(conStr));


builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
  {
      options.RequireHttpsMetadata = false;
      options.TokenValidationParameters = new TokenValidationParameters
      {
          // ��������, ����� �� �������������� �������� ��� ��������� ������
          ValidateIssuer = true,
          // ������, �������������� ��������
          ValidIssuer = AuthOptions.ISSUER,

          // ����� �� �������������� ����������� ������
          ValidateAudience = true,
          // ��������� ����������� ������
          ValidAudience = AuthOptions.AUDIENCE,
          // ����� �� �������������� ����� �������������
          ValidateLifetime = true,

          // ��������� ����� ������������
          IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey,
          // ��������� ����� ������������
          ValidateIssuerSigningKey = true,
      };
  });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
