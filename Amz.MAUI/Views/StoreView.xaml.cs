namespace Amz.MAUI.Views;

public partial class StoreView : ContentPage
{
	public StoreView()
	{
		InitializeComponent();
		BindingContext = new ShopViewModel();
	}

	private void AddToCartClicked(object sender, EventArgs e){
		(BindingContext as ShopViewModel).PlaceInCart();
	}

	private void BackClicked(object sender, EventArgs e){
		Shell.Current.GoToAsync("//MainPage");
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e){
		(BindingContext as ShopViewModel).Refresh();
	}

	private void InventorySearchClicked(object sender, EventArgs e){
		(BindingContext as ShopViewModel).Search(); //search feature is in SVM getter
	}
	
}