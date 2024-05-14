using MessageContract;

namespace CardStatusChangeKeyEnterPublisher;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IBus _bus;

    public Worker(ILogger<Worker> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Yield();

            var keyPressed = Console.ReadKey(true);
            if (keyPressed.Key == ConsoleKey.Escape)
            {
                return;
            }

            var button = keyPressed.Key.ToString();
            _logger.LogInformation($"{button} Pressed from CardStatusChangeKeyPressPublisher at {DateTimeOffset.Now}...");
            var message = GetMockMessage(keyPressed.Key);

            if (!string.IsNullOrWhiteSpace(message?.CardStatusCode))
            {
                await _bus.Publish(message, stoppingToken);
                _logger.LogInformation("Published CardStatusChange.{message}", message);

                await Task.Delay(10, stoppingToken);
            }
            else
            {
                _logger.LogInformation("Unmapped key press, no message published.");
            }
        }
    }

    private CardStatusChangeDto? GetMockMessage(ConsoleKey key)
    {
        var cardStatusCode = key.MapToCardStatusCode();
        if (string.IsNullOrWhiteSpace(cardStatusCode))
        {
            return null;
        }

        return new CardStatusChangeDto
        {
            Card1 = "1234567890123456",
            Expiry1 = "05/27",
            CardStatusCode = cardStatusCode,
            BrandCode = "B2",
            ContextMetadata = new ContextMetadata
            {
                CorrelationId = Guid.NewGuid().ToString(),
                OriginatorInfo = "CardStatusChangeKeyPressPublisher",
                ClientReference = "C00001",
                BinReference = "123456",
                MultiCardArtEnabled = true,
                LostOrStolenEnabled = false,
                InstantIssuanceEnabled = false
            }
        };
    }
}
