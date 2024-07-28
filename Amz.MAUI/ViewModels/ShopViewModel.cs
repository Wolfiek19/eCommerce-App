using System.ComponentModel;
using System.Runtime.CompilerServices;
using Amz.Library;

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
            return InventoryServiceProxy.Current.Items.Where(p =>p != null).Where(p => p?.Name?.ToUpper().Contains(InventoryQuery.ToUpper())?? false).
            Select(p => new ProductViewModel(p)).ToList() ?? new List<ProductViewModel>(); 
        } 
    }

    public void Refresh()
    {
        InventoryQuery = string.Empty;
        NotifyPropertyChanged(nameof(Items));
       
    }

    public void Search(){
        NotifyPropertyChanged(nameof(Items));
    }
    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}