using MassTransit;
using MessageContract;

namespace CardStatusChangeConsumingService;

public class CardStatusChangeConsumer : IConsumer<CardStatusChangeDto>
{
    private readonly ILogger<CardStatusChangeConsumer> _logger;

    public CardStatusChangeConsumer(ILogger<CardStatusChangeConsumer> logger)
    {
        _logger = logger;
    }
     
    public async Task Consume(ConsumeContext<CardStatusChangeDto> context)
    {
        var message = context.Message;
        _logger.LogInformation("CardStatusChangeConsumer - consuming message: {message}", message);
        await ProcessMultiCardArtTriggerAsync(context, message);
        //await ProcessCardExpiryUpdateTriggerAsync(context, message);
        //await ProcessTokenLifeCycleTriggerAsync(context, message);
        //await ProcessCardReplacementTriggerAsync(context, message);
    }

    private async Task ProcessMultiCardArtTriggerAsync(ConsumeContext<CardStatusChangeDto> context, CardStatusChangeDto message)
    {
        if (!message.ContextMetadata.MultiCardArtEnabled || string.IsNullOrWhiteSpace(message.BrandCode))
        {
            return;
        }

        var multiCardArtRefreshMessage = message.ToMultiCardArtDto();
        _logger.LogInformation("MultiCardArt - send MultiCardArtDto message: {multiCardArtRefreshMessage}", multiCardArtRefreshMessage);

        await context.Publish(multiCardArtRefreshMessage);
        //await _bus.Publish<MultiCardArtDto>(multiCardArtRefreshMessage);
    }

    private async Task ProcessCardExpiryUpdateTriggerAsync(ConsumeContext<CardStatusChangeDto> context, CardStatusChangeDto message)
    {
        throw new NotImplementedException();
    }

    private async Task ProcessTokenLifeCycleTriggerAsync(ConsumeContext<CardStatusChangeDto> context, CardStatusChangeDto message)
    {
        throw new NotImplementedException();
    }

    private async Task ProcessCardReplacementTriggerAsync(ConsumeContext<CardStatusChangeDto> context, CardStatusChangeDto message)
    {
        throw new NotImplementedException();
    }
}
