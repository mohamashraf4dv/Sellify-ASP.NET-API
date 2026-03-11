namespace Sellify.Application.Global
{
    public record GenericResultDTO(
        dynamic data,
        int statusCode,
        Dictionary<string, HashSet<string>>? errorsKeyValues = null
    );

}
