using System.Threading.RateLimiting;
using CryptoKeyLab.Components;
using CryptoKeyLab.Services;
using Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure Appsetting
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

//injecting http client service
builder.Services.AddHttpClient();

//injecting File Export Service
builder.Services.AddScoped<FileExportService>();

//add rate limiter service with a limit of 100 requests per hour
//builder.Services.AddRateLimiter(options => 
//{
//    //REJECT STATUS CODE 429 TOO MANY REQUESTS
//    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

//    //create a policy that limits to 3 requests per minute per client IP
//    options.AddPolicy("RsaGenRateLimit", context => 
//    {
//        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

//        return RateLimitPartition.GetFixedWindowLimiter(ip, _ => new FixedWindowRateLimiterOptions
//        {
//            PermitLimit = 3, //allow 3 requests
//            Window = TimeSpan.FromMinutes(1), //per minute
//            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
//            QueueLimit = 0 //no queuing, reject immediately when limit is exceeded
//        });
//    });
//});

//for allowed to read limits
builder.Services.AddHttpContextAccessor();

//inject the services
builder.Services.AddSingleton(new CryptoKeyLab.Services.RateLimiter(5, TimeSpan.FromMinutes(30)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
