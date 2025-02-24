using FoodStore.BL.Services.Implements.ChatBot;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.API.Controllers.ChatBot;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly DialogflowService _dialogflowService;

    public ChatController(DialogflowService dialogflowService)
    {
        _dialogflowService = dialogflowService;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] UserMessage message)
    {
        var reply = await _dialogflowService.SendMessageToDialogflowAsync(message.Text);
        return Ok(new { reply });
    }
}

public class UserMessage
{
    public string Text { get; set; }
}