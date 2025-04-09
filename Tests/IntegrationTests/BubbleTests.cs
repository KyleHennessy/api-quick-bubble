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

        [Theory]
        [InlineData("Test", "#ffffff", null)]
        [InlineData("Test", "#aaa", null)]
        [InlineData("Test", "#ffffff", "png")]
        [InlineData("Test", "#ffffff", "jpg")]
        public async Task SendBubble_WhenCalled_Sends_Bubble_WhenInputValid(string message, string colour, string? fileExtension)
        {
            string? background = null;

            if(fileExtension != null)
            {
                background = LoadFile($"test.{fileExtension}");
            }
            //Arrange
            var bubble = new Bubble
            {
                Message = message,
                Colour = colour,
                Background = background,
            };
            var jsonObj = JsonConvert.SerializeObject(bubble);
            var dataObj = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            //Act
            var response = await _httpClient.PostAsync($"/api/Bubble/send/testConnectionId", dataObj);
            var responseObj = JsonConvert.DeserializeObject<Bubble>(await response.Content.ReadAsStringAsync());

            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(responseObj);
            Assert.Equal(bubble.Message, responseObj.Message);
            Assert.NotNull(responseObj.Id.ToString());
        }

        [Theory]
        [InlineData("dummy")]
        [InlineData("ddummyy")]
        [InlineData("a dummy")]
        public async Task SendBubble_Will_Censor_Profanity(string message)
        {
            //Arrange
            var bubble = new Bubble
            {
                Message = message,
                Colour = "#ffffff",
                Background = null,
            };
            var jsonObj = JsonConvert.SerializeObject(bubble);
            var dataObj = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            //Act
            var response = await _httpClient.PostAsync($"/api/Bubble/send/testConnectionId", dataObj);
            var responseObj = JsonConvert.DeserializeObject<Bubble>(await response.Content.ReadAsStringAsync());

            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(responseObj);
            Assert.Equal(bubble.Message, responseObj.Message);
            Assert.True(responseObj.Message.Contains('*'));
        }

        [Theory]
        [InlineData(null, "#ffffff", null)]
        [InlineData("Test", null, null)]
        [InlineData("Test", "not a colour", null)]
        [InlineData("Test", "#ffffff", "txt")]
        [InlineData("Test", "#ffffff", "pdf")]
        public async Task SendBubble_WhenCalled_Returns_BadRequest_WhenInputInvalid(string? message, string? colour, string? fileExtension)
        {
            string? background = null;

            if (fileExtension != null)
            {
                background = LoadFile($"test.{fileExtension}");
            }
            var bubble = new Bubble
            {
                Message = message!,
                Colour = colour!,
                Background = background
            };

            var jsonObj = JsonConvert.SerializeObject(bubble);
            var dataObj = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            //Act
            var response = await _httpClient.PostAsync($"/api/Bubble/send/testConnectionId", dataObj);
            var responseObj = JsonConvert.DeserializeObject<Bubble>(await response.Content.ReadAsStringAsync());

            //Assert
            Assert.False(response.IsSuccessStatusCode);
        }

        private static string LoadFile(string filename)
        {
            var dir = Directory.GetCurrentDirectory();

            var path = Path.Combine(dir, "TestData", filename);

            var fileBytes = File.ReadAllBytes(path);

            return Convert.ToBase64String(fileBytes);
        }
    }
}
