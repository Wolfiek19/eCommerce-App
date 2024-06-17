using System.Reflection.Metadata;

namespace Amz.Library;

public class Item
{
    private string? name;
    public string? Name{
        get{ return name; }
        set{ name = value; }
    }
    private string? description;
    public string? Description{
        get{ return description; }
        set{ description = value; }
    }
    private decimal price;
    public decimal Price{
        get{return price;}
        set{price = (decimal)value;}
    }
    public int? Id{get; set; } = 0;
    private int quantity;
    public int Quantity{
        get{ return quantity; }
        set{ quantity = value;}
    }
    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Description: {Description}, Price: {Price:C}, Quantity: {Quantity}";
    }

    public Item(){

    }

    public Item(Item it){
        this.Name = it.Name;
        this.Description = it.Description;
        this.Price = it.Price;
        this.Id = it.Id;
        this.Quantity = it.Quantity;
    }
}
