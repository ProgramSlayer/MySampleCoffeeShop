using CoffeeShop.Services.Coffees;
using CoffeeShop.Web.Server.Extensions;
using CoffeeShop.Web.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddCors("CorsPolicy")
    .AddDatabase(builder.Configuration)
    .AddIdentity()
    .AddJwtAuthentication(
        builder.Services.GetJwtSettings(
            builder.Configuration))
    .AddHttpContextAccessor()
    .AddScoped<ICurrentUserService, CurrentUserService>()
    .AddApplicationServices()
    .AddControllers();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app
        .UseDeveloperExceptionPage()
        .UseSwagger()
        .UseSwaggerUI();
}

app
    .UseHttpsRedirection()
    .UseCors("CorsPolicy")
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();

app.Initialize();

app.Run();

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
