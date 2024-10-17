using AppNomesBr.Domain.Interfaces.ExternalIntegrations.IBGE.Censos;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;

namespace AppNomesBr.Infrastructure.ExternalIntegrations.IBGE.Censos
{
    public class NomesApi : INomesApi
    {
        private readonly string? baseUrl = "api/v2/censos/nomes/";
        private readonly string rankingEndpoint = "ranking";
        private readonly HttpClient httpClient;

        public NomesApi(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.rankingEndpoint = baseUrl + this.rankingEndpoint;
        }

        public async Task<string> RetornaCensosNomesRanking(string? cidade, string? sexo)
        {
            var consulta = rankingEndpoint;
            if (cidade != null)
            {
                consulta += "?localidade=" + cidade;
            }
            if (sexo != null)
            {
                if (cidade == null)
                    consulta += "?sexo=" + sexo;
                else
                    consulta += "&sexo=" + sexo;
            }

            var response = await httpClient.GetAsync(consulta);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> RetornaCensosNomesPeriodo(string nome)
        {
            var url = baseUrl + nome;
            var response = await httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
