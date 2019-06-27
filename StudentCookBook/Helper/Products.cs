using SQLite;

namespace StudentCookBook.Helper
{
	/// <summary>
	/// Шаблон таблицы с продуктами из базы данных
	/// </summary>
	class Products
	{
		/// <summary>
		/// Id продукта
		/// </summary>
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		/// <summary>
		/// Название продукта
		/// </summary>
		public string Product { get; set; }
	}
}