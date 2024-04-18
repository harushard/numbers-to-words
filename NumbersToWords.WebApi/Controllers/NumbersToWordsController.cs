using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using NumbersConverter;

namespace NumbersToWords.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ExcludeFromCodeCoverage]
    public class NumbersToWordsController : ControllerBase
    {
        private readonly IConverter _converter;

        public NumbersToWordsController(IConverter converter)
        {
            _converter = converter;
        }

        [HttpGet]
        public ActionResult Get(string input)
        {
            // input needed to be a string instead of decimal type
            // because converter supported up to 65 digits and decimal only supported up to 29 digits.
            try
            {
                var output = _converter.Convert(input);

                return new JsonResult(new
                {
                    success = true,
                    result = output
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }
    }
}