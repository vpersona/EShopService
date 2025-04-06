using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using EShop.Application;

namespace EShop.Application.Tests
{
    public class CreditCardServiceTests
    {
        private readonly CreditCardService _service = new CreditCardService();

        [Theory]
        [InlineData("4111 1111 1111 1111", true)]
        [InlineData("5500-0000-0000-0004", true)]
        [InlineData("3400 000000 00009", true)]
        [InlineData("1234 5678 9012 3456", false)]
        [InlineData("4111-1111-1111", false)]

        public void ValidateCard_ReturnsExpectedResult(string cardNumber, bool expected)
        {
            var result = _service.ValidateCard(cardNumber);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("4111111111111111", "Visa")]
        [InlineData("5500000000000004", "MasterCard")]
        [InlineData("340000000000009", "American Express")]
        [InlineData("6011000000000004", "Discover")]
        [InlineData("3528000000000000", "JCB")]
        [InlineData("30000000000004", "Diners Club")]
        [InlineData("6759649826438453", "Maestro")]
        [InlineData("9999999999999999", null)]

        public void GetCardType_ReturnCorrectType(string cardNumber,string? expectedType)
        {
            var result = _service.GetCardType(cardNumber);
            Assert.Equal(expectedType, result); 
        }
    }
}
