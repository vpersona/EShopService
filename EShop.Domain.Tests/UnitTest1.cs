using System;
using Xunit;
using EShop.Application; // jeœli walidacja kart jest w Application
using EShop.Domain.Enums;
using EShop.Domain.Exceptions;

namespace EShop.Domain.Tests
{
    public class CreditCardDomainTests
    {
        private readonly CreditCardService _service = new CreditCardService();

        [Theory]
        [InlineData("4111 1111 1111 1111", CreditCardProvider.Visa)]
        [InlineData("5500 0000 0000 0004", CreditCardProvider.MasterCard)]
        [InlineData("3400 000000 00009", CreditCardProvider.AmericanExpress)]
        public void Validate_ValidCard_ReturnsExpectedProvider(string cardNumber, CreditCardProvider expected)
        {
            var result = _service.Validate(cardNumber);

            Assert.True(result.IsValid);
            Assert.Equal(expected, result.Provider);
        }

        [Theory]
        [InlineData("1234 5678 9012 3456 7890 1234", typeof(CardNumberTooLongException))]
        [InlineData("4111", typeof(CardNumberTooShortException))]
        [InlineData("1234 5678 9012 3456", typeof(CardNumberInvalidException))]
        public void Validate_InvalidCard_ThrowsExpectedDomainException(string cardNumber, Type expectedException)
        {
            var ex = Assert.Throws(expectedException, () => _service.Validate(cardNumber));
            Assert.IsType(expectedException, ex);
        }

        [Fact]
        public void Validate_UnsupportedProvider_ThrowsNotSupportedException()
        {
            // Maestro card - unsupported in business logic
            var cardNumber = "6759649826438453";

            var ex = Assert.Throws<NotSupportedException>(() => _service.Validate(cardNumber));
            Assert.Equal("Wydawca karty nie jest wspierany.", ex.Message);
        }
    }
}
