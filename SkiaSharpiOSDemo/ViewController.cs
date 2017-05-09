using System;
using UIKit;

using CustomSkiaSharpViews;

namespace SkiaSharpiOSDemo
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var canvas = new SkiaSharpView(View.Bounds);
			canvas.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;

			View.AddSubview(canvas);
		}
	}
}
