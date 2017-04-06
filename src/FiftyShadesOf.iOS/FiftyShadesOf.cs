using System;
using System.Collections.Generic;
using UIKit;

namespace FiftyShadesOf.iOS
{
	public class FiftyShadesOf
	{
		private readonly UIViewController _context;
		private readonly Dictionary<UIView, IFiftyShadeView> _viewStates = new Dictionary<UIView, IFiftyShadeView>();

		public FiftyShadesOf(UIViewController context)
		{
			_context = context;
		}

		public static FiftyShadesOf With(UIViewController context) => new FiftyShadesOf(context);

		public FiftyShadesOf On(params UIView[] views)
		{
			if (views == null)
			{
				throw new ArgumentNullException(nameof(views));
			}

			foreach (var view in views)
			{
				Add(view);
			}
			return this;
		}
		public FiftyShadesOf Except(params UIView[] views)
		{
			foreach (var view in views)
			{
				if (_viewStates.ContainsKey(view))
				{
					_viewStates.Remove(view);
				}
			}
			return this;
		}

		public FiftyShadesOf FadeIn(bool fadeIn)
		{
			return this;
		}

		public FiftyShadesOf Start()
		{
			return this;
		}

		public FiftyShadesOf Stop()
		{
			return this;
		}

		private void Add(UIView view)
		{
			IFiftyShadeView shadeView = CreateFiftyShadeView(view);

			if (shadeView != null)
			{
				_viewStates.Add(view, shadeView);
			}

			if (view.Subviews != null)
			{
				foreach (var subview in view.Subviews)
				{
					Add(subview);
				}
			}
		}

		protected virtual IFiftyShadeView CreateFiftyShadeView(UIView fromView)
		{
			if (fromView is UILabel)
			{
				return new LabelFiftyShadeView(_context, (UILabel) fromView);
			}
			if (fromView is UIImageView)
			{
				return new ImageFiftyShadeView(_context, (UIImageView) fromView);
			}

			return null;
		}
	}
}
