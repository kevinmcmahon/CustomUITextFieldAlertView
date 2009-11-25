using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace CustomTextFieldAlertView
{
	public partial class RootView : UIViewController
	{
		#region Constructors

		// The IntPtr and NSCoder constructors are required for controllers that need 
		// to be able to be created from a xib rather than from managed code

		public RootView (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public RootView (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public RootView () : base("RootView", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
		}
		
		#endregion
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.button.TouchDown += delegate(object sender, EventArgs e) 
										{
											PromptForName(HandlerToUse.Delegate);	
										};
			
			this.buttonForClicked.TouchDown += delegate(object sender, EventArgs e) 
												{
													PromptForName(HandlerToUse.Clicked);	
												};
		}
		
		private void PromptForName(HandlerToUse handlerType)
		{
			UITextField tf = new UITextField (new System.Drawing.RectangleF (12f, 45f, 260f, 25f));
			tf.BackgroundColor = UIColor.White;
			tf.UserInteractionEnabled = true;
			tf.AutocorrectionType = UITextAutocorrectionType.No;
			tf.AutocapitalizationType = UITextAutocapitalizationType.None;
			tf.ReturnKeyType = UIReturnKeyType.Done;
			tf.SecureTextEntry = false;  // Set this to true if you want a password-style text masking
			
			UIAlertView myAlertView = new UIAlertView
			{
				Title = "Please enter your name",
				Message = "this line is hidden"
			};
			
			myAlertView.AddButton("Cancel");
			myAlertView.AddButton("Ok");
			myAlertView.AddSubview(tf);
			
			if (handlerType == HandlerToUse.Delegate)
			{
				myAlertView.Delegate = new MyAlertDelegate(this);
			}
			else
			{
				myAlertView.Clicked += HandleMyAlertViewClicked;
			}
			
			myAlertView.Transform = MonoTouch.CoreGraphics.CGAffineTransform.MakeTranslation (0f, 110f);
			
			myAlertView.Show ();
		}

		void HandleMyAlertViewClicked (object sender, UIButtonEventArgs e)
		{
			if(e.ButtonIndex == 1)
			{
				string nameEntered = ((UIAlertView) sender).Subviews.OfType<UITextField>().Single().Text;
				
				this.SetNameGreeting(nameEntered);
				this.SetWhereLabel("via Clicked");
			}
			else
			{
				// Cancelled was clicked
			}
		}
		
		public void SetNameGreeting(string name)
		{
			this.nameLabel.Text = string.Format("Hello {0}!", name);
		}
		
		public void SetWhereLabel(string whereFrom)
		{
			this.whereLabel.Text = whereFrom;
		}
	}
	public enum HandlerToUse
	{
		Delegate,
		Clicked
	}
	
	public class MyAlertDelegate : UIAlertViewDelegate
	{
		RootView viewController;
		
		public MyAlertDelegate(RootView vc)
		{
			this.viewController = vc;
		}
	
		public override void Clicked (UIAlertView alertview, int buttonIndex)
		{
			if (buttonIndex == 1)
			{
				string nameEntered = alertview.Subviews.OfType<UITextField>().Single().Text;
				
				viewController.SetNameGreeting(nameEntered);
				viewController.SetWhereLabel("via Delegate");
			}
			else
			{
				// Cancelled was clicked
			}
		}
	}
}