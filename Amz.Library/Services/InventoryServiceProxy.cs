using System.Collections.ObjectModel;

namespace Amz.Library;

public class InventoryServiceProxy
{
    private InventoryServiceProxy(){ 
        //Remove sample data
        items = new List<Item>{
            new Item{Id = 1, Name = "Item 1", Price = 4.50M, Quantity = 1},
            new Item{Id = 2, Name = "Item 2", Price = 10M, Quantity = 10 },
            new Item{Id = 3, Name = "Item 3", Price = 123.55M, Quantity = 100 }
        };
    }
    private static InventoryServiceProxy? instance;
    private static object instanceLock = new object();
    public static InventoryServiceProxy Current{
        get{
            
            lock(instanceLock) //this makes it thread safe, we need singletons to be thread safe
            {
                if(instance == null){
                    instance = new InventoryServiceProxy();
                }
            }
            
            
            return instance;
        }
    }

    private List<Item>? items;
    public ReadOnlyCollection<Item>? Items { //this is the read
        get
        {
            return items?.AsReadOnly();
        }
    }

    //================functionality================
    public int LastId{
        get{
            if(items?.Any() ?? false){
                return items?.Select(c => c.Id)?.Max() ?? 0;
            }
            return 0;
        }
    }
    public Item? invAdd(Item item)
    {
        if(items == null)
        {
            return null;
        }
        bool isAdd = false; 

        if(item.Id == 0){
            item.Id = LastId + 1;
            isAdd = true;
        }
        if(isAdd){
            items.Add(item);
        }
        return item;
    }
    public Item? FindById(int id){
        return items?.FirstOrDefault(c => c.Id == id);
    }
    
    public void Delete(int id){
        if(items == null){
            return;
        }
        
        var itemToDelete = items.FirstOrDefault(c => c.Id == id);

        if(itemToDelete != null){
            items.Remove(itemToDelete);
        }
    }
}
