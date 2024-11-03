using MessegnerBackend;
using MessegnerBackend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
var Configuration = builder.Configuration;

//Check if some parameters in configuration is valid
if(string.IsNullOrEmpty(Configuration["Jwt:Issuer"]) ||
    string.IsNullOrEmpty(Configuration["Jwt:Audience"]) ||
    string.IsNullOrEmpty(Configuration["Jwt:Key"])
    ){
    throw new ArgumentNullException("Some jwt params are null");
}



services.AddHttpContextAccessor();
services.AddDbContext<TiedDBContext>();
services.AddSingleton<IUserGetter, UserGetter>();
services.AddSingleton<ITokenGenerator, TokenGenerator>();

services.AddControllers();

services.AddDirectoryBrowser();



services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddMvcCore();


services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Configuration["Jwt:Issuer"],
            ValidAudience = Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
        };
    });
services.AddAuthorization();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "static")),
    RequestPath = "/images",
    EnableDirectoryBrowsing = true
});

app.UseCors(builder =>
    builder.AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod()
    );
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


app.Use(async (context, next) =>
{
    await next(context);
});

var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromSeconds(2)
};

app.UseWebSockets(webSocketOptions);

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});


app.Run();