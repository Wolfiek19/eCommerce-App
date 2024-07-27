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
	private void AddClicked(object sender, EventArgs e){
		Shell.Current.GoToAsync("//Product");
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e){ 
		(BindingContext as InventoryViewModel)?.Refresh();
	}

}