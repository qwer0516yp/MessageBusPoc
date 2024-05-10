using MessageContract;

namespace CardStatusChangeKeyEnterPublisher;

public static class CardStatusCodeMapper
{
    public static string MapToCardStatusCode(this ConsoleKey key)
    {
        var result = string.Empty;

        switch (key)
        {
            case ConsoleKey.D0:
                result = CardStatus.AVAILABLE_FOR_USE;
                break;
            case ConsoleKey.D1:
                result = CardStatus.REFER_TO_ISSUER;
                break;
            case ConsoleKey.D2:
                result = CardStatus.ISSUED_INACTIVE;
                break;
            case ConsoleKey.D4:
                result = CardStatus.STOLEN_PICK_UP_CARD;
                break;
            case ConsoleKey.A:
                result = CardStatus.DEPOSITS_ONLY_WARM_CARD;
                break;
            case ConsoleKey.B:
                result = CardStatus.DELETED_CARD;
                break;
            case ConsoleKey.C:
                result = CardStatus.LOST_PICK_UP_CARD;
                break;
            case ConsoleKey.D:
                result = CardStatus.ADMINISTRANTION_CARD;
                break;
            case ConsoleKey.E:
                result = CardStatus.PICK_UP_CARD;
                break;
            case ConsoleKey.F:
                result = CardStatus.STATUS_UNCHANGED;
                break;
            default:
                break;
        }

        return result;
    }
}
