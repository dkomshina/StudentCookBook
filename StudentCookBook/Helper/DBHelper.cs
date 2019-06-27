using System;
using System.IO;
using Android.Content;
using Android.Database.Sqlite;

namespace StudentCookBook.Helper
{
	/// <summary>
	/// Класс для подключения к базе данных
	/// </summary>
	public class DBHelper : SQLiteOpenHelper
	{
		private static string DB_PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
		private static string DB_NAME = "DataB.db";
		private static int VERSION = 1;
		private Context context;

		public DBHelper(Context context) : base(context, DB_NAME, null, VERSION)
		{
			this.context = context;
		}

		/// <summary>
		/// Возвращает путь к базе данных
		/// </summary>
		/// <returns></returns>
		private string GetSQLiteDBPath()
		{
			return Path.Combine(DB_PATH, DB_NAME);
		}

		/// <summary>
		/// Возвращает базу данных
		/// </summary>
		public override SQLiteDatabase WritableDatabase
		{
			get {
				return CreateSQLiteDB();
			}
		}

		/// <summary>
		/// Открытие базы данных
		/// </summary>
		/// <returns></returns>
		private SQLiteDatabase CreateSQLiteDB()
		{
			SQLiteDatabase sqliteDB = null;
			string path = GetSQLiteDBPath();
			Stream streamSQLite = null;
			FileStream streamWriter = null;
			bool isSQLiteInit = false;
			try
			{
				if (File.Exists(path))
					isSQLiteInit = true;
				else
				{
					streamSQLite = context.Resources.OpenRawResource(Resource.Raw.DataB);
					streamWriter = new FileStream(path, FileMode.Create, FileAccess.Write);
					if (streamSQLite != null && streamWriter != null)
					{
						if (CopySQLiteDB(streamSQLite, streamWriter))
							isSQLiteInit = true;
					}
				}
				if (isSQLiteInit)
				{
					sqliteDB = SQLiteDatabase.OpenDatabase(path, null, DatabaseOpenFlags.OpenReadonly);
				}
			}
			catch { }
			return sqliteDB;
		}

		/// <summary>
		/// Проверка, если база данных существует
		/// </summary>
		/// <param name="streamSQLite"></param>
		/// <param name="streamWriter"></param>
		/// <returns></returns>
		private bool CopySQLiteDB(Stream streamSQLite, FileStream streamWriter)
		{
			bool isSuccess = false;
			int length = 256;
			Byte[] buffer = new byte[length];
			try
			{
				int bytesRead = streamSQLite.Read(buffer, 0, length);
				while (bytesRead > 0)
				{
					streamWriter.Write(buffer, 0, bytesRead);
					bytesRead = streamSQLite.Read(buffer, 0, length);
				}
				isSuccess = true;
			}
			catch { }
			finally
			{
				streamSQLite.Close();
				streamWriter.Close();
			}
			return isSuccess;
		}

		public override void OnCreate(SQLiteDatabase db)
		{
			throw new NotImplementedException();
		}

		public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
		{
			throw new NotImplementedException();
		}
	}
}