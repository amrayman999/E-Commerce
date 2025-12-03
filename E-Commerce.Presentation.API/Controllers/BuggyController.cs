using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.API.Controllers
{
    public class BuggyController : APIBaseController
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFoundResponse()
        {
            return NotFound();
        }
        [HttpGet("badrequest")]
        public IActionResult GetBadRequestResponse()
        {
            return BadRequest();
        }
        [HttpGet("badrequest/{id}")]
        public IActionResult GetValidationErrorResponse(int id)
        {
            return BadRequest();
        }
        [HttpGet("servererror")]
        public IActionResult GetServerErrorResponse()
        {
            throw new Exception();
            return BadRequest();
        }
        [HttpGet("unauthorized")]
        public IActionResult GetUnAuthorizedResponse()
        {
            return Unauthorized();
        }
    }
}
