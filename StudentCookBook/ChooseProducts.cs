using System.Collections.Generic;
using Android.App;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Views;
using Android.Widget;
using StudentCookBook.Helper;

namespace StudentCookBook
{
	/// <summary>
	/// Класс для страницы выбора продуктов
	/// </summary>
	[Activity(Label = "ChooseProducts")]
	public class ChooseProducts : Activity
	{
		DBHelper db;
		SQLiteDatabase sqliteDB;
		LinearLayout linLayout;
		List<CheckBox> checkBoxes = new List<CheckBox>(); 
		List<Products> listProducts = new List<Products>();
		Button button;
		int t;
		int length = 108;
		
		/// <summary>
		/// Создание страницы
		/// </summary>
		/// <param name="savedInstanceState"></param>
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			db = new DBHelper(this);
			sqliteDB = db.WritableDatabase;
			
			SetContentView(Resource.Layout.ChooseProducts);
			linLayout = FindViewById<LinearLayout>(Resource.Id.linLayout);
			AddData();

			button = FindViewById<Button>(Resource.Id.button1);
			button.Click += OnButtonClicked;

		}

		/// <summary>
		/// Добавление новых продуктов в список
		/// </summary>
		private void AddData()
		{
			ICursor selectData = sqliteDB.RawQuery("select * from Products", new string[] { });
			selectData.MoveToFirst();
			do
			{
				Products prod = new Products();
				prod.ID = int.Parse(selectData.GetString(selectData.GetColumnIndex("ID")));
				prod.Product = selectData.GetString(selectData.GetColumnIndex("product"));

				listProducts.Add(prod);
			} while (selectData.MoveToNext());
			selectData.Close();

			
			Data.Prod = new string[listProducts.Count+1];
			for (int i = 1; i < Data.Prod.Length; i++)
			{
				Data.Prod[i] = listProducts[i - 1].Product;
			}

			foreach(var item in listProducts)
			{
				CheckBox checkBoxProd = new CheckBox(this);
				checkBoxProd.TextSize = 20;
				checkBoxProd.Text = item.Product;
				checkBoxProd.Id = item.ID;
				checkBoxProd.LayoutParameters =new ViewGroup.LayoutParams
					(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
				linLayout.AddView(checkBoxProd);
				checkBoxes.Add(checkBoxProd);
			}

			Data.PrInf = new bool[length + 1, listProducts.Count + 1];
			selectData = sqliteDB.RawQuery("select * from db_prod", new string[] { });
			selectData.MoveToFirst();
			do
			{
				db_prod dp = new db_prod();
				dp.db_id = int.Parse(selectData.GetString(selectData.GetColumnIndex("db_id")));
				dp.prod_id = int.Parse(selectData.GetString(selectData.GetColumnIndex("prod_id")));

				Data.PrInf[dp.db_id, dp.prod_id] = true;
				
			} while (selectData.MoveToNext());
			selectData.Close();
		}

		/// <summary>
		/// Если продукт был выбран, информация об этом передается в промежуточный статический класс Data
		/// </summary>
		/// <param name="id"></param>
		private void dbProd(int id)
		{
			ICursor selectData = sqliteDB.RawQuery("select * from db_prod", new string[] { });
			selectData.MoveToFirst();
			do
			{
				db_prod dp = new db_prod();
				dp.db_id = int.Parse(selectData.GetString(selectData.GetColumnIndex("db_id")));
				dp.prod_id = int.Parse(selectData.GetString(selectData.GetColumnIndex("prod_id")));

				if (dp.prod_id == id)
				{
					Data.RowData[dp.db_id, 0] = true;
					Data.RowData[dp.db_id, id] = true;
					t++;
				}
			} while (selectData.MoveToNext());
			selectData.Close();
		}

		/// <summary>
		/// Событие при нажатии на кнопку "Продолжить", загрузка страницы выбора рецепта
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnButtonClicked(object sender, System.EventArgs e)
		{
			t = 0;
			Data.RowData = new bool[length + 1, listProducts.Count + 1];
			foreach (var item in checkBoxes)
			{
				if (item.Checked) dbProd(item.Id);
			}
			if (t == 0) Toast.MakeText(this, "Выберите продукты", ToastLength.Short).Show();
			else StartActivity(new Android.Content.Intent(this, typeof(ChooseRecipe)));
		}
	}
}