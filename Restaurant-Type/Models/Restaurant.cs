using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RestaurantType;

namespace RestaurantType.Models
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private string _location;
    private int category_id;

    public Restaurant(string newName, string newLocation, int newcat, int id = 0)
    {
      _name = newName;
      _location = newLocation;
      category_id = newcat;
      _id = id;
    }
    public override bool Equals(System.Object otherRestaurant)
    {
      if (!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool idEquality = (this.GetId() == newRestaurant.GetId());
        bool nameEquality = (this.GetName() == newRestaurant.GetName());
        bool locationEquality = (this.GetLocation() == newRestaurant.GetLocation());
        bool catIdEquality = (this.GetCategoryId() == newRestaurant.GetCategoryId());
        return (idEquality && locationEquality && catIdEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
        return this.GetId().GetHashCode();
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetLocation()
    {
      return _location;
    }
    public int GetCategoryId()
    {
      return category_id;
    }

    public static List<Restaurant> GetAll()
    {
      List<Restaurant> allRestaurants = new List<Restaurant> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurants";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        string restaurantLocation = rdr.GetString(2);
        int restaurantCatId = rdr.GetInt32(3);
        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantLocation, restaurantCatId, restaurantId);
        allRestaurants.Add(newRestaurant);
      }

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allRestaurants;
    }

    // public static Restaurant Find(int id)
    // {
    // }
    //
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM restaurants";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }
    //
    public void Save()
    {
      MySqlConnection conn= DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO restaurants (name, location, category_id) VALUES (@name, @location, @catid)";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter location = new MySqlParameter();
      location.ParameterName = "@location";
      location.Value = this._location;
      cmd.Parameters.Add(location);

      MySqlParameter catid = new MySqlParameter();
      catid.ParameterName = "@catid";
      catid.Value = this.category_id;
      cmd.Parameters.Add(catid);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    //
    // public void Edit()
    // {
    // }
    //
    // public void Delete()
    // {
    // }

  }
}
