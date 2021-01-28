using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using StudioGhibliAPI.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Test_
{
    public class API_Test : IClassFixture<WebApplicationFactory<StudioGhibliAPI.Startup>>
    {
        public HttpClient  _client { get; }

        public API_Test(WebApplicationFactory<StudioGhibliAPI.Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task  Get_Should_Return_API_Data()
        {
            var response = await _client.GetAsync("/api/Film");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var film = JsonConvert.DeserializeObject<Film[]>(await response.Content.ReadAsStringAsync());
            film.Should().HaveCount(20);
        }
    }
}
