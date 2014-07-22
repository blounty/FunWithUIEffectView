using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;

namespace FunWithEffects
{
	public class CircularEffectButton
	 : UIButton
	{
		private UIVisualEffectView effectView;

		public CircularEffectButton (PointF location, float size, UIVisualEffect effect, UIColor backgroundColor, string title, UIColor titleColor)
			: base(UIButtonType.Custom)
		{

			this.Frame = new RectangleF(location, new SizeF(size, size));
			this.effectView = new UIVisualEffectView (effect);
			this.effectView.Frame = new RectangleF(new PointF(0,0), this.Frame.Size);
			this.effectView.UserInteractionEnabled = false;
			this.Add (this.effectView);

			this.BackgroundColor = backgroundColor.ColorWithAlpha(0.2f);
			this.SetTitle (title, UIControlState.Normal);
			var contrastingColor = this.ContrasingColor (backgroundColor);
			this.SetTitleColor (contrastingColor, UIControlState.Normal);
			this.TitleLabel.AdjustsFontSizeToFitWidth = true;
			this.TitleLabel.TextAlignment = UITextAlignment.Center;
			this.Layer.CornerRadius = size / 2;
			this.Layer.BorderWidth = 1;
			this.Layer.BorderColor = contrastingColor.CGColor;
			this.ClipsToBounds = true;
		}

		private UIColor ContrasingColor(UIColor originalColor)
		{
			var originalCGColor = originalColor.CGColor;

			var numberOfComponents = originalCGColor.NumberOfComponents;

			if (numberOfComponents == 1) {
				return new UIColor (originalCGColor);
			}

			var originalComponentColors = originalCGColor.Components;
			var newComponentColors = new float[numberOfComponents];

			var i = numberOfComponents - 1;

			newComponentColors [i] = originalComponentColors [i];
			i--;
			while (i >= 0) {
				newComponentColors[i] = 1 - originalComponentColors[i];
				i--;
			}

			var newCGColor = new CGColor (originalCGColor.ColorSpace, newComponentColors);
			var newUIColor = new UIColor (newCGColor);

			return newUIColor;
		}

	}
}

