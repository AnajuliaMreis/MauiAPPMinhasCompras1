using MauiAPPMinhasCompras.Models;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using System.Collections.ObjectModel;

namespace MauiAPPMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
    public ListaProduto()
    {
        InitializeComponent();

        lst_produtos.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        try
        {
            lista.Clear();

            List<Produto> tmp = await App.Db.GetAll();

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops", ex.Message, "OK");
        }
    }
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());

        }
        catch (Exception ex)
        {
            DisplayAlertAsync("Ops", ex.Message, "OK");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;
         
            lista.Clear();

            List<Produto> tmp = await App.Db.Search(q);

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops", ex.Message, "OK");
        }
        
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = $"O total é {soma:C}";

        DisplayAlertAsync("Total dos Produtos", msg, "OK");
    }





    private async void lst_produtos_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Produto p = e.CurrentSelection.FirstOrDefault() as Produto;

        if (p == null)
            return;

        string action = await DisplayActionSheetAsync(
            p.Descricao,
            "Cancelar",
            null,
            "Editar",
            "Excluir");

        if (action == "Editar")
        {
            await Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p
            });
        }
        else if (action == "Excluir")
        {

            bool confirm = await DisplayAlertAsync(
                 "Tem Certeza?",
                 $"Remover {p.Descricao}?",
                 "Sim",
                 "Não");

            if (confirm)
            {
                await App.Db.Delete(p.Id);
                var all = await App.Db.GetAll();
                await DisplayAlertAsync("DEBUG", $"Quantidade: {all.Count}", "OK");
                lista.Remove(p);
            }

        }

        ((CollectionView)sender).SelectedItem = null;
    }

 

    private async void pickerCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        string categoria = pickerCategoria.SelectedItem?.ToString() ?? "Todos";
        await CarregarProdutos(categoria);
    }

    private async Task CarregarProdutos(string categoria)
    {
        lista.Clear();

        List<Produto> produtos;
        if (categoria == "Todos")
            produtos = await App.Db.GetAll();
        else
            produtos = await App.Db.GetByCategoria(categoria);

        produtos.ForEach(p => lista.Add(p));
        lst_produtos.ItemsSource = lista;

    }

    private async void ToolbarItem_Clicked_2(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.RelatorioCategoria());
    }
}