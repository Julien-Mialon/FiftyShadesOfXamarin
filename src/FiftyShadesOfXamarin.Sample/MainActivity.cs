using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using Florent37.FiftyShadesOfXamarin;

namespace FiftyShadesOfXamarin.Sample
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
	public class MainActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.activity_main);
			View content = FindViewById(Android.Resource.Id.Content);
			content!.Click += OnClick;
		}

		private void OnClick(object sender, EventArgs e)
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