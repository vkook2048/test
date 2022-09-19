using System;
using System.Text;
using Microsoft.Data.Sqlite;

namespace ForSQLite
{
    class Program
    {
        static void Main(string[] args)
        {
            string sqlExpression = "select * from genres";
            // "select sum(ID) from Films"
            //Console.WriteLine("\tc:\r\n\\p///");
            using (var connection = new SqliteConnection("Data Source=c:\\Users\\Lexa\\Desktop\\SmartGit\\test\\second.s3db"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            var id = reader.GetValue(0);
                            byte[] bytes = new byte[1024];
                            long size = reader.GetBytes(1, 0, bytes, 0, 1024);
                            byte[] bt = new byte[size];
                            for (int i = 0; i < size; i++)
                                bt[i] = bytes[i];
                            string genre = Encoding.Default.GetString(bt);

                            Console.WriteLine($"{id} \t {genre}");
                        }
                    }
                }
            }
            /*SqliteCommand command = new SqliteCommand(sqlExpression, connection);
            var result = command.ExecuteScalar();
            Console.WriteLine(result);*/
        
            Console.Read();
        }
    }
}
