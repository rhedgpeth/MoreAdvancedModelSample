using System;
using ManualViewModelSample.ViewModels;
using UIKit;

namespace ManualViewModelSample.iOS
{
	public partial class ViewController : UIViewController
	{
		PatientViewModel viewModel;

		public ViewController(IntPtr handle) : base(handle)
		{ }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			viewModel = new PatientViewModel();

			viewModel.OnSaveComplete = DoSomethingOnSave;

			viewModel.WatchProperty(nameof(viewModel.Nicknames), () =>
			{
				// Listen for property change event and update the UI
			});
		}

		public void DoSomethingOnSave(bool success)
		{
			if (success)
			{
				// Do something
			}
			else 
			{
				// Do something else
			}
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}
	}
}
