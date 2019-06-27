namespace StudentCookBook.Helper
{
	/// <summary>
	/// Шаблон для адаптера в меню выбор рецепта
	/// </summary>
	public class RecipeName
	{
		/// <summary>
		/// Номер элемента
		/// </summary>
		public int ID { get; set; }
		/// <summary>
		/// Название рецепта
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Наименование продуктов, которых не хватает
		/// </summary>
		public string Products { get; set; }
	}
}