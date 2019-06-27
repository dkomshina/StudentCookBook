using SQLite;

namespace StudentCookBook.Helper
{
	/// <summary>
	/// Шаблон таблицы с рецептами из базы данных
	/// </summary>
	class Db
	{
		/// <summary>
		/// Id рецепта
		/// </summary>
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		/// <summary>
		/// Название рецепта
		/// </summary>
		public string name { get; set; }
		/// <summary>
		/// Рецепт
		/// </summary>
		public string recipe { get; set; }
		/// <summary>
		/// Изображение блюда
		/// </summary>
		public byte[] picture { get; set; }
	}
}