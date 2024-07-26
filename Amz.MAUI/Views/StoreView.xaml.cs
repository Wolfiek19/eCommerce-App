namespace Amz.MAUI.Views;

public partial class StoreView : ContentPage
{
	public StoreView()
	{
		InitializeComponent();
	}

	private void BackClicked(object sender, EventArgs e){
		Shell.Current.GoToAsync("//MainPage");
	}

	private void CartClicked(object sender, EventArgs e){
		Shell.Current.GoToAsync("//Cart");
	}
}