using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using InterceptorHelper;
using PostgresqlHelper;
using WebAPI.Services;
using static Greet.Greeter;
using static InventoryMicroservice.inventoryproto;
using static UserMicroservice.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IInventoryService, InventoryService>();
builder.Services.AddSingleton<IUserService, UserService>();

var inventoryChannel = GrpcChannel.ForAddress("https://localhost:7176");
var inventoryInvoker = inventoryChannel.Intercept(new ErrorHandlingInterceptor());


builder.Services.AddSingleton<IPostgresService, PostgresqlService>();
builder.Services.AddSingleton(serviceProvider =>
{
    return new GreeterClient(inventoryInvoker);
});

builder.Services.AddSingleton(serviceProvider =>
{
    return new inventoryprotoClient(inventoryInvoker);
});

var userChannel = GrpcChannel.ForAddress("https://localhost:7099");
var userInvoker = userChannel.Intercept(new ErrorHandlingInterceptor());

builder.Services.AddSingleton(serviceProvider =>
{
    return new UserClient(userInvoker);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
