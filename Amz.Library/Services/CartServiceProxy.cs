using System.Collections.ObjectModel;
using Microsoft.VisualBasic;

namespace Amz.Library;

public class CartServiceProxy
{
    
    private List<Cart> carts;
    public ReadOnlyCollection<Cart> Carts{
        get{
            return carts.AsReadOnly();
        }
    }
    public Cart ShoppingCart{
        get{
            if(!carts.Any()){
                
                var newCart = new Cart();
                carts.Add(newCart);
                return newCart;
            }
            return carts.FirstOrDefault() ?? new Cart();      
    }
    }
    private CartServiceProxy(){
        carts = new List<Cart>();
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


    public void CartAdd(Item newItem)
{
    if (ShoppingCart == null || ShoppingCart.CartItems == null)
    {
        return;
    }

    var existingProduct = ShoppingCart.CartItems.FirstOrDefault(existingProduct => existingProduct.Id == newItem.Id);
    var inventoryProduct = InventoryServiceProxy.Current.Items.FirstOrDefault(invProd => invProd.Id == newItem.Id);
    if (inventoryProduct == null)
    {
        return;
    }

    inventoryProduct.Quantity -= newItem.Quantity;

    if (existingProduct != null)
    {
        existingProduct.Quantity += newItem.Quantity;
    }
    else
    {
        ShoppingCart.CartItems.Add(newItem);
    }
}

    public void Delete(int id, int quantity){
        if(cartItems == null){
            return;
        }
        var toRemove = CartItems.FirstOrDefault(c => c.Id == id);
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

        decimal subTotal = 0;

        Console.WriteLine("Receipt: ");
        Console.WriteLine("-----------------------------");

        foreach(var cartItem in cartItems){
            if(cartItem == null){
                continue;
            }
            var itemTotal = cartItem.Quantity * cartItem.Price;
            //decimal itemTotal = 0;
            subTotal += itemTotal;
            Console.WriteLine($"{cartItem.Quantity} @ {cartItem.Price} \t {cartItem.Name} \t {itemTotal:C}");
        }
        decimal tax = subTotal * 0.07m;
        decimal total = subTotal + tax;

        Console.WriteLine("-------------------------");
        Console.WriteLine($"Subtotal: \t\t\t {subTotal:C}");
        Console.WriteLine($"Tax: \t\t\t {tax:C}");
        Console.WriteLine($"Total: \t\t\t {total:C}");


        cartItems.Clear();
    }
}
