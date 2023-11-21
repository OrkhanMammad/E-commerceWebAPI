using E_commerce.API.Configurations;
using E_commerce.Application.Validators.ProductValidators;
using E_commerce.Infrastructure;
using E_commerce.Persistence;
using E_commerce.SignalR;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<AddProductValidator>()); ;
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddSignalRServices();



builder.Services.AddCors
    (Options => Options.AddDefaultPolicy
    (policy => policy.AllowAnyOrigin()/*WithOrigins("http://localhost:4200", "https://localhost:4200")*/.AllowAnyHeader().AllowAnyMethod()));//cors policy

SqlColumn sqlColumn = new SqlColumn();
sqlColumn.ColumnName = "UserName";
sqlColumn.DataType = System.Data.SqlDbType.NVarChar;
sqlColumn.PropertyName = "UserName";
sqlColumn.DataLength = 50;
sqlColumn.AllowNull = true;
ColumnOptions columnOpt = new ColumnOptions();
columnOpt.Store.Remove(StandardColumn.Properties);
columnOpt.Store.Add(StandardColumn.LogEvent);
columnOpt.AdditionalColumns = new Collection<SqlColumn> { sqlColumn };

Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.MSSqlServer(
    connectionString: "Server=DESKTOP-CJBAJUG\\SQLEXPRESS; Database=E-commerceWebApi; Trusted_Connection=true; Encrypt=False;",
     sinkOptions: new MSSqlServerSinkOptions
     {
         AutoCreateSqlTable = true,
         TableName = "logs",
     },
     appConfiguration: null,
     columnOptions: columnOpt
    )
    .Enrich.FromLogContext()
    .Enrich.With<CustomUserNameColumn>()
    .MinimumLevel.Information()
    .CreateLogger();
builder.Host.UseSerilog(log);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,


            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires,securityToken, validationParameters) => expires != null ? expires>DateTime.UtcNow : false,
            NameClaimType=ClaimTypes.Name, //JWT UZERINDE NAME CLAIMINE QARSILIQ GELEN DEYERI USER.IDENTITY.NAME ILE ELDE EDE BILERIK


        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseSerilogRequestLogging();

app.UseCors();//cors politicsin tetbiq olunmasi

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var username = context.User.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("UserName", username);

    await next();
});

app.MapControllers();
app.MapHubs();

app.Run();
