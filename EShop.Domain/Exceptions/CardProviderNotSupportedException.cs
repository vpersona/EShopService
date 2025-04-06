namespace EShop.Domain.Exceptions;

[Serializable]
public class CardProviderNotSupportedException : Exception
{
    public int StatusCode { get; } = 406;

    public CardProviderNotSupportedException()
        : base("Wydawca karty nie jest obsługiwany (obsługiwani: Visa, Mastercard, American Express).") { }

    public CardProviderNotSupportedException(string message)
        : base(message) { }

    public CardProviderNotSupportedException(string message, Exception inner)
        : base(message, inner) { }

    protected CardProviderNotSupportedException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}
