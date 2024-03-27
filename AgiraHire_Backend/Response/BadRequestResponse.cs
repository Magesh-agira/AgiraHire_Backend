namespace AgiraHire_Backend.Response
{
    using Microsoft.AspNetCore.Mvc;

    namespace AgiraHire_Backend.Responses
    {
        public static class BadRequestResponse
        {
            public static IActionResult WithMessage(string message)
            {
                return new OkObjectResult(new { Message = message });
              
            }
        }
    }

}
    