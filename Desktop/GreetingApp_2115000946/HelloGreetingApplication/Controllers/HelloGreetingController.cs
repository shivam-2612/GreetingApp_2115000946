using BusinessLayer.Interface;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;

namespace HelloGreetingApplication.Controllers
{
    /// <summary>
    /// Class providing API for HelloGreeting
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HelloGreetingController : ControllerBase
    {
        private static Dictionary<string, string> greetings = new Dictionary<string, string>();
        private readonly IGreetingBL _greetingBL;

        public HelloGreetingController(IGreetingBL greetingBL)
        {
            _greetingBL = greetingBL;
        }

        /// <summary>
        /// Get method to get the greeting message
        /// </summary>
        /// <returns> "Hello World" </returns>
        [HttpGet]
        public IActionResult GetMethod() 
        {
            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Hello to Greeting App Api endpoint.";
            responseModel.Data = "Hello World";
            return Ok(responseModel);
        }

        /// <summary>
        /// Post method to get the greeting message
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns> response model</returns>
        [HttpPost]
        public IActionResult Post(RequestModel requestModel) {
            if (requestModel == null || string.IsNullOrWhiteSpace(requestModel.Key) || string.IsNullOrWhiteSpace(requestModel.Value))
            {
                return BadRequest(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Key and Value are required.",
                    Data = null
                });
            }

            if (greetings.ContainsKey(requestModel.Key))
            {
                return Conflict(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Key already exists. Use a different key.",
                    Data = null
                });
            }

            greetings[requestModel.Key] = requestModel.Value;
            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting added successfully.",
                Data = $"Key: {requestModel.Key}, Value: {requestModel.Value}"
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="newValue"></param>
        /// <returns>Updated greeting message</returns>
        [HttpPut("IDKEY")]
        public IActionResult Put(string key, string newValue) 
        {
            if (!greetings.ContainsKey(key))
            {
                return NotFound(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Greeting not found.",
                    Data = null
                });
            }

            greetings[key] = newValue;
            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting updated successfully.",
                Data = $"Key: {key}, Value: {newValue}"
            });
        
        }

        /// <summary>
        /// Delete method to remove a greeting message
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Deleted greeting message</returns>
        [HttpDelete]
        public IActionResult Delete(string key) 
        {
            if (!greetings.ContainsKey(key))
            {
                return NotFound(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Greeting not found.",
                    Data = null
                });
            }

            string removedGreeting = greetings[key];
            greetings.Remove(key);
            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting deleted successfully.",
                Data = $"Key: {key}, Value: {removedGreeting}"
            });
        }

        [HttpPatch("{key}")]
        public IActionResult Patch(string key, string partialUpdate) 
        {
            if (!greetings.ContainsKey(key))
            {
                return NotFound(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Greeting not found.",
                    Data = null
                });
            }

            greetings[key] += " " + partialUpdate;
            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting modified successfully.",
                Data = $"Key: {key}, Value: {greetings[key]}"
            });
        }

        [HttpGet("Greet")]
        public IActionResult GetGreeting()
        {
            var greetingMessage = _greetingBL.GetGreetingMessage();
            var response = new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting retrieved successfully.",
                Data = greetingMessage
            };

            return Ok(response);
        }


        [HttpPost("getfirstname")]
        public IActionResult GetGreeting([FromQuery] string firstName = null, [FromQuery] string lastName = null)
        {
            string message = _greetingBL.GetGreetingMessage(firstName, lastName);
            return Ok(new { Greeting = message });
        }


        [HttpPost("addgreet")]
        public IActionResult AddGreeting([FromQuery] string firstName, [FromQuery] string lastName)
        {
            string message = _greetingBL.GetGreetingMessage(firstName, lastName);

            // Create response with additional details
            var response = new
            {
                FirstName = firstName,
                LastName = lastName,
                Message = message,
                CreatedAt = DateTime.Now  // Set current timestamp
            };

            return Ok(response);
        }


        [HttpGet("{id}")]
        public IActionResult GetGreetingById(int id)
        {
            var greeting = _greetingBL.GetGreetingById(id);
            if (greeting == null)
                return NotFound(new { Message = "Greeting not found!" });

            return Ok(greeting);
        }


        [HttpGet("all")]
        public IActionResult GetAllGreetings()
        {
            List<GreetingEntity> greetings = _greetingBL.GetAllGreetings();

            if (greetings == null || greetings.Count == 0)
                return NotFound(new { Message = "No greetings found!" });

            return Ok(greetings);
        }

        [HttpPut("update")]
        public IActionResult UpdateGreeting([FromBody] GreetingEntity updatedGreeting)
        {
            var greeting = _greetingBL.UpdateGreeting(updatedGreeting);
            if (greeting == null)
                return NotFound(new { Message = "Greeting not found!" });

            return Ok(greeting);
        }



    }
}
