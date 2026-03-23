using MauiAPPMinhasCompras.Models;

namespace MauiAPPMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto produto_anexado = BindingContext as Produto;

            Produto p = new Produto
            {
                Id = produto_anexado.Id,
                Descricao = produto_anexado.Descricao,
                Quantidade = produto_anexado.Quantidade,
                Preco = produto_anexado.Preco
            };

            await App.Db.Update(p);
            await DisplayAlertAsync("Sucesso!", "Registro Atualizado", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops", ex.Message, "OK");
        }
    }
}