using Domain.DTO;
using Domain.Entities;
using Domain.Repositories;
using Npgsql;

namespace Infrastructure.Data;

public class UserRepository : IUserRepository
{
    private readonly string _connStr;

    public UserRepository(string connStr)
    {
        _connStr = connStr;
    }

    public async Task AddAsync(User merchant)
    {
        await using var conn = new NpgsqlConnection(_connStr);
        await conn.OpenAsync();

        var sql = @"
        INSERT INTO users (id, username, password_hash, role)
        VALUES (@id, @username, @password_hash, @role)";

        await using var cmd = new NpgsqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("id", merchant.Id);
        cmd.Parameters.AddWithValue("username", merchant.Username);
        cmd.Parameters.AddWithValue("password_hash", merchant.PasswordHash);
        cmd.Parameters.AddWithValue("role", merchant.Role);

        await cmd.ExecuteNonQueryAsync();
    }


    public async Task<User?> GetByUsernameAsync(string username)
    {
        await using var conn = new NpgsqlConnection(_connStr);
        await conn.OpenAsync();

        var sql = "SELECT id, username, password_hash, role FROM Users WHERE username = @username";
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("username", username);

        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new User
            {
                Id = reader.GetGuid(0),
                Username = reader.GetString(1),
                PasswordHash = reader.GetString(2),
                Role = reader.GetString(3)
            };
        }

        return null;
    }

    public async Task<List<DeliveryPersonDto>> GetDeliveryPeople()
    {
        var deliveryPeople = new List<DeliveryPersonDto>();

        await using var conn = new NpgsqlConnection(_connStr);
        await conn.OpenAsync();

        var sql = @"
        SELECT id, username, password_hash, role
        FROM users
        WHERE role = 'DeliveryPerson'";

        await using var cmd = new NpgsqlCommand(sql, conn);

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var deliveryPerson = new DeliveryPersonDto
            {
                Id = reader.GetGuid(0),
                Username = reader.GetString(1),
            };

            deliveryPeople.Add(deliveryPerson);
        }

        return deliveryPeople;
    }
}
