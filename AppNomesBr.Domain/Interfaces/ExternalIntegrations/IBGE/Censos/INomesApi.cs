namespace AppNomesBr.Domain.Interfaces.ExternalIntegrations.IBGE.Censos
{
    public interface INomesApi
    {
        Task<string> RetornaCensosNomesRanking(string? cidade = null, string? sexo = null);
        Task<string> RetornaCensosNomesPeriodo(string nome);
    }
}
