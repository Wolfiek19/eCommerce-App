using Amz.Library;

namespace Amz.MAUI;

public class ProductViewModel
{
    public override string ToString(){
        if(Model == null){
            return string.Empty;
        }
        return $"{Model.Id} - {Model.Name} - {Model.Price:C} ";
    }
    public Item? Model { get; set; }

    public string DisplayPrice{
        get{

            if(Model == null){
                return string.Empty;
            }
            return  $"{Model.Price:C}";
        }
    }

    public string PriceAsString{
        set{
            if(Model == null){
                return;
            }
            if(decimal.TryParse(value, out var price)){
                Model.Price = price;
            }
            else{

            }
        }
    }

    public ProductViewModel(){
        Model = new Item();
    }
    public ProductViewModel(Item? model){
        if(model != null){
            Model = model;
        }
        else{
            Model = new Item(); //violates fail fast
        }
    }

    public void Add(){
        if(Model != null){
            InventoryServiceProxy.Current.invAdd(Model);
        }
    }

    public static implicit operator ProductViewModel(Item v)
    {
        throw new NotImplementedException();
    }
}
