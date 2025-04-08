using Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;


namespace Tests.IntegrationTests
{
    public class BubbleTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;

        public BubbleTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task SendBubble_WhenCalled_Sends_Bubble_WhenInputValid()
        {
            //Arrange
            var bubble = new Bubble
            {
                Colour = "#ffffff",
                Background = null,
                Message = "Hello",
            };
            var jsonObj = JsonConvert.SerializeObject(bubble);
            var dataObj = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            //Act
            var response = await _httpClient.PostAsync($"/api/Bubble/send/testConnectionId", dataObj);

            //Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
