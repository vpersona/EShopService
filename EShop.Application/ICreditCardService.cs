namespace EShop.Application
{
    public interface ICreditCardService
    {
        bool ValidateCard(string cardNumber);
        string? GetCardType(string cardNumber);

        
        CreditCardValidationResult Validate(string cardNumber);
    }
}
