using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using RestaurantType.Models;

namespace RestaurantType.Tests
{
  [TestClass]
  public class RestaurantTests : IDisposable
  {
    public RestaurantTests()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=restauranttype_test;";
    }
    [TestMethod]
    public void TwoRestsAreEqual()
    {
      Restaurant rest1 = new Restaurant("a", "b", 1, 2);
      Restaurant rest2 = new Restaurant("a", "b", 1, 2);

      Assert.AreEqual(rest1, rest2);
    }
    [TestMethod]
    public void Save_SavesToDB_RestaurantList()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Pizza Hut", "Main St", 1);
      //Act
      testRestaurant.Save();
      // Assert.IsTrue(testRestaurant.GetId() != 0);
      List<Restaurant> resultList = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};
      Console.WriteLine(resultList);
      Console.WriteLine(testList);
      //Assert
      CollectionAssert.AreEqual(resultList, testList);
    }
    [TestMethod]
    public void GetAll_ReturnsAllRestaurants_RestaurantList()
    {
      //Arrange
      Restaurant newRestaurant1 = new Restaurant("Pizza Hut", "Main St", 1);
      Restaurant newRestaurant2 = new Restaurant("Dominoes","Seattle", 1);
      List<Restaurant> testList = new List<Restaurant>{newRestaurant1, newRestaurant2};
      //Act
      newRestaurant1.Save();
      newRestaurant2.Save();
      List<Restaurant> result = Restaurant.GetAll();
      //Assert
      CollectionAssert.AreEqual(result, testList);
    }
    [TestMethod]
    public void Find_FindsCorrectRestaurant_Restaurant()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Pizza Hut", "Main St", 1);
      //Act
      testRestaurant.Save();
      Restaurant result = Restaurant.Find(testRestaurant.GetId());
      //Assert
      Assert.AreEqual(result, testRestaurant);
    }
    [TestMethod]
    public void Delete_DeletesRestaurantFromDB_RestaurantList()
    {
      Restaurant newRestaurant = new Restaurant("Pizza Hut", "Main St", 1);
      newRestaurant.Save();
      List<Restaurant> resultList = new List<Restaurant>{};
      newRestaurant.Delete();
      CollectionAssert.AreEqual(resultList, Restaurant.GetAll());
    }
    [TestMethod]
    public void Edit_ChangesRestaurantName_String()
    {
      string newLocation = "Hell";
      string firstRestaurant = "Taco Bell";
      Restaurant testRestaurant = new Restaurant(firstRestaurant, newLocation, 1);
      testRestaurant.Save();
      string secondRestaurant = "Pizza Hut";

      testRestaurant.Edit(secondRestaurant, newLocation, 1);

      string result = Restaurant.Find(testRestaurant.GetId()).GetName();

      Assert.AreEqual(result, secondRestaurant);
    }
    [TestMethod]
    public void Edit_ChangesRestaurantLocation_String()
    {
      string firstLocation = "Hell";
      string newRestaurant = "Taco Bell";
      Restaurant testRestaurant = new Restaurant(newRestaurant, firstLocation, 1);
      testRestaurant.Save();
      string secondLocation="Heaven";

      testRestaurant.Edit(newRestaurant, secondLocation, 1);

      string result = Restaurant.Find(testRestaurant.GetId()).GetLocation();

      Assert.AreEqual(result, secondLocation);
    }
    public void Dispose()
    {
      Restaurant.DeleteAll();
    }

  }

}
