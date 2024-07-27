namespace Amz.MAUI.Views;

public partial class CartView : ContentPage
{
	public CartView()
	{
		InitializeComponent();
	}

    private void BackClicked(object sender, EventArgs e){
		Shell.Current.GoToAsync("//Store");
	}
}