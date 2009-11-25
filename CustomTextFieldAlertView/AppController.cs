using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace CustomTextFieldAlertView
{
	[Register ("AppController")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;
		
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			RootView rvc = new RootView();
			
			window = new UIWindow(UIScreen.MainScreen.Bounds);
			window.AddSubview(rvc.View);
			window.MakeKeyAndVisible();
			
			return true;
		}

		// This method is required in iPhoneOS 3.0
		public override void OnActivated (UIApplication application)
		{
		}
	}
}