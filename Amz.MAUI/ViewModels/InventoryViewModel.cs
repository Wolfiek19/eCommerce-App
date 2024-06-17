using Amz.Library;

namespace Amz.MAUI;

public class InventoryViewModel
{
    public List<Item> Items { 
        get{ return InventoryServiceProxy.Current.Items.ToList(); } 
    }
}
