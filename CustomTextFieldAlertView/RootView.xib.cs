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
			
			PromptForPassword();
			
			this.button.TouchDown += delegate { PromptForName(HandlerToUse.Delegate); };
			this.buttonForClicked.TouchDown += delegate { PromptForName(HandlerToUse.Clicked); };
			this.buttonPassword.TouchDown += delegate { PromptForPassword(); };
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
			
			UIAlertView myAlertView = new UIAlertView()
			{
				Title = "Please enter your name",
				Message = "this is hidden"
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

		void HandleMyPasswordAlertViewClicked (object sender, UIButtonEventArgs e)
		{
			if(e.ButtonIndex == 1)
			{
				string pwEntered = ((TextFieldAlertView) sender).EnteredText;
				
				this.SetNameLabel(string.Format("Password is {0}", pwEntered));
				this.SetWhereLabel("via Password");
			}
			else
			{
				// Cancelled was clicked
			}
		}
		
		private void PromptForPassword()
		{
			
			TextFieldAlertView myAlertView = new TextFieldAlertView(true)
			{
				Title = "Faux Account Password ",
				Message = "account@example.com"
			};
			
			myAlertView.Clicked += HandleMyPasswordAlertViewClicked;
			myAlertView.Transform = MonoTouch.CoreGraphics.CGAffineTransform.MakeTranslation (0f, 110f);
			myAlertView.Show ();
		}

		void HandleMyAlertViewClicked (object sender, UIButtonEventArgs e)
		{
			if(e.ButtonIndex == 1)
			{
				string nameEntered = ((TextFieldAlertView) sender).EnteredText;
				
				this.SetNameLabel(string.Format("Hello {0}!", nameEntered));
				this.SetWhereLabel("via Clicked");
			}
			else
			{
				// Cancelled was clicked
			}
		}
		
		public void SetNameLabel(string name)
		{
			this.nameLabel.Text = name;
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
				string nameEntered = ((TextFieldAlertView) alertview).EnteredText;
				
				viewController.SetNameLabel(nameEntered);
				viewController.SetWhereLabel("via Delegate");
			}
			else
			{
				// Cancelled was clicked
			}
		}
	}
}