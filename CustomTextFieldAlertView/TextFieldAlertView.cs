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
			: base(title,message,alertViewDelegate, cancelBtnTitle, otherButtons)
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
			
			ComposeTextFieldControl(_secureTextEntry);
			
			AdjustControlLocation();
		}

		private void ComposeTextFieldControl(bool secureTextEntry)
		{
			_tf = new UITextField (new System.Drawing.RectangleF (12f, 45f, 260f, 25f));
			_tf.BackgroundColor = UIColor.White;
			_tf.UserInteractionEnabled = true;
			_tf.AutocorrectionType = UITextAutocorrectionType.No;
			_tf.AutocapitalizationType = UITextAutocapitalizationType.None;
			_tf.ReturnKeyType = UIReturnKeyType.Done;
			_tf.SecureTextEntry = secureTextEntry;
			
			this.AddSubview(_tf);
		}
		
		private void AdjustControlLocation()
		{
			float tfWidth = 260.0f;
			float tfHeight = 25.0f;
			float tfExtHeight = tfHeight + 16.0f;
			
			RectangleF frame = new RectangleF(this.Frame.X, this.Frame.Y - tfExtHeight/2, this.Frame.Size.Width, this.Frame.Size.Height + tfExtHeight);
			this.Frame = frame;
			
			UIView lowestView = this.Subviews[0];
			int i = 0;
			while(!(this.Subviews[i] is UIControl))
			{
				UIView v = this.Subviews[i];
				if(lowestView.Frame.Y + lowestView.Frame.Size.Height < v.Frame.Y + v.Frame.Size.Height)
				{
					lowestView = v;
				}
				i++;
			}
			
			foreach(var view in this.Subviews)
			{
				if(view is UIControl)
				{
					view.Frame = new RectangleF(view.Frame.X, view.Frame.Y + tfExtHeight, view.Frame.Size.Width, view.Frame.Size.Height);
				}
			}
		}
	}
}