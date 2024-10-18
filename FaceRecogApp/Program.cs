using FaceRecogApp.Data;
using FaceRecogApp.Interfaces;
using FaceRecogApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IJobTitleRepository, JobTitleRepository>();
builder.Services.AddScoped<IPictureRepository, PictureRepository>();
builder.Services.AddScoped<IFaceServiceRepository, FaceServiceRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
} );

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

string CorsAddressHttp;
string CorsAddressHttps;

if (builder.Environment.IsDevelopment())
{
    CorsAddressHttp = builder.Configuration.GetValue<string>("CorsUrlHTTPDev");
    CorsAddressHttps = builder.Configuration.GetValue<string>("CorsUrlHTTPSDev");
} else
{
    CorsAddressHttp = builder.Configuration.GetValue<string>("CorsUrlHTTP");
    CorsAddressHttps = builder.Configuration.GetValue<string>("CorsUrlHTTPS");
}

app.UseCors(policy => 
    policy.WithOrigins(CorsAddressHttps, CorsAddressHttp)
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
