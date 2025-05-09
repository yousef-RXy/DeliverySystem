using Domain.Entities;
using Domain.Repositories;
using Npgsql;

namespace Infrastructure.Data;

public class DeliveryPersonRepository : IDeliveryPersonRepository
{
    private readonly string _connStr;

    public DeliveryPersonRepository(string connStr)
    {
        _connStr = connStr;
    }

    public async Task AddAsync(DeliveryPerson person)
    {
        await using var conn = new NpgsqlConnection(_connStr);
        await conn.OpenAsync();

        var sql = "INSERT INTO delivery_persons (id, name) VALUES (@id, @name)";
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", person.Id);
        cmd.Parameters.AddWithValue("name", person.Name);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<DeliveryPerson?> GetByIdAsync(string username)
    {
        await using var conn = new NpgsqlConnection(_connStr);
        await conn.OpenAsync();

        var sql = "SELECT id, username, password_hash FROM merchants WHERE username = @username";
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("username", username);

        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new DeliveryPerson
            {
                Id = reader.GetGuid(0),
                Name = reader.GetString(1)
            };
        }

        return null;
    }

    public async Task<List<DeliveryPerson>> GetAllAsync()
    {
        var list = new List<DeliveryPerson>();

        await using var conn = new NpgsqlConnection(_connStr);
        await conn.OpenAsync();

        var sql = "SELECT id, name FROM delivery_persons";
        await using var cmd = new NpgsqlCommand(sql, conn);
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(new DeliveryPerson
            {
                Id = reader.GetGuid(0),
                Name = reader.GetString(1)
            });
        }

        return list;
    }
}
