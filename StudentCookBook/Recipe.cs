using Android.App;
using Android.OS;
using Android.Widget;
using Android.Database;
using Android.Database.Sqlite;
using StudentCookBook.Helper;
using Android.Graphics;

namespace StudentCookBook
{
	/// <summary>
	/// Класс для страницы с рецептом
	/// </summary>
	[Activity(Label = "Recipe")]
	public class Recipe : Activity
	{
		DBHelper db;
		SQLiteDatabase sqliteDB;
		TextView tv1, 
			tv2;
		ImageView iv;

		/// <summary>
		/// Создание страницы
		/// </summary>
		/// <param name="savedInstanceState"></param>
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			db = new DBHelper(this);
			sqliteDB = db.WritableDatabase;

			SetContentView(Resource.Layout.Recipe);
			ShowData();
		}

		/// <summary>
		/// Отображение на экран рецепта и изображения блюда
		/// </summary>
		private void ShowData()
		{
			ICursor selectData = sqliteDB.RawQuery($"select * from db where name='{Data.StringData}'", new string[] { });
			selectData.MoveToFirst();
			tv1 = FindViewById<TextView>(Resource.Id.tv1);
			tv2 = FindViewById<TextView>(Resource.Id.tv2);
			iv = FindViewById<ImageView>(Resource.Id.iv);

			tv1.Text = selectData.GetString(selectData.GetColumnIndex("name"));

			byte[] byteArray = selectData.GetBlob(selectData.GetColumnIndex("picture"));
			Bitmap bmp = BitmapFactory.DecodeByteArray(byteArray, 0, byteArray.Length);
			ImageView image = (ImageView)FindViewById(Resource.Id.iv);
			image.SetImageBitmap(bmp);

			tv2.Text = selectData.GetString(selectData.GetColumnIndex("recipe"));
			selectData.Close();
		}
	}
}