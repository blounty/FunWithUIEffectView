using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreLocation;
using MonoTouch.MapKit;

namespace FunWithEffects
{
	public partial class FunWithEffectsViewController : UIViewController
	{
		private UIVisualEffectView blurView;
		private const int CIRCLE_RECT_SIZE = 250;

		public FunWithEffectsViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			this.blurView = new UIVisualEffectView (UIBlurEffect.FromStyle (UIBlurEffectStyle.Light));
			this.blurView.Frame = this.View.Frame;
			this.blurView.UserInteractionEnabled = false;
			this.View.Add (this.blurView);

			var maskLayer = new CAShapeLayer ();
			var maskPath = new CGPath ();
			maskPath.AddRect (this.blurView.Bounds);
			maskPath.AddEllipseInRect (new RectangleF (((this.blurView.Bounds.Width - CIRCLE_RECT_SIZE) / 2), ((this.blurView.Bounds.Height - CIRCLE_RECT_SIZE) / 2), CIRCLE_RECT_SIZE, CIRCLE_RECT_SIZE));
			maskLayer.Path = maskPath;
			maskLayer.FillRule = CAShapeLayer.FillRuleEvenOdd;
			this.blurView.Layer.Mask = maskLayer;

			this.AddLocationButton (UIColor.Blue, "Sydney", 0, -33.8678500, 151.2073200);
			this.AddLocationButton (UIColor.Purple, "Boston", 1, 42.3584300, -71.0597700);
			this.AddLocationButton (UIColor.Green, "San Fran", 2, 37.7749300, -122.4194200);
			this.AddLocationButton (UIColor.Red, "Seattle", 3, 47.6062100, -122.3320700);
		}

		private void AddLocationButton(UIColor buttonColor, string buttonText, int position, double latitude, double longitude)
		{
			var xPosition = 10 + (80 * position);

			var button = new CircularEffectButton (
				new PointF (xPosition, this.View.Frame.Height - 100), 
				60, 
				new UIVibrancyEffect(),
				buttonColor, 
				buttonText,
				UIColor.White
			);

			button.TouchUpInside += (object sender, EventArgs e) => {
				var geoLocation = new CLLocationCoordinate2D(latitude, longitude);
				var span = new MKCoordinateSpan(0.3, 0.3);
				var mapRegion = new MKCoordinateRegion(geoLocation, span);

				this.MapView.SetRegion(mapRegion, true);

			};
			this.View.InsertSubviewAbove(button, this.blurView);
		}

		#endregion
	}
}

