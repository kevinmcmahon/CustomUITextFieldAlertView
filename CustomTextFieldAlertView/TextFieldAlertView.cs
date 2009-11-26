using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace CustomTextFieldAlertView
{
	public class TextFieldAlertView : UIAlertView
	{
		private UITextField _tf;
		private bool _secureTextEntry;
		
		public TextFieldAlertView() : this(false) {}
		
		public TextFieldAlertView(bool secureTextEntry, string title, string message, UIAlertViewDelegate alertViewDelegate, string cancelBtnTitle, params string[] otherButtons)
			: base(title, message, alertViewDelegate, cancelBtnTitle, otherButtons)
		{
			InitializeControl(secureTextEntry);
		}
		
		public TextFieldAlertView(bool secureTextEntry)
		{	
			InitializeControl(secureTextEntry);
		}
		
		private void InitializeControl(bool secureTextEntry)
		{
			_secureTextEntry = secureTextEntry;
			this.AddButton("Cancel");
			this.AddButton("Ok");
			this.Transform = MonoTouch.CoreGraphics.CGAffineTransform.MakeTranslation(0f, 110f);
		}
		
		public string EnteredText { get { return _tf.Text; } }
		
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			
			_tf = ComposeTextFieldControl(_secureTextEntry);
			
			this.AddSubview(_tf);
			
			AdjustControlSize();
		}

		private UITextField ComposeTextFieldControl(bool secureTextEntry)
		{
			UITextField textField = new UITextField (new System.Drawing.RectangleF (12f, 45f, 260f, 25f));
			textField.BackgroundColor = UIColor.White;
			textField.UserInteractionEnabled = true;
			textField.AutocorrectionType = UITextAutocorrectionType.No;
			textField.AutocapitalizationType = UITextAutocapitalizationType.None;
			textField.ReturnKeyType = UIReturnKeyType.Done;
			textField.SecureTextEntry = secureTextEntry;
			return textField;
		}
		
		private void AdjustControlSize()
		{
			float tfH = 25.0f;
			float tfExtH = tfH + 16.0f;
			
			RectangleF frame = new RectangleF(this.Frame.X, 
			                                  this.Frame.Y - tfExtH/2,
			                                  this.Frame.Size.Width,
			                                  this.Frame.Size.Height + tfExtH);
			this.Frame = frame;
			
			foreach(var view in this.Subviews)
			{
				if(view is UIControl)
				{
					view.Frame = new RectangleF(view.Frame.X, 
					                            view.Frame.Y + tfExtH,
					                            view.Frame.Size.Width, 
					                            view.Frame.Size.Height);
				}
			}
		}
	}
}