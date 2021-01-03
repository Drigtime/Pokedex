using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using PokeApi;
using Pokedex;
using Xunit;

namespace ApiHelperXUnitTests
{
    public class ApiTests
    {
        /* Liens utiles :
         https://www.c-sharpcorner.com/article/mocking-httpclient-using-xunit-in-net-core/
         https://github.com/Moq/moq4/wiki/Quickstart
         https://dev.to/gautemeekolsen/how-to-test-httpclient-with-moq-in-c-2ldp
         https://gingter.org/2018/07/26/how-to-mock-httpclient-in-your-net-c-unit-tests/*/


/*      [Fact] are tests which are always true. They test invariant conditions.

        [Theory] are tests which are only true for a particular set of data.*/

        private readonly Uri uri = new Uri(AppMethods.BaseAddress, "api/v2/pokemon/ivysaur");

/*        [Fact]
        public async Task isItPokemonClassType_ReturnTrue()
        {
            // Arrange
            
            // Act
            var result = await AppMethods.GetPokemon(uri);

            // Assert
            Assert.IsType<PokeApi>(result);
            //Assert.IsType(PokeApi result, result);
        }

        [Fact]
        public async Task isItPokemonClassType_ReturnFalse()
        {
            // Arrange
            Uri uri = new Uri(AppMethods.BaseAddress, $"api/v2/pokemon/ivysaur");

            // Act
            var result = await AppMethods.GetPokemon(uri);

            // Assert
            Assert.IsNotType<PokemonAbility>(result);
            //Assert.IsType(PokeApi result, result);
        }*/

        [Fact]
        public async Task isItPokemonClassType_ReturnTrue_Moq()
        {
            var result = new Pokemon();
            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
                {
                    var response = new HttpResponseMessage();
                    response.StatusCode = HttpStatusCode.OK; //Setting statuscode    
                    response.Content =
                        new StringContent(JsonConvert.SerializeObject(result)); // configure your response here    
                    response.Content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/json"); //Setting media type for the response    
                    return response;
                })
                //option supplementaire
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            var appMethods = new AppMethods(httpClient);

            var pokemonResult = await appMethods.GetPokemon(uri);

            Assert.IsType<Pokemon>(pokemonResult);
            //Assert.NotNull(result);

            // options supplementaires
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task isItLocationAreaEncounterClassType_ReturnTrue_Moq()
        {
            var result = new LocationAreaEncounter();
            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
                {
                    var response = new HttpResponseMessage();
                    response.StatusCode = HttpStatusCode.OK; //Setting statuscode    
                    response.Content =
                        new StringContent(JsonConvert.SerializeObject(result)); // configure your response here    
                    response.Content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/json"); //Setting media type for the response    
                    return response;
                })
                //option supplementaire
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            var appMethods = new AppMethods(httpClient);

            var locationAreaEncounterResult = await appMethods.GetPokemonLocationAreaEncounters(uri);

            Assert.IsType<LocationAreaEncounter>(locationAreaEncounterResult);
            //Assert.NotNull(result);

            // options supplementaires
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task isItPokemonSpeciesClassType_ReturnTrue_Moq()
        {
            var result = new PokemonSpecies();
            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
                {
                    var response = new HttpResponseMessage();
                    response.StatusCode = HttpStatusCode.OK; //Setting statuscode    
                    response.Content =
                        new StringContent(JsonConvert.SerializeObject(result)); // configure your response here    
                    response.Content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/json"); //Setting media type for the response    
                    return response;
                })
                //option supplementaire
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            var appMethods = new AppMethods(httpClient);

            var pokemonSpeciesResult = await appMethods.GetPokemonSpecies(uri);

            Assert.IsType<PokemonSpecies>(pokemonSpeciesResult);
            //Assert.NotNull(result);

            // options supplementaires
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}