using System;
using System.IO;
using System.Collections.Generic;

class kiosk {         
    static void Main(string[] args)
    {
        //program takes the catalogue file name as a command line argument
        if (args.Length == 0 || !File.Exists(args[0])){
            System.Console.WriteLine("please enter a valid pricing catalogue.");
            return;
        }

        bool on = true;
        while(on){
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

            checkout(cartFile, args[0]);       

        }

    }

    static void checkout(string cart, string catalogue){
        //read in the content of the cart file
        string[] items = File.ReadAllLines(cart);  
        string[] prices = File.ReadAllLines(catalogue);

        //create a dictionary of the items and their prices 
        IDictionary<string,float> catalogueDict = new Dictionary<string,float>();
        foreach (string price in prices){
            string[] catalogueInfo = price.Split(',');
            string foodItem = catalogueInfo[0];
            float itemPrice = float.Parse(catalogueInfo[1]);
            catalogueDict.Add(foodItem, itemPrice);

            //check if the item is on sale and add it to the dict
            if(catalogueInfo.Length > 2){
                float salePrice = float.Parse(catalogueInfo[2]);
                foodItem = foodItem + "sale";
                catalogueDict.Add(foodItem, salePrice);
            }
        }

        //find the item prices
        float totalPrice = 0;
        string recieptString = "";
        foreach (string item in items){

            if(!catalogueDict.ContainsKey(item)){
                //not sure how this should be handles so I just printed an error message
                System.Console.WriteLine(item + " not found in the catalogue. I guess its free!!");
                continue;
            }

            if(catalogueDict.ContainsKey(item + "sale")){
                //item is on sale use this price instead
                totalPrice = totalPrice + catalogueDict[item + "sale"];
                recieptString = recieptString + item + " = normally: " + catalogueDict[item].ToString() + " now: " + catalogueDict[item + "sale"].ToString() + "\n";
                continue;
            }
            else{
                //item isnt on sale, use normal price
                totalPrice = totalPrice + catalogueDict[item];
                recieptString = recieptString + item + " = " + catalogueDict[item].ToString() + "\n";
            }

        }
        recieptString = recieptString + "\nTotal = " + totalPrice.ToString() + "\nThanks for shopping with us!";  
        System.Console.WriteLine(recieptString);
    }
}
