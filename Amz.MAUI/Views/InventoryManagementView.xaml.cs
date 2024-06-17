namespace Amz.MAUI.Views;

public partial class InventoryManagementView : ContentPage
{
	public InventoryManagementView()
	{
		InitializeComponent();
		BindingContext = new InventoryViewModel();
	}
	
	private void BackClicked(object sender, EventArgs e){
		Shell.Current.GoToAsync("//MainPage");
	}
}