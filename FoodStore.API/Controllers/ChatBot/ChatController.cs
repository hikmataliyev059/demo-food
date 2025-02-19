// using Google.Cloud.Dialogflow.V2;
// using Microsoft.AspNetCore.Mvc;
//
// namespace FoodStore.API.Controllers.ChatBot;
//
// [Route("api/[controller]")]
// [ApiController]
// public class ChatController : ControllerBase
// {
//     private readonly SessionsClient _sessionsClient;
//     private readonly string _sessionId;
//
//     public ChatController()
//     {
//         _sessionsClient = SessionsClient.Create();
//         _sessionId = Guid.NewGuid().ToString();
//     }
//
//     [HttpPost]
//     public async Task<IActionResult> SendMessage([FromBody] UserMessage message)
//     {
//         var session = SessionName.FromProjectSession("food-demo-450914", _sessionId);
//
//         var queryInput = new QueryInput
//         {
//             Text = new TextInput
//             {
//                 Text = message.Text,
//                 LanguageCode = "en"
//             }
//         };
//
//         var response = await _sessionsClient.DetectIntentAsync(session, queryInput);
//
//         return Ok(new { reply = response.QueryResult.FulfillmentText });
//     }
// }
//
// public class UserMessage
// {
//     public string Text { get; set; }
// }