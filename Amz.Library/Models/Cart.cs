
namespace Amz.Library;

public class Cart
{
    
    
    public List<Item>? CartItems{ get; set; }  //may have to readd the private backing field if problems are caused I am not sure if necessary right now though
    

    public int Id { get; set; }
    public Cart(){
        CartItems = new List<Item>();
    }

}
