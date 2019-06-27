using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace StudentCookBook
{
	/// <summary>
	/// Главная страница приложения
	/// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
		Button btnChoose;

		/// <summary>
		/// Создание страницы
		/// </summary>
		/// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

			btnChoose = FindViewById<Button>(Resource.Id.btnChoose);
			btnChoose.Click += (x, y) => StartActivity(new Android.Content.Intent(this, typeof(ChooseProducts)));
        }
    }
}