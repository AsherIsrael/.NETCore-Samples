using System;
using System.Collections.Generic;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace DbConnection
{
    public class DbConnector
    {

        public static List<Dictionary<string, object>> ExecuteQuery(string queryString)
        {
            using(MySqlConnection connection = new MySqlConnection(
                "Server=localhost;Database=quotes;UserID=root;Password=root;"))
            {
                connection.Open();
                
                using(MySqlCommand command = new MySqlCommand(queryString, connection))
                {
                    try
                    {
                        List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
                        using(DbDataReader rdr = command.ExecuteReader())
                        {
                            while(rdr.Read())
                            {
                                Dictionary<string, object> dict = new Dictionary<string, object>();
                                for( int i = 0; i < rdr.FieldCount; i++ ) {
                                    dict.Add(rdr.GetName(i), rdr.GetValue(i));
                                }
                                result.Add(dict);
                            }
                            return result;
                        }
                    }
                    catch
                    {
                       Console.WriteLine("something went wrong"); 
                        return new List<Dictionary<string, object>>();
                    }
                }
            }
        }
    }
}