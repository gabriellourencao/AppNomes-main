using AppNomesBr.Domain.DataTransferObject.ExternalIntegrations.IBGE.Censos;
using AppNomesBr.Domain.Interfaces.Services;
using System.Text.Json;

namespace AppNomesBr.Pages;

public partial class RankingNomesBrasileiros : ContentPage
{
    private readonly INomesBrService service;
    private string? sexo;

    public RankingNomesBrasileiros(INomesBrService service)
    {
        InitializeComponent();
        this.service = service;
        this.BtnPesquisar.Clicked += BtnPesquisar_Clicked;
    }

    private async void BtnPesquisar_Clicked(object? sender, EventArgs e)
    {
        var cidade = TxtMunicipio.Text.ToLower();
        var sexo = this.sexo;

        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "https://servicodados.ibge.gov.br/api/v1/localidades/municipios/" + cidade);
        var response = await client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        // As duas linhas de código abaixo estão retornando o valor correto aparentemente
        var localidadesRoot = JsonSerializer.Deserialize<LocalidadeRoot>(result);
        await CarregarNomes(localidadesRoot?.id.ToString(), sexo);
       
    }

    protected override async void OnAppearing()
    {
        await CarregarNomes();
        base.OnAppearing();
    }

    private async Task CarregarNomes(string? cidade = null, string? sexo = null)
    {
        var result = await service.ListaTop20Nacional(cidade, sexo);
        this.GrdNomesBr.ItemsSource = result.FirstOrDefault()?.Resultado;
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var RadioButton = sender as Microsoft.Maui.Controls.RadioButton;
        var valor = RadioButton?.Content.ToString();
        if (valor == "Feminino")
            this.sexo = "F";
        else if (valor == "Masculino")
            this.sexo = "M";
    }

}
