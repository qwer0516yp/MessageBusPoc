namespace MessageContract;

public static class CardStatus
{
    public static readonly string AVAILABLE_FOR_USE = "00";
    public static readonly string REFER_TO_ISSUER = "01";
    public static readonly string ISSUED_INACTIVE = "02";
    public static readonly string STOLEN_PICK_UP_CARD = "04";
    public static readonly string DEPOSITS_ONLY_WARM_CARD = "1A";
    public static readonly string DELETED_CARD = "1B";
    public static readonly string LOST_PICK_UP_CARD = "1C";
    public static readonly string ADMINISTRANTION_CARD = "1D";
    public static readonly string PICK_UP_CARD = "1E";
    public static readonly string STATUS_UNCHANGED = "1F";
}
