using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Database;
using Android.Database.Sqlite;
using StudentCookBook.Helper;

namespace StudentCookBook
{
	/// <summary>
	/// Класс страницы выбора рецепта
	/// </summary>
	[Activity(Label = "ChooseRecipe")]
	public class ChooseRecipe : Activity
	{
		DBHelper db;
		SQLiteDatabase sqliteDB;
		ListView lv;
		List<RecipeName> listDb = new List<RecipeName>();
		List<string> helperList = new List<string>();

		/// <summary>
		/// Создание страницы
		/// </summary>
		/// <param name="savedInstanceState"></param>
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			db = new DBHelper(this);
			sqliteDB = db.WritableDatabase;

			SetContentView(Resource.Layout.ChooseRecipe);
			lv = FindViewById<ListView>(Resource.Id.listView1);

			AddData();
			ShowData();

			lv.ItemClick += Lv_ItemClick;
		}

		/// <summary>
		/// Открытие страницы рецепта при нажатии на его название
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			Data.StringData = helperList[e.Position];
			StartActivity(new Android.Content.Intent(this, typeof(Recipe)));
		}

		/// <summary>
		/// Добавление данных о том, каких ингредиентов не хватает, составление шаблонного списка рецептов
		/// </summary>
		private void AddData()
		{
			ICursor selectData = sqliteDB.RawQuery("select name from db", new string[] { });
			selectData.MoveToFirst();
			int i = 0;
			do
			{
				if (Data.RowData[++i, 0])
				{
					string str = "Не хватает: ";
					for (int j = 0; j < Data.PrInf.GetLength(1); j++)
					{

						if (Data.PrInf[i, j] == true && Data.RowData[i, j] == false)
							str += $"{Data.Prod[j]}, ";
					}
					if (str == "Не хватает: ") str = "";
					else str = str.Substring(0, str.Length - 2);

					string s = selectData.GetString(selectData.GetColumnIndex("name"));
					helperList.Add(s);

					RecipeName recipeName = new RecipeName()
					{
						ID = i,
						Name = s,
						Products = str
					};
					listDb.Add(recipeName);
				}
			} while (selectData.MoveToNext());
			selectData.Close();
		}

		/// <summary>
		/// Отображение списка рецептов на экран
		/// </summary>
		private void ShowData()
		{
			var adapter = new CustomAdapter(this, listDb);
			lv.Adapter = adapter;
		}
	}
}