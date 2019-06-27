namespace StudentCookBook.Helper
{
	/// <summary>
	/// Шаблон таблицы связей между рецептами и продуктами из базы данных
	/// </summary>
	class db_prod
	{
		/// <summary>
		/// Id рецепта
		/// </summary>
		public int db_id { get; set; }
		/// <summary>
		/// Id продукта
		/// </summary>
		public int prod_id { get; set; }
	}
}