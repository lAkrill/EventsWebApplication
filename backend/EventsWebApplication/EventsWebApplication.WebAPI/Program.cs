using EventsWebApplication.Application.Interfaces;
using EventsWebApplication.Application.Mapping;
using EventsWebApplication.Application.Services;
using EventsWebApplication.DataAccess;
using EventsWebApplication.DataAccess.Repositories;
using EventsWebApplication.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();

builder.Services.AddAutoMapper(typeof(CategoryProfile), 
    typeof(EventProfile),
    typeof(ParticipantProfile),
    typeof(UserProfile));

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<ParticipantService>();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<FileStorageSettings>(builder.Configuration.GetSection("FileStorage"));

builder.Services.AddScoped<IFileStorageService>(provider =>
{
    var settings = provider.GetRequiredService<IOptions<FileStorageSettings>>().Value;
    var environment = provider.GetRequiredService<IWebHostEnvironment>();

    var basePath = Path.Combine(environment.ContentRootPath, settings.BasePath);
    return new LocalFileStorageService(basePath);
});

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = "/uploads"
});

app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
