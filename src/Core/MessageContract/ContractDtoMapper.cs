namespace MessageContract;

public static class ContractDtoMapper
{
    public static MultiCardArtDto ToMultiCardArtDto(this CardStatusChangeDto cardStatusChangeDto) => new MultiCardArtDto
        {
            CardNumber = string.IsNullOrWhiteSpace(cardStatusChangeDto.Card2) ? cardStatusChangeDto.Card1 : cardStatusChangeDto.Card2,
            BrandCode = cardStatusChangeDto.BrandCode!,
            ContextMetadata = cardStatusChangeDto.ContextMetadata
        };
}
