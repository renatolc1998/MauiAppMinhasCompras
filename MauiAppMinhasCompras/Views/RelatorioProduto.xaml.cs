using MauiAppMinhasCompras.Helpers;
using System.Collections.Generic;

namespace MauiAppMinhasCompras.Views;

public partial class RelatorioProduto : ContentPage
{
	public RelatorioProduto()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarRelatorio();
    }
    private async Task CarregarRelatorio()
    {
        List<RelatorioCategoria> totais = await App.Db.GetTotalPorCategoria();

        foreach (var total in totais)
        {
            switch (total.Categoria)
            {
                case "Alimentos":
                    txt_alimentos.Text = total.TotalGasto.ToString("C");
                    break;
                case "Bebidas":
                    txt_bebidas.Text = total.TotalGasto.ToString("C");
                    break;
                case "Limpeza":
                    txt_limpeza.Text = total.TotalGasto.ToString("C");
                    break;
                case "Higiene Pessoal":
                    txt_higienepessoal.Text = total.TotalGasto.ToString("C");
                    break;
                case "Outros":
                    txt_outros.Text = total.TotalGasto.ToString("C");
                    break;
            }
        }
    }
    private void ToolbarItem_Voltar(object sender, EventArgs e)
    {
		Navigation.PopAsync();
    }
}