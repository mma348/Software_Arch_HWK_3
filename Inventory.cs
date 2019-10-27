using System;
using System.Data.SQLite;
using System.Collections.Generic;

namespace SoftwareArch.OSC{
    class Inventory
    {
        private Database databaseConnection;
        private List<Item> inventoryList;

        public Inventory()
        {
            databaseConnection = new Database();
            inventoryList = GetCurrentInventory();
        }

        public List<Item> GetCurrentInventory()
        {
            List<Item> items = new List<Item>();
            string query = "SELECT * FROM INVENTORY";
            SQLiteDataReader result = databaseConnection.ExecuteQuery(query);

            if (result.HasRows)
            {
                while (result.Read())
                {
                    Item item = new Item(result["name"].ToString(), result["productID"].ToString(), result["description"].ToString(), (int)result["quantity"], (float)result["price"]);
                    items.Add(item);
                }
            }
            return items;
        }

        public void DisplayCurrentInventory()
        {
            foreach (Item item in inventoryList){
                Console.WriteLine("Name:  {0}  |  Price:  {1}  |  Quanity Available:  {2}  |  ID:  {3}", item.Name, item.Price, item.Quantity, item.Id);
            }
        }

        public Item GetItemByID(string productID)
        {
            string query = "SELECT * FROM INVENTORY WHERE productID ='" + productID + "'";
            SQLiteDataReader result = databaseConnection.ExecuteQuery(query);
            
            if (result.HasRows)
            {
                Item item = new Item(result["name"].ToString(), result["productID"].ToString(), result["description"].ToString(), (int)result["quantity"], (float)result["price"]);

                Console.WriteLine("Item Selected: {0}", item.Name);
                Console.WriteLine("Description: {0}", item.Description);
                Console.WriteLine("Quantity: {0}", item.Quantity);

                
                return item;
            }
            else
            {
                Console.WriteLine("Item not found");
                Console.WriteLine("Please enter a correct product ID from the store");
                productID = Console.ReadLine();
                return GetItemByID(productID);
            }
        }
    }
}