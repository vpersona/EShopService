namespace EShop.Domain.Exceptions;
[Serializable]
public class CardNumberTooLongException : Exception
{
    public int StatusCode { get; } = 414;

    public CardNumberTooLongException()
        : base("Numer karty jest zbyt długi.") { }

    public CardNumberTooLongException(string message)
        : base(message) { }

    public CardNumberTooLongException(string message, Exception inner)
        : base(message, inner) { }

    protected CardNumberTooLongException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}
