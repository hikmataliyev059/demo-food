// using Microsoft.AspNetCore.Mvc;
// using OpenAiNg;
// using OpenAiNg.Completions;
// using OpenAiNg.Models;
//
// namespace FoodStore.API.Controllers;
//
// [ApiController]
// public class GptController : ControllerBase
// {
//     private readonly string _apiKey;
//
//     public GptController(IConfiguration configuration)
//     {
//         _apiKey = configuration.GetValue<string>("OpenAI:ApiKey");
//     }
//
//     [HttpGet]
//     [Route("UseChatGPT")]
//     public async Task<IActionResult> UseChatGpt(string query)
//     {
//         string outPutResult = "";
//         var openai = new OpenAiApi(_apiKey);
//         CompletionRequest completionRequest = new CompletionRequest
//         {
//             Prompt = query,
//             Model = Model.DavinciText
//         };
//
//         var completions = await openai.Completions.CreateCompletionAsync(completionRequest);
//
//         foreach (var completion in completions.Completions)
//         {
//             outPutResult += completion.Text;
//         }
//
//         return Ok(outPutResult);
//     }
// }