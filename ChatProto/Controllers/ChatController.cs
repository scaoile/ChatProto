using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ChatProto.Services;

namespace YourNamespace.Controllers
{
    public class ChatController : Controller
    {
        private readonly GeminiClient _geminiClient;

        public ChatController(GeminiClient geminiClient)
        {
            _geminiClient = geminiClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return BadRequest("Message cannot be empty.");
            }

            string systemInstructions = "You are a career assistant or guide that helps students assess their skills and their capabilities to assist them in what career they might want to pursue";
            string prompt = $"{systemInstructions}\nUser: {message}\nAssistant:";

            string responseText = await _geminiClient.GenerateContentAsync(prompt, HttpContext.RequestAborted);

            return Json(responseText); // Return the assistant's response as JSON
        }
    }
}

