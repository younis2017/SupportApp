using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using SupportApp.Application.Abstractions.Persistence;
using SupportApp.Application.Abstractions.Services;
using SupportApp.Application.Services;
using SupportApp.Infrastructure.Persistence;
using SupportApp.Infrastructure.Persistence.Options;
using SupportApp.Infrastructure.Persistence.Repositories;
var builder = WebApplication.CreateBuilder(args);

// =======================
// SERVICES
// =======================
//builder.Services.AddDbContext<NassadContext>(options =>
//    options.UseSqlServer("Server=db36600.public.databaseasp.net; Database=db36600; User Id=db36600; Password=Ln3+t%Z2P7#k; Encrypt=True; TrustServerCertificate=True;", b =>
//        b.MigrationsAssembly("nasssys.Infrastructure") // <--- migrations go here
//    )
//);

// SignalR
builder.Services.AddSignalR();


// MVC + API
builder.Services.AddControllersWithViews();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
    );
//allow API to access from nassad.ca
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNassad",
        policy =>
        {
            policy
                .WithOrigins(
                    "https://nassad.ca",
                    "https://www.nassad.ca"
                )
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// JWT Token
builder.Services.AddSingleton<JwtService>();

// Twilio SMS Service
//builder.Services.AddSingleton<TwilioSmsService>();

// EF Core
builder.Services.AddDbContext<SupportAppDbContext>((sp, options) =>
{
    var dbOptions = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
    options.UseSqlServer(dbOptions.ConnectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(dbOptions.MaxRetryCount);
    });
});

// Swagger (enabled in all environments)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "SupportApp API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter JWT token"
        });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
// Add services 
builder.Services.AddPersistenceServices(builder.Configuration);

// api versioning and explorer
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
// JWT AUTHENTICATION
var jwtSettings = builder.Configuration.GetSection("Jwt");

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.RequireHttpsMetadata = false;
//    options.SaveToken = true;

//    options.TokenValidationParameters = new TokenValidationParameters
//        {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,

//        ValidIssuer = jwtSettings["Issuer"],
//        ValidAudience = jwtSettings["Audience"],

//        IssuerSigningKey = new SymmetricSecurityKey(
//            Encoding.UTF8.GetBytes(jwtSettings["Key"])
//        )
//        };
//});

// ===== OpenTelemetry Tracing =====
builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .SetResourceBuilder(
                ResourceBuilder.CreateDefault().AddService("nasssys-api")
            )
            //.AddAspNetCoreInstrumentation()
            //.AddHttpClientInstrumentation()
            .AddConsoleExporter();
            //.AddJaegerExporter(options =>
            //{
            //    options.AgentHost = "localhost";
            //    options.AgentPort = 6831;
            //});
    });

// --- Register Repositories ---
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAgentRepository, AgentRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

// --- Register Services ---
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAgentService, AgentService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ICommentService, CommentService>();
// =======================
// BUILD APP
// =======================
var app = builder.Build();

// =======================
// APPLY EF CORE MIGRATIONS AUTOMATICALLY
// =======================
//using (var scope = app.Services.CreateScope())
//    {
//    var db = scope.ServiceProvider.GetRequiredService<NassadContext>();
//    db.Database.Migrate(); // automatically applies pending migrations
//    }

// =======================
// MIDDLEWARE
// =======================

// Developer exception page
if (app.Environment.IsDevelopment())
    {
    app.UseDeveloperExceptionPage();
    }
else
    {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    }

// using just in development Enviroment 
if (app.Environment.IsDevelopment())
    {
    app.UseDeveloperExceptionPage();

    // Swagger ONLY in Development
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SupportApp API V1");
    });
    }
else
    {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    }
// use https
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
// Allow to access just from nassad.ca to access API
app.UseCors("AllowSupportApp");
//  Authentication must come before Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controllers and routes
app.MapControllers();
//  Protect Swagger endpoints
app.MapSwagger().RequireAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=index}/{id?}"
);

// Map SignalR hub
//app.MapHub<NotificationHub>("/notificationHub");
//app.MapHub<ChatHub>("/chathub");
app.Run();
public partial class Program { }