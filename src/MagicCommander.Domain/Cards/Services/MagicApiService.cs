using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using MagicCommander.Domain._Shared.Exceptions;
using MagicCommander.Domain.Cards.Entities;

namespace MagicCommander.Domain.Cards.Services;

public interface IApiMagicService
{
    Task<Card?> GetCardByExternalId(string externalId);
}

public class ApiMagicService : IApiMagicService
{
    private readonly HttpClient _httpClient;

    public ApiMagicService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Card?> GetCardByExternalId(string externalId)
    {
        var response = await _httpClient.GetAsync($"v1/cards/{externalId}");

        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var responseValue = JsonSerializer.Deserialize<MagicApiCardResponseDto>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
            // return responseValue.Card;
        }
        else if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new MagicApiResourceNotFoundException($"The selected card with id {externalId} doesn't exists");
        }

        return null;
    }
}
