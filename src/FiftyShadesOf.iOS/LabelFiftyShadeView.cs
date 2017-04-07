using UIKit;

namespace Florent37.FiftyShadesOfXamarin
{
	public class LabelFiftyShadeView : DefaultFiftyShadeView<UILabel>
	{
		public LabelFiftyShadeView(UIViewController context, UILabel view) : base(context, view)
		{
			bool bold = view.Font?.FontDescriptor?.SymbolicTraits.HasFlag(UIFontDescriptorSymbolicTraits.Bold) ?? false;

			NormalColor = bold ? ShadesColors.DarkerGray : ShadesColors.DefaultGray;
			AnimatedColor = bold ? ShadesColors.DarkerGrayAnimated : ShadesColors.DefaultGrayAnimated;
		}
	}
}