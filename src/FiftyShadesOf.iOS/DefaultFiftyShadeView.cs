using System;
using CoreAnimation;
using UIKit;

namespace FiftyShadesOf.iOS
{
	public abstract class DefaultFiftyShadeView<TView> : IFiftyShadeView where TView : UIView
	{
		private nfloat _previousAlpha;
		private bool _fadeIn;

		protected UIViewController Context { get; }
		protected TView View { get; }
		protected UIView Shade { get; private set; }

		protected UIColor NormalColor { get; set; } = ShadesColors.DefaultGray;

		protected UIColor AnimatedColor { get; set; } = ShadesColors.DefaultGrayAnimated;

		protected DefaultFiftyShadeView(UIViewController context, TView view)
		{
			Context = context;
			View = view;
		}

		public virtual void Start(bool fadeIn)
		{
			_fadeIn = fadeIn;
			SaveState();
			Shade = CreateShade();

			StartShadeAnimation();
		}

		public virtual void Stop()
		{
			Shade.Layer.RemoveAllAnimations();
			UIView.Animate(0.4, () =>
			{
				Shade.Alpha = 0;
			}, () =>
			{
				UIView.Animate(0.4, RestoreState, () => { });
			});
		}

		protected virtual UIView CreateShade()
		{
			UIView shade = new UIView
			{
				BackgroundColor = UIColor.Clear,
			};
			Context.View.Add(shade);
			Context.View.AddConstraints(new[]
			{
				NSLayoutConstraint.Create(shade, NSLayoutAttribute.Width, NSLayoutRelation.Equal, View, NSLayoutAttribute.Width, 1f, 0f),
				NSLayoutConstraint.Create(shade, NSLayoutAttribute.Height, NSLayoutRelation.Equal, View, NSLayoutAttribute.Height, 1f, 0f),
				NSLayoutConstraint.Create(shade, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterX, 1f, 0f),
				NSLayoutConstraint.Create(shade, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterY, 1f, 0f),
			});

			return shade;
		}

		protected virtual void DestroyShade()
		{
			Shade.RemoveFromSuperview();
			Shade.Dispose();
			Shade = null;
		}

		protected virtual void StartShadeAnimation()
		{
			Shade.Layer.RemoveAllAnimations();

			CABasicAnimation animation = CABasicAnimation.FromKeyPath("backgroundColor");
			animation.Duration = 0.750;
			animation.RepeatCount = int.MaxValue;
			animation.AutoReverses = true;
			animation.SetFrom(NormalColor.CGColor);
			animation.SetTo(AnimatedColor.CGColor);

			Shade.Layer.AddAnimation(animation, "backgroundColor");
		}

		protected virtual void SaveState()
		{
			_previousAlpha = View.Alpha;
			View.Alpha = 0;
		}

		protected virtual void RestoreState()
		{
			View.Alpha = _previousAlpha;
		}
	}
}