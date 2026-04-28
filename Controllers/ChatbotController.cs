using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Portify.Controllers
{
    public class ChatbotController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        [HttpPost]
        public async Task<ActionResult> Ask(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return Json(new { success = false, response = "Please say something!" });
            }

            string apiKey = ConfigurationManager.AppSettings["GeminiApiKey"];
            string model = ConfigurationManager.AppSettings["GeminiModel"] ?? "gemini-1.5-flash";
            string supportEmail = ConfigurationManager.AppSettings["SupportEmail"] ?? "portify.support@gmail.com";

            string systemPrompt = $@"
You are the Portify Assistant, a friendly and helpful AI for Portify. 
Portify is a tool where people can make their own professional portfolios really fast by just picking a template and typing in their details.

Your Vibe:
- Use natural, everyday language. Don't be too formal or 'robotic'.
- Keep your answers short and simple so they are easy for everyone to understand.
- Be helpful and encouraging!

What Portify does:
- Pick a template and fill in your info (work, school, skills, etc.).
- Download your portfolio as a real website (HTML/CSS) when you're done.
- Login with Google or Email.

If you don't know something or it's a technical problem, just tell them to email us at: {supportEmail}.
";

            try
            {
                var requestBody = new
                {
                    contents = new[]
                    {
                        new { 
                            parts = new[] { new { text = systemPrompt + "\n\nUser Question: " + message } } 
                        }
                    }
                };

                string jsonRequest = JsonConvert.SerializeObject(requestBody);
                string url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}";

                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
                string responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    dynamic result = JsonConvert.DeserializeObject(responseString);
                    string aiResponse = result.candidates[0].content.parts[0].text;
                    return Json(new { success = true, response = aiResponse });
                }
                else
                {
                    // For debugging: Include the status code and response body
                    return Json(new { 
                        success = false, 
                        response = $"I'm having trouble connecting to my brain right now. (Status: {response.StatusCode})",
                        debug = responseString,
                        url = url
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, response = "An error occurred: " + ex.Message });
            }
        }
    }
}
