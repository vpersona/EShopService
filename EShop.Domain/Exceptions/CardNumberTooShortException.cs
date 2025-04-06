namespace EShop.Domain.Exceptions;
[Serializable]
public class CardNumberTooShortException : Exception
{
    public int StatusCode { get; } = 400;

    public CardNumberTooShortException()
        : base("Numer karty jest zbyt krótki.") { }

    public CardNumberTooShortException(string message)
        : base(message) { }

    public CardNumberTooShortException(string message, Exception inner)
        : base(message, inner) { }

    protected CardNumberTooShortException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}
