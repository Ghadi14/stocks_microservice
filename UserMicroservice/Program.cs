using PostgresqlHelper;
using UserMicroservice.RepositoryService;
using UserMicroservice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSingleton<IPostgresService, PostgresqlService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<UserService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
