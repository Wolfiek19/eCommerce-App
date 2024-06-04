using System.Collections.ObjectModel;

namespace Amz.Library;

public class CartServiceProxy
{
    private CartServiceProxy(){
        cartItems = new List<Item>();
    }
    private static CartServiceProxy? instance;
    private static object instanceLock = new object();
    public static CartServiceProxy? Current
        {
            get{

                lock(instanceLock){
                    if(instance == null){
                    instance = new CartServiceProxy();
                }

                }
                return instance;
            }
            
        }
    private List<Item>? cartItems;
    public ReadOnlyCollection<Item>? CartItems { //this is the read
        get
        {
            return cartItems?.AsReadOnly();
        }
    }

    //=============================functionality===============================
    public void CartAdd(int id, int quantity){
        if(cartItems == null){
            return;
        }
        var item = InventoryServiceProxy.Current.FindById(id);
        if(item == null){
            Console.WriteLine("Item not found in inventory.");
        }
        if(item.Quantity < quantity){
            Console.WriteLine("There is not enough items in inventory, please try again.");
        }
        var cartItem = new Item(item) {Quantity = quantity};

        cartItems.Add(cartItem);

        item.Quantity -= quantity;
    }

    public void Delete(int id, int quantity){
        if(cartItems == null){
            return;
        }
        var toRemove = cartItems.FirstOrDefault(c => c.Id == id);
        if(toRemove != null){
            if(toRemove.Quantity > quantity){
                toRemove.Quantity -= quantity;

            }
            else{
                
                quantity = toRemove.Quantity;
                cartItems.Remove(toRemove);  
            }
            var invItem = InventoryServiceProxy.Current.FindById(id);
            if (invItem != null){
                invItem.Quantity += quantity;
            }
        }
        
    }

    public void Checkout(){
        if(cartItems == null || !cartItems.Any()){
            Console.WriteLine("Your cart is empty");
            return;
        }

        double subTotal = 0;

        Console.WriteLine("Receipt: ");
        Console.WriteLine("-----------------------------");

        foreach(var cartItem in cartItems){
            double itemTotal = cartItem.Quantity * cartItem.Price;
            subTotal += itemTotal;
            Console.WriteLine($"{cartItem.Quantity} @ {cartItem.Price} \t {cartItem.Name} \t {itemTotal:C}");
        }
        double tax = subTotal * 0.07;
        double total = subTotal + tax;

        Console.WriteLine("-------------------------");
        Console.WriteLine($"Subtotal: \t\t\t {subTotal:C}");
        Console.WriteLine($"Tax: \t\t\t {tax:C}");
        Console.WriteLine($"Total: \t\t\t {total:C}");


        cartItems.Clear();
    }
}
