namespace MessageContract;

public record CardStatusChangeDto
{
    //Original
    public required string Card1 { get; init; }
    public required string Expiry1 { get; init; }
    public string? Card2 { get; init; }
    public string? Expiry2 { get; init; }
    public required string CardStatusCode { get; init; }
    public string? FromCardStatusCode { get; init; }

    //Add-on
    public string? BrandCode { get; init; }

    //Metadata
    public required ContextMetadata ContextMetadata { get; init; } 
}

public record MultiCardArtDto
{
    public required string CardNumber { get; init; }
    public required string BrandCode { get; init; }

    //Metadata
    public required ContextMetadata ContextMetadata { get; init; }
}

public record VisaCardExpiryUpdateDto
{
    public required string CardNumber { get; init; }
    public required string ExpiryOld { get; init; }
    public required string ExpiryNew { get; init; }
    public required string CorrelationId { get; init; }
}

public record VisaCardReplacementDto
{
    public required string CardNumberOld { get; init; }
    public required string ExpiryOld { get; init; }
    public required string CardNumberNew { get; init; }
    public required string ExpiryNew { get; init; }
    public required string CorrelationId { get; init; }
}