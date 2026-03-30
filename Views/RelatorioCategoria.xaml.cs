namespace MauiAPPMinhasCompras.Views;

public partial class RelatorioCategoria : ContentPage
{
	public RelatorioCategoria()
	{
		InitializeComponent();
        CarregarRelatorio();
    }


    private async void CarregarRelatorio()
    {
        var produtos = await App.Db.GetAll();

        var totalPorCategoria = produtos
            .GroupBy(p => p.Categoria)
            .Select(g => new
            {
                Categoria = g.Key,
                TotalGasto = g.Sum(p => p.Total)
            })
            .ToList();

        lst_relatorio.ItemsSource = totalPorCategoria;
    }
}