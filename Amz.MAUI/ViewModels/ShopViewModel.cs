using System.ComponentModel;
using System.Runtime.CompilerServices;
using Amz.Library;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;

namespace Amz.MAUI;

public class ShopViewModel : INotifyPropertyChanged 
{
    public ShopViewModel(){
        InventoryQuery = string.Empty;
    }
    private string inventoryQuery;
    public string InventoryQuery {
        set { 
            inventoryQuery = value;
            NotifyPropertyChanged();
        }
        get { return inventoryQuery; }
    }
    public List<ProductViewModel> Items { 
        get{ 
            return InventoryServiceProxy.Current.Items.Where(p =>p != null).Where(p => p.Quantity > 0).Where(p => p?.Name?.ToUpper().Contains(InventoryQuery.ToUpper())?? false).
            Select(p => new ProductViewModel(p)).ToList() ?? new List<ProductViewModel>(); 
        } 
    }

    public List<ProductViewModel> ItemsInCart
    {
        get{
            return ShoppingCart?.CartItems?.Where(p => p != null)
            .Where(p => p?.Name?.ToUpper().Contains(InventoryQuery.ToUpper()) ?? false)
            .Select(p => new ProductViewModel(p)).ToList()
            ?? new List<ProductViewModel>(); 
        }
    }

    //private Item itemToBuy;

    private ProductViewModel? itemToBuy;
    public ProductViewModel? ItemToBuy{ 
        get => itemToBuy;
        set {
            itemToBuy = value;
            if(itemToBuy != null && itemToBuy.Model == null){
                itemToBuy.Model = new Item();
            }else if(itemToBuy != null && itemToBuy.Model != null){
                itemToBuy.Model = new Item(itemToBuy.Model);
            }

            //NotifyPropertyChanged();
        }
     }
    

    public Cart ShoppingCart {
        get{
            return CartServiceProxy.Current.ShoppingCart;
        }
    }

    public void Refresh()
    {
        InventoryQuery = string.Empty;
        NotifyPropertyChanged(nameof(Items));
        NotifyPropertyChanged(nameof(ItemToBuy));
       
    }

    public void Search(){
        NotifyPropertyChanged(nameof(Items));
    }

    public decimal CartTotal
    {
        get{
            return ShoppingCart?.CartItems?.Sum(item => item.Quantity * item.Price) ?? 0;
        }
    }

    public void PlaceInCart(){
        if(ItemToBuy == null|| ItemToBuy.Model == null){
            return;
        }
        ItemToBuy.Model.Quantity = 1;
        CartServiceProxy.Current.CartAdd(ItemToBuy.Model);
        ItemToBuy = null;
        NotifyPropertyChanged(nameof(ItemsInCart));
        NotifyPropertyChanged(nameof(Items));
        NotifyPropertyChanged(nameof(CartTotal));
       
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}