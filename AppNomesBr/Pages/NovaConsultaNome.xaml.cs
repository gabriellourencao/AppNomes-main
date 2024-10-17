using AppNomesBr.Domain.Interfaces.Repositories;
using AppNomesBr.Domain.Interfaces.Services;

namespace AppNomesBr.Pages;

public partial class NovaConsultaNome : ContentPage
{
    private readonly INomesBrService service;
    private readonly INomesBrRepository repository;
    private string? sexo;

    public NovaConsultaNome(INomesBrService service, INomesBrRepository repository)
    {
        InitializeComponent();
        this.service = service;
        this.repository = repository;
        BtnPesquisar.Clicked += BtnPesquisar_Clicked;
        BtnDeleteAll.Clicked += BtnDeleteAll_Clicked;
    }

    private async void BtnDeleteAll_Clicked(object? sender, EventArgs e)
    {
        var registros = await repository.GetAll();

        foreach (var registro in registros)
            await repository.Delete(registro.Id);

        await CarregarNomes();
    }

    protected override async void OnAppearing()
    {
        await CarregarNomes();
        base.OnAppearing();
    }

    private async void BtnPesquisar_Clicked(object? sender, EventArgs e)
    {
        await service.InserirNovoRegistroNoRanking(TxtNome.Text.ToUpper(), sexo);
        await CarregarNomes();
    }

    private async Task CarregarNomes()
    {
        var result = await service.ListaMeuRanking();
        this.GrdNomesBr.ItemsSource = result.FirstOrDefault()?.Resultado;
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var RadioButton = sender as Microsoft.Maui.Controls.RadioButton;
        var valor = RadioButton?.Content.ToString();
        var sexo = this.sexo;
        if (valor == "Feminino")
            this.sexo = "F";
        else if (valor == "Masculino")
            this.sexo = "M";
    }
}