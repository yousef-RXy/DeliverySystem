using Domain.Entities;
using Domain.Repositories;
using Npgsql;

namespace Infrastructure.Data;
public class DeliveryRequestRepository : IDeliveryRequestRepository
{
    private readonly string _connStr;

    public DeliveryRequestRepository(string connStr)
    {
        _connStr = connStr;
    }

    public async Task AddAsync(DeliveryRequest request)
    {
        await using var conn = new NpgsqlConnection(_connStr);
        await conn.OpenAsync();

        var sql = @"
        INSERT INTO delivery_requests (id, title, merchant_id, delivery_person_id, package_size, weight, address, status)
        VALUES (@id, @title, @merchant_id, @delivery_person_id, @package_size, @weight, @address, @status)";

        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", request.Id);
        cmd.Parameters.AddWithValue("title", request.Title);
        cmd.Parameters.AddWithValue("merchant_id", request.MerchantId);
        cmd.Parameters.AddWithValue("delivery_person_id", request.DeliveryPersonId);
        cmd.Parameters.AddWithValue("package_size", request.PackageSize);
        cmd.Parameters.AddWithValue("weight", request.Weight);
        cmd.Parameters.AddWithValue("address", request.Address);
        cmd.Parameters.AddWithValue("status", request.Status.ToString());

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<List<DeliveryRequest>> GetAssignedAsync(Guid deliveryPersonId)
    {
        var requests = new List<DeliveryRequest>();

        await using var conn = new NpgsqlConnection(_connStr);
        await conn.OpenAsync();

        var sql = @"
        SELECT id, merchant_id, package_size, weight, address, status, delivery_person_id, title
        FROM delivery_requests
        WHERE delivery_person_id = @deliveryPersonId AND status != 'Delivered'";

        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("deliveryPersonId", deliveryPersonId);

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            requests.Add(new DeliveryRequest
            {
                Id = reader.GetGuid(0),
                MerchantId = reader.GetGuid(1),
                PackageSize = reader.GetDouble(2),
                Weight = reader.GetDouble(3),
                Address = reader.GetString(4),
                Status = Enum.Parse<DeliveryStatus>(reader.GetString(5)),
                DeliveryPersonId = reader.GetGuid(6),
                Title = reader.GetString(7)
            });
        }

        return requests;
    }
    public async Task<List<DeliveryRequest>> GetRequestedAsync(Guid MerchantId)
    {
        var requests = new List<DeliveryRequest>();

        await using var conn = new NpgsqlConnection(_connStr);
        await conn.OpenAsync();

        var sql = @"
        SELECT id, merchant_id, package_size, weight, address, status, delivery_person_id, title
        FROM delivery_requests
        WHERE merchant_id = @merchant_id";

        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("merchant_id", MerchantId);

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            requests.Add(new DeliveryRequest
            {
                Id = reader.GetGuid(0),
                MerchantId = reader.GetGuid(1),
                PackageSize = reader.GetDouble(2),
                Weight = reader.GetDouble(3),
                Address = reader.GetString(4),
                Status = Enum.Parse<DeliveryStatus>(reader.GetString(5)),
                DeliveryPersonId = reader.GetGuid(6),
                Title = reader.GetString(7)
            });
        }

        return requests;
    }


    public async Task UpdateStatusAsync(Guid deliveryId, DeliveryStatus status)
    {
        await using var conn = new NpgsqlConnection(_connStr);
        await conn.OpenAsync();

        var sql = "UPDATE delivery_requests SET status = @status WHERE id = @id";
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("status", status.ToString());
        cmd.Parameters.AddWithValue("id", deliveryId);

        await cmd.ExecuteNonQueryAsync();
    }

}