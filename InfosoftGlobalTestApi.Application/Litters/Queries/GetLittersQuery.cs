using Dapper;
using InfosoftGlobalTestApi.Application.Common;
using InfosoftGlobalTestApi.Application.Litters.Dto_s;
using MediatR;
using Microsoft.Data.Sqlite; // 🔥 Офіційний провайдер для SQLite
using Microsoft.Extensions.Configuration;
using System.Data;

namespace InfosoftGlobalTestApi.Application.Litters.Queries;

public record GetLittersQuery(
    Guid BreederId,
    string? Status,
    int PageNumber,
    int PageSize
) : IRequest<PaginatedListDto<LitterReadDto>>;

public class GetLittersQueryHandler : IRequestHandler<GetLittersQuery, PaginatedListDto<LitterReadDto>>
{
    private readonly string _connectionString;

    public GetLittersQueryHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string is missing.");
    }

    public async Task<PaginatedListDto<LitterReadDto>> Handle(GetLittersQuery request, CancellationToken cancellationToken = default)
    {
        using var connection = new SqliteConnection(_connectionString);

        var whereConditions = new List<string>();
        var parameters = new DynamicParameters();

        if (request.BreederId != Guid.Empty)
        {
            whereConditions.Add("UPPER(BreederId) = @BreederId");
            parameters.Add("BreederId", request.BreederId.ToString().ToUpper());
        }

        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            whereConditions.Add("Status = @Status");
            parameters.Add("Status", request.Status.Trim());
        }

        var whereClause = whereConditions.Any()
            ? "WHERE " + string.Join(" AND ", whereConditions)
            : "";

        var offset = (request.PageNumber - 1) * request.PageSize;
        parameters.Add("Limit", request.PageSize);
        parameters.Add("Offset", offset);

        var sql = $@"
        SELECT Id, BreederId, Status, CreatedAt 
        FROM Litters 
        {whereClause} 
        ORDER BY CreatedAt DESC 
        LIMIT @Limit OFFSET @Offset;

        SELECT COUNT(*) 
        FROM Litters 
        {whereClause};";

        var command = new CommandDefinition(
            sql,
            parameters,
            cancellationToken: cancellationToken
        );

        await connection.OpenAsync(cancellationToken);
        using var multi = await connection.QueryMultipleAsync(command);

        var items = (await multi.ReadAsync<LitterReadDto>()).ToList();
        var totalCount = await multi.ReadFirstAsync<int>();

        return new PaginatedListDto<LitterReadDto>(
            items,
            totalCount,
            request.PageNumber,
            request.PageSize
        );
    }
}