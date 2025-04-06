using Microsoft.AspNetCore.Mvc;
using EShop.Application;
using EShop.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace EShop.Application.Controllers
{

    [ApiController] //obsluga api, automatyczna walidacja modelu
    [Route("api/[controller]")]//koncowy adres --> POST /api/creditcard/validate
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardService _creditCardService;
        //wstrzykiwanie zaleznosci
        public CreditCardController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        [HttpPost("validate")] // opowiada na żądanie post na endpoint /api/creditcard/validate
        public IActionResult ValidateCard([FromBody] CreditCardRequest request) //przyjecie numeru karty w zadaniu http  i zwrocenie jako string 
        {
            try
            {
                var result = _creditCardService.Validate(request.CardNumber);

                return Ok(new
                {
                    Status = "Valid",
                    Provider = result.Provider.ToString()
                });
            }
            catch (CardNumberTooLongException ex)
            {
                return StatusCode(StatusCodes.Status414UriTooLong, new { Error = ex.Message });
            }
            catch (CardNumberTooShortException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (CardNumberInvalidException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (CardProviderNotSupportedException ex)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, new { Error = ex.Message });//zwraca http i wiadomosc bledu w JSON
            }
        }
    }

    public class CreditCardRequest
    {
        public string CardNumber { get; set; } = string.Empty;
    }
}
