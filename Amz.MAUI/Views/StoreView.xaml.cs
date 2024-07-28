namespace Amz.MAUI.Views;

public partial class StoreView : ContentPage
{
	public StoreView()
	{
		InitializeComponent();
		BindingContext = new ShopViewModel();
	}

	private void BackClicked(object sender, EventArgs e){
		Shell.Current.GoToAsync("//MainPage");
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e){
		(BindingContext as ShopViewModel).Refresh();
	}
	
}