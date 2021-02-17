using System;
using System.IO;
using System.Collections.Generic;
using grocerystore;

namespace grocerystore
{
    class kiosk {         
        static void Main(string[] args)
        {
            //program takes the catalogue file name as a command line argument
            if (args.Length == 0 || !File.Exists(args[0])){
                System.Console.WriteLine("please enter a valid pricing catalogue.");
                return;
            }

            //create a list of food objects from the catalogue
            string catalogueFile = args[0];
            List<foodItem> catalogue = createCatalogue(catalogueFile);

            //get the current datetime
            DateTime startTime = DateTime.Now;

            while(true){
                //get the name of the basket file from the user
                System.Console.WriteLine("enter the name of the basket file or type 'q' to exit");
                string cartFile = Console.ReadLine();

                //allow the user to exit if they choose
                if(cartFile == "q"){
                    return;
                }

                //check the user entered a valid file
                if (!File.Exists(cartFile)){
                    System.Console.WriteLine("invalid file name.");
                    continue;
                }

                //get the last moddified time
                DateTime lastMod = File.GetLastWriteTime(catalogueFile);
                int changed = DateTime.Compare(startTime, lastMod);

                //if the catalogue has been updated during the program re-create it
                if(changed < 0){
                    startTime = lastMod;
                    catalogue = createCatalogue(catalogueFile);
                }

                //perform the checkout
                checkout(cartFile, catalogue);       

            }

        }

        static void checkout(string cart, List<foodItem> catalogueList){

            //read in the content of the cart file
            string[] items = File.ReadAllLines(cart);  

            //find the item prices
            float totalPrice = 0;
            string receiptString = "";
            foreach (string item in items){
                //find the index of the object correlating to the food item
                int index = catalogueList.FindIndex(food => food.getItemName() == item);

                //check if that item is in the catalogue
                if(!(index >= 0))
                {
                    //not sure how this should be handled so I just printed an error message
                    System.Console.WriteLine(item + " not found in the catalogue. I guess its free!!");
                    continue;
                }
                else
                {
                    var food = catalogueList[index];
                    //check if there is a sale on the item
                    if (food.getSalePrice() < 0){
                        //item isnt on sale
                        receiptString += food.getItemName() + " = $" + food.getItemPrice().ToString("0.00") + "\n";
                        totalPrice += food.getItemPrice();
                    }
                    else{
                        //item is on sale
                        receiptString += food.getItemName() + " = Regular Price: $" + food.getItemPrice().ToString("0.00") + " Sale Price: $" + food.getSalePrice().ToString("0.00") + "\n"; 
                        totalPrice +=food.getSalePrice();
                    }
                }
            }
            //print final results
            receiptString = receiptString + "\nTotal = $" + totalPrice.ToString("0.00") + "\nThanks for shopping with us!\n\n";  
            System.Console.WriteLine(receiptString);
        }

        static List<foodItem> createCatalogue(string catalogue){

            //read in the file
            string[] prices = File.ReadAllLines(catalogue);

            //create a list of the items and their prices 
            List<foodItem> catalogueList = new List<foodItem>();
            foreach(string price in prices){
                string[] catalogueInfo = price.Split(',');
                string itemName = catalogueInfo[0];
                string itemPrice = catalogueInfo[1];

                //check if the item is on sale and add it to the list
                if(catalogueInfo.Length > 2){
                    string salePrice = catalogueInfo[2];
                    grocerystore.foodItem foodObject = new foodItem(itemName, itemPrice, salePrice);
                    catalogueList.Add(foodObject);
                    continue;
                }

                //if the item isnt on sale add it to the list
                grocerystore.foodItem foodObj = new foodItem(itemName, itemPrice);
                catalogueList.Add(foodObj);
            }

            return catalogueList;
        }
    }
}
