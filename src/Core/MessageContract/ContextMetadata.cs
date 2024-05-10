namespace MessageContract;

public record ContextMetadata
{
    public required string CorrelationId { get; init; }
    public required string OriginatorInfo { get; init; }
    public required string ClientReference { get; init; }
    public required string BinReference { get; init; }
    public bool MultiCardArtEnabled { get; init; }
    public bool LostOrStolenEnabled { get; init; }
    public bool InstantIssuanceEnabled { get; init; }
    public bool HasVisa { get; init; }
    public bool HasEftpos { get; init; }
}