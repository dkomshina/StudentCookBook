namespace StudentCookBook.Helper
{
	/// <summary>
	/// Класс для передачи данный между activity
	/// </summary>
	static class Data
	{
		/// <summary>
		/// Возвращает таблицу, где на нулевой позиции отмечено, если этот рецепт используется, 
		/// на остальных позициях, если продукт присутствует в рецепте и при это выбран пользователем
		/// </summary>
		public static bool[,] RowData { get; set; }
		/// <summary>
		/// Возвращает таблицу, где на всех позициях, кроме нулевой, отмечены продукты, которые есть в рецепте
		/// </summary>
		public static bool[,] PrInf { get; set; }
		/// <summary>
		/// Возвращает название рецепта
		/// </summary>
		public static string StringData { get; set; }
		/// <summary>
		/// Возвращает продукты, которые есть в определенном рецепте
		/// </summary>
		public static string[] Prod { get; set; }
	}
}