using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoreGraphics;
using UIKit;

namespace FiftyShadesOf.iOS.Sample
{
	public class MainController : UIViewController
	{
		public MainController()
		{
			UILabel firstLine = new UILabel(new CGRect(32, 100, 200, 30))
			{
				Font = UIFont.SystemFontOfSize(14, UIFontWeight.Semibold),
				Text = "Hello world"
			};
			UILabel secondLine = new UILabel(new CGRect(32, 140, 200, 25))
			{
				Font = UIFont.SystemFontOfSize(12, UIFontWeight.Regular),
				Text = "This is a loading view"
			};

			UITapGestureRecognizer tap = new UITapGestureRecognizer(recognizer =>
			{
				recognizer.Enabled = false;
				FiftyShadesOf shade = FiftyShadesOf.With(this).On(View).Start();

				Task.Run(async () =>
				{
					await Task.Delay(2500);
					InvokeOnMainThread(() =>
					{
						shade.Stop();
						recognizer.Enabled = true;
					});
				});
			});

			View.AddSubviews(firstLine, secondLine);
			View.BackgroundColor = UIColor.White;
			View.AddGestureRecognizer(tap);
		}
	}
}
