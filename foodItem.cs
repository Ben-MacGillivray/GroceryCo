
namespace grocerystore
{
    //class to represent one type of food item and all of its possible prices
    public class foodItem{

        private string itemName;
        private float itemPrice;
        private float itemSalePrice;


        //constuctor if no sale price is given
        public foodItem(string name, string price)
        {
            itemName = name;
            itemPrice = float.Parse(price);
            itemSalePrice = -1;
        }

        //overloaded constructor for if a sale price is given
        public foodItem(string name, string price, string salePrice)
        {
            itemName = name;
            itemPrice = float.Parse(price);
            itemSalePrice = float.Parse(salePrice);
        }

        //getters and setters

        public void setItemName(string newName)
        {
            itemName = newName;
        }

        public void setItemPrice(string newPrice)
        {
            itemPrice = float.Parse(newPrice);
        }

        public void setSalePrice(string newSalePrice)
        {
            itemSalePrice = float.Parse(newSalePrice);
        }

        public string getItemName()
        {
            return itemName;
        }

        public float getItemPrice()
        {
            return itemPrice;
        }

            public float getSalePrice()
        {
            return itemSalePrice;
        }

    }
}