using Application.Services;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = "User Id=postgres.uyrcskqwugdhlcypitfy;Password=!Np3mhxm74YzHRu%fYN2;Server=aws-0-us-east-2.pooler.supabase.com;Port=5432;Database=postgres";

builder.Services.AddInfrastructure(connectionString);

builder.Services.AddScoped<SupabaseSchemaInitializer>(_ =>
    new SupabaseSchemaInitializer(connectionString));

builder.Services.AddScoped<IUserRepository>(_ =>
    new UserRepository(connectionString));
builder.Services.AddScoped<IDeliveryRequestRepository>(_ =>
    new DeliveryRequestRepository(connectionString));

builder.Services.AddScoped<RegisterUserService>();
builder.Services.AddScoped<LoginMerchantService>();
builder.Services.AddScoped<RequestDeliveryService>();
builder.Services.AddScoped<GetDeliveryPeopleService>();
builder.Services.AddScoped<UpdateDeliveryStatusService>();
builder.Services.AddScoped<ViewAssignedDeliveriesService>();
builder.Services.AddScoped<ViewRequestedDeliveriesService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<SupabaseSchemaInitializer>();
    await initializer.InitializeAsync();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
