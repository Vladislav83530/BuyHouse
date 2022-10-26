using BuyHouse.BLL.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BuyHouse.BLL.Clients
{
    public class CurrencyConverterClient
    {
        private readonly string _apiKey;
        private readonly string _apiAdress;
        private readonly string _apiHost;
        public CurrencyConverterClient(IConfiguration config)
        {
            _apiAdress = config["CurrencyConverterApiAdress"];
            _apiKey = config["CurrencyConverterApiKey"];
            _apiHost = config["CurrencyConverterApiHost"];
        }

        /// <summary>
        /// Convert currency:
        /// USD -> UAH
        /// USD -> Euro
        /// UAH -> USD
        /// UAH -> Euro
        /// Euro -> USD
        /// Euro -> UAH
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="amount"></param>
        /// <returns>Convert amount</returns>
        public async Task<ulong> ConvertCurrecyAsync(string from, string to, ulong amount)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_apiAdress}/convert?from={from}&to={to}&amount={amount}"),
                Headers =
                {
                    { "X-RapidAPI-Key", _apiKey },
                    { "X-RapidAPI-Host", _apiHost},
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CurrencyConvertDTO>(body);
                return (ulong)result.Result;
            }
        }
    }
}
