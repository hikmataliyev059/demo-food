using Google.Cloud.Dialogflow.V2;
using Grpc.Core;

namespace FoodStore.BL.Services.Implements.ChatBot;

public class DialogflowService
{
    private readonly SessionsClient _sessionsClient;

    public DialogflowService()
    {
        var clientBuilder = new SessionsClientBuilder
        {
            Endpoint = "dialogflow.googleapis.com:443",
            ChannelCredentials = ChannelCredentials.Insecure
        };

        _sessionsClient = clientBuilder.Build();
    }

    public async Task<string> SendMessageToDialogflowAsync(string userMessage)
    {
        var sessionName = SessionName.FromProjectSession("food-demo-sutx", Guid.NewGuid().ToString());

        var queryInput = new QueryInput
        {
            Text = new TextInput
            {
                Text = userMessage,
                LanguageCode = "az"
            }
        };

        var response = await _sessionsClient.DetectIntentAsync(sessionName, queryInput);
        return response.QueryResult.FulfillmentText;
    }
}