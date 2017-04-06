using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Florent37.FiftyShadesOfXamarin;
using System.Threading.Tasks;

namespace FiftyShadesOfXamarin.Sample
{
	[Activity(Label = "FiftyShadesOfXamarin.Sample", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			View content = FindViewById(Android.Resource.Id.Content);
			content.Click += OnClick;
		}

		private void OnClick(object sender, System.EventArgs e)
		{
			FiftyShadesOf fiftyShadesOf = FiftyShadesOf.With(this)
													   .On(Resource.Id.layout, Resource.Id.layout1, Resource.Id.layout2)
													   .Start();

			Task.Run(async () =>
			{
				await Task.Delay(2500);
				RunOnUiThread(() => fiftyShadesOf.Stop());
			});
		}
	}
}

