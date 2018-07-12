using System;
using MySql.Data.MySqlClient;
using RestaurantType;

namespace RestaurantType
{
    public class DB
    {
      public static MySqlConnection Connection()
      {
        MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
        return conn;
      }
    }
}
