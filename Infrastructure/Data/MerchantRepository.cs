using Domain.Entities;
using Domain.Repositories;
using Npgsql;

namespace Infrastructure.Data;

public class MerchantRepository : IMerchantRepository
{
    private readonly string _connStr;

    public MerchantRepository(string connStr)
    {
        _connStr = connStr;
    }

    public async Task AddAsync(Merchant merchant)
    {
        await using var conn = new NpgsqlConnection(_connStr);
        await conn.OpenAsync();

        var sql = "INSERT INTO merchants (id, username, password_hash) VALUES (@id, @username, @password_hash)";
        await using var cmd = new NpgsqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("id", merchant.Id);
        cmd.Parameters.AddWithValue("username", merchant.Username);
        cmd.Parameters.AddWithValue("password_hash", merchant.PasswordHash);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<Merchant?> GetByUsernameAsync(string username)
    {
        await using var conn = new NpgsqlConnection(_connStr);
        await conn.OpenAsync();

        var sql = "SELECT id, username, password_hash FROM merchants WHERE username = @username";
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("username", username);

        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Merchant
            {
                Id = reader.GetGuid(0),
                Username = reader.GetString(1),
                PasswordHash = reader.GetString(2)
            };
        }

        return null;
    }

}
