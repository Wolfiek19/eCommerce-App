using System.ComponentModel;
using System.Runtime.CompilerServices;
using Amz.Library;

namespace Amz.MAUI;

public class InventoryViewModel : INotifyPropertyChanged
{
    public List<ProductViewModel> Items { 
        get{ 
            return InventoryServiceProxy.Current.Items.Where(p =>p != null).
            Select(p => new ProductViewModel(p)).ToList() ?? new List<ProductViewModel>(); 
        } 
    }

    public void Refresh()
    {
        NotifyPropertyChanged("Items");
        //InventoryServiceProxy.Current.Refresh();
    }
    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
}
