namespace Amz.MAUI;

public partial class MainPage : ContentPage
{
	

	public MainPage()
	{
		InitializeComponent();
	}
	private void Inventory(object sender, EventArgs e){
		Shell.Current.GoToAsync("//Inventory");
	}
	private void Store(object sender, EventArgs e){
		Shell.Current.GoToAsync("//Store");
	}

}

