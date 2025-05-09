using Npgsql;

namespace Infrastructure.Data;

public class SupabaseSchemaInitializer
{
    private readonly string _connStr;

    public SupabaseSchemaInitializer(string connStr) => _connStr = connStr;

    public async Task InitializeAsync()
    {
        using var conn = new NpgsqlConnection(_connStr);
        await conn.OpenAsync();

        var sql = @"
        CREATE TABLE IF NOT EXISTS merchants (
            id UUID PRIMARY KEY,
            username TEXT NOT NULL UNIQUE,
            password_hash TEXT NOT NULL
        );

        CREATE TABLE IF NOT EXISTS delivery_requests (
            id UUID PRIMARY KEY,
            merchant_id UUID REFERENCES merchants(id),
            delivery_person_id UUID REFERENCES delivery_persons(id);
            package_size TEXT NOT NULL,
            weight DOUBLE PRECISION NOT NULL,
            address TEXT NOT NULL,
            status TEXT NOT NULL
        );

        CREATE TABLE IF NOT EXISTS delivery_persons (
            id UUID PRIMARY KEY,
            name TEXT NOT NULL
        );";

        try
        {
            using var cmd = new NpgsqlCommand(sql, conn);
            await cmd.ExecuteNonQueryAsync();
            Console.WriteLine("Tables initialized.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Schema initialization failed: " + ex.Message);
        }
    }
}
