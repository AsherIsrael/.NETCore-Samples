using System.Collections.Generic;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace DbConnection
{
    public class DbConnector
    {

        public static List<Dictionary<string, object>> ExecuteQuery(string queryString)
        {
            using(var connection = new MySqlConnection(
                "Server=localhost;Database=efStudents;UserID=root;Password=root;"))
            {
                connection.Open();
                
                using(var command = new MySqlCommand(queryString, connection))
                {
                    var result = new List<Dictionary<string, object>>();
                    using(DbDataReader rdr = command.ExecuteReader())
                    {
                        while(rdr.Read())
                        {
                            var dict = new Dictionary<string, object>();
                            for( int i = 0; i < rdr.FieldCount; i++ ) {
                                dict.Add(rdr.GetName(i), rdr.GetValue(i));
                            }
                            result.Add(dict);
                        }
                    }
                    return result;
                }
            }
        }
    }
}