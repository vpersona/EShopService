namespace EShop.Domain.Exceptions;

[Serializable]



public class CardNumberInvalidException : Exception
{
    public int StatusCode { get; } = 400;

    public CardNumberInvalidException()
        : base("Numer karty jest niepoprawny (błędna suma kontrolna).") { }

    public CardNumberInvalidException(string message)
        : base(message) { }

    public CardNumberInvalidException(string message, Exception inner)
        : base(message, inner) { }

    protected CardNumberInvalidException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}
