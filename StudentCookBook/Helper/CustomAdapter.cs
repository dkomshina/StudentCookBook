using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;

namespace StudentCookBook.Helper
{
	/// <summary>
	/// Класс-шаблон для view
	/// </summary>
	public class ViewhHolder : Java.Lang.Object
	{
		public TextView txtName { get; set; }
		public TextView txtProduct { get; set; }
	}

	/// <summary>
	/// Собственный адаптер для меню "Выбор рецепта"
	/// </summary>
	public class CustomAdapter : BaseAdapter
	{
		private Activity activity;
		private List<RecipeName> recipeNames;

		public CustomAdapter(Activity activity, List<RecipeName> recipeNames)
		{
			this.activity = activity;
			this.recipeNames = recipeNames;
		}

		/// <summary>
		/// Возвращает число элементов
		/// </summary>
		public override int Count => recipeNames.Count;

		/// <summary>
		/// Возвращает объект на данной позиции
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public override Java.Lang.Object GetItem(int position)
		{
			return null;
		}

		/// <summary>
		/// Возвращает номер элемента на данной позиции
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public override long GetItemId(int position)
		{
			return recipeNames[position].ID;
		}

		/// <summary>
		/// Возвращает вид, который в шаблоне
		/// </summary>
		/// <param name="position"></param>
		/// <param name="convertView"></param>
		/// <param name="parent"></param>
		/// <returns></returns>
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.listViewChooseRecipe_Template, parent, false);
			var txtName = view.FindViewById<TextView>(Resource.Id.textV1);
			var txtProduct = view.FindViewById<TextView>(Resource.Id.textV2);

			txtName.Text = recipeNames[position].Name;
			txtProduct.Text = recipeNames[position].Products;

			return view;
		}
	}
}