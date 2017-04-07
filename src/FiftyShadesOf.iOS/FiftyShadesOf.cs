using System;
using System.Collections.Generic;
using UIKit;

namespace Florent37.FiftyShadesOfXamarin
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

		public FiftyShadesOf Start()
		{
			foreach (IFiftyShadeView view in _viewStates.Values)
			{
				view.Start();
			}

			return this;
		}

		public FiftyShadesOf Stop()
		{
			foreach (IFiftyShadeView view in _viewStates.Values)
			{
				view.Stop();
			}

			return this;
		}

		public FiftyShadesOf AutoLayout(bool enableAutoLayout)
		{
			foreach (IFiftyShadeView view in _viewStates.Values)
			{
				view.AutoLayout(enableAutoLayout);
			}

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
