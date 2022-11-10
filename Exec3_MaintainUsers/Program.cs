using ISpan.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Exec3_MaintainUsers
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//Insert("ABC", "132", "123", new DateTime(1995,4,2), 180);
			//Delete(1);
			//Update("CCC",2);
			Select(2);
		}
		
		static void Insert(string name, string account,string password,DateTime dateofBirthd,int height)
		{
			SqlDbHelper dbHelper = new SqlDbHelper("default");
			string sql = "INSERT INTO Users(Name,Account,Password,DateOfBirthd,Height) Values(@Name,@Account,@Password,@DateOfBirthd,@Height)";
			try
			{
				var parameters = new SqlParameterBuilder()
					.AddNvarchar("@Name", 50, name)
					.AddNvarchar("@Account", 50, account)
					.AddNvarchar("@Password", 50, password)
					.AddDateTime("@DateOfBirthd", dateofBirthd)
					.AddInt("Height", height)
					.Build();

				dbHelper.ExecuteNonQuery(sql, parameters);

				Console.WriteLine("紀錄已新增");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"操作失敗，原因：{ex.Message}");
			}
		}
		static void Delete(int id)
		{
			SqlDbHelper dbHelper = new SqlDbHelper("default");

			string sql = "Delete from Users where id=@id";

			try
			{
				var parameters = new SqlParameterBuilder()
						.AddInt("id", id)
						.Build();
				dbHelper.ExecuteNonQuery(sql, parameters);

				Console.WriteLine("紀錄已刪除");
			}
			catch(Exception ex)
			{
				Console.WriteLine($"操作失敗，原因：{ex.Message}");
			}
		}
		static void Update(string name, int id)
		{
			SqlDbHelper DbHelper = new SqlDbHelper("default");

			string sql = "Update users set name=@name where id=@id";

			try
			{
				var parameters = new SqlParameterBuilder()
					.AddNvarchar("@name", 50, name)
					.AddInt("@id", id)
					.Build();

				DbHelper.ExecuteNonQuery(sql, parameters);
				Console.WriteLine("紀錄已更新");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"操作失敗，原因：{ex.Message}");
			}
		}
		static void Select(int inputid)
		{
			var dbhelper = new SqlDbHelper("default");
			string sql = "Select id, name From users where Id=@Id  Order by Id Desc";
			try
			{
				var parameters = new SqlParameterBuilder().AddInt("Id", inputid).Build();
				DataTable users = dbhelper.Select(sql, parameters);

				foreach (DataRow row in users.Rows)
				{
					int id = row.Field<int>("id");
					string name = row.Field<string>("name");

					Console.WriteLine($"Id={id}, Title={name}");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"發生錯誤，原因{ex.Message}");
			}
		}

	}
}
