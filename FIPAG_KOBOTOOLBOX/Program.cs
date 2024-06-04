using AspNetCoreRateLimit;
using Dates;
using FIPAG_KOBOTOOLBOX.Domains.Interface;
using FIPAG_KOBOTOOLBOX.Extensions;
using FIPAG_KOBOTOOLBOX.Jobs;
using FIPAG_KOBOTOOLBOX.Persistence.Contexts;
using FIPAG_KOBOTOOLBOX.Persistence.Repositories;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FIPAG_KOBOTOOLBOX.Domains.Interfaces;
using FIPAG_KOBOTOOLBOX.Helper;
using FIPAG_KOBOTOOLBOX.MiddleWares;

using FIPAG_KOBOTOOLBOX.Services;
using System.Diagnostics;
using System.Globalization;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

var cronJobs = new CronJobs();


ConversionExtension conversionExtension = new ConversionExtension();
//2022-10-19 12:59:44'


ConfigurationManager configuration = builder.Configuration;

//builder.Services.AddDbContext<SGOFCTX>(options => options.UseSqlServer(configuration.GetConnectionString("DBconnect")));
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DBconnect"),

        sqlServerOptions => sqlServerOptions.CommandTimeout(120));
});
builder.Services.AddDbContext<AuthAppContext>(options => options.UseSqlServer(configuration.GetConnectionString("DBconnect")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthAppContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IGenericRepository, GenericRepository>();
builder.Services.AddScoped<IKOBORepository, KOBORepository>();
builder.Services.AddScoped<IKOBOService, KOBOService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Debug.Print("OnAuthenticationFailed " + context.Exception.Message.ToString());
            //var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Startup>>();
            //ogger.LogError(context.Exception, "Authentication failed.");
            return Task.CompletedTask;
        },
        OnForbidden = context =>
        {

            Debug.Print("OnForbidden " + context.Response.Body.ToString());
            return Task.CompletedTask;
        },

        OnChallenge = context =>
        {

            Debug.Print(context.Error);
            return Task.CompletedTask;
        },
        // Add more event handlers as needed
    };
    options.SaveToken = false;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddInMemoryRateLimiting();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMvcCore().AddApiExplorer();
builder.Services.AddHangfire(configuration => configuration.UseSqlServerStorage(builder.Configuration.GetConnectionString("DBconnect"), new SqlServerStorageOptions
{
    SchemaName = "SFGOFHANGFIRE",

}));


//builder.Services.AddControllers(o =>
//{
//    o.Filters.Add(typeof(GlobalResponses));
//});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});
//builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();
//TFRService TFRService;
app.UseIpRateLimiting();
app.UseHangfireServer();

app.UseHangfireDashboard("/Jobs");



// Configure the root path ("/") to return the HTML file
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<HttpLoggingMiddleware>();
app.UseEndpoints(endpoints =>
{

    //endpoints.MapHealthChecksUI();

    endpoints.MapGet("/", async context => await context.Response.WriteAsync("THE WEB SERVER IS ON!"));
});

//app.UseHttpsRedirection();



cronJobs.JobHandler();

app.MapControllers();

app.Run();