using System.Text.RegularExpressions;
using EShop.Domain.Enums;
using EShop.Domain.Exceptions;

namespace EShop.Application
{
    public class CreditCardValidationResult
    {
        public bool IsValid { get; set; }
        public CreditCardProvider Provider { get; set; }
    }

    public class CreditCardService : ICreditCardService
    {
        public bool ValidateCard(string cardNumber)
        {
            cardNumber = cardNumber.Replace(" ", "").Replace("-", "");

            if (cardNumber.Length < 13 || cardNumber.Length > 19 || !cardNumber.All(char.IsDigit))
                return false;

            int sum = 0;
            bool alternate = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int digit = cardNumber[i] - '0';
                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9) digit -= 9;
                }
                sum += digit;
                alternate = !alternate;
            }

            return (sum % 10 == 0);
        }

        public string? GetCardType(string cardNumber)
        {
            cardNumber = cardNumber.Replace(" ", "").Replace("-", "");

            if (Regex.IsMatch(cardNumber, @"^4(\d{12}|\d{15}|\d{18})$"))
                return "Visa";
            else if (Regex.IsMatch(cardNumber, @"^(5[1-5]\d{14}|2(2[2-9][1-9]|2[3-9]\d{2}|[3-6]\d{3}|7([01]\d{2}|20\d))\d{10})$"))
                return "MasterCard";
            else if (Regex.IsMatch(cardNumber, @"^3[47]\d{13}$"))
                return "American Express";
            else if (Regex.IsMatch(cardNumber, @"^(6011\d{12}|65\d{14}|64[4-9]\d{13}|622(1[2-9][6-9]|[2-8]\d{2}|9([01]\d|2[0-5]))\d{10})$"))
                return "Discover";
            else if (Regex.IsMatch(cardNumber, @"^(352[89]|35[3-8]\d)\d{12}$"))
                return "JCB";
            else if (Regex.IsMatch(cardNumber, @"^3(0[0-5]|[68]\d)\d{11}$"))
                return "Diners Club";
            else if (Regex.IsMatch(cardNumber, @"^(50|5[6-9]|6\d)\d{10,17}$"))
                return "Maestro";

            return null;
        }

        public CreditCardValidationResult Validate(string cardNumber)
        {
            string normalized = cardNumber.Replace(" ", "").Replace("-", "");

            if (normalized.Length > 19)
                throw new CardNumberTooLongException("Numer karty jest za długi.");

            if (normalized.Length < 13)
                throw new CardNumberTooShortException("Numer karty jest za krótki.");

            if (!normalized.All(char.IsDigit))
                throw new CardNumberInvalidException("Numer karty zawiera nieprawidłowe znaki.");

            if (!ValidateCard(normalized))
                throw new CardNumberInvalidException("Numer karty nie przechodzi walidacji Luhna.");

            string? type = GetCardType(normalized);

            if (type is null)
                throw new CardProviderNotSupportedException("Wydawca karty nie jest wspierany.");

            var provider = MapToProvider(type);

            return new CreditCardValidationResult
            {
                IsValid = true,
                Provider = provider
            };
        }

        private CreditCardProvider MapToProvider(string type)
        {
            return type switch
            {
                "Visa" => CreditCardProvider.Visa,
                "MasterCard" => CreditCardProvider.MasterCard,
                "American Express" => CreditCardProvider.AmericanExpress,
                _ => throw new CardProviderNotSupportedException("Wydawca karty nie jest wspierany.")
            };
        }
    }
}


