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

builder.Services.AddScoped<IDeliveryPersonRepository>(_ =>
    new DeliveryPersonRepository(connectionString));
builder.Services.AddScoped<IMerchantRepository>(_ =>
    new MerchantRepository(connectionString));
builder.Services.AddScoped<IDeliveryRequestRepository>(_ =>
    new DeliveryRequestRepository(connectionString));

builder.Services.AddScoped<RegisterMerchantService>();
builder.Services.AddScoped<LoginMerchantService>();
builder.Services.AddScoped<RequestDeliveryService>();
builder.Services.AddScoped<UpdateDeliveryStatusService>();
builder.Services.AddScoped<ViewAssignedDeliveriesService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
