namespace Sellify.Application.Global
{
    //BASIC
    public record GenericResultDTO(
        dynamic data,
        int statusCode,
        Dictionary<string, HashSet<string>>? errorsKeyValues = null
    );

    //GENERIC   
    public record GenericResultDTO<DTOType>(
        DTOType data,
        int statusCode,
        Dictionary<string,
            HashSet<string>>? errorsKeyValues = null
        );

}
