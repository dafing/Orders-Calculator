using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersCalculatorUtilities
{

    // This is an Item object to represent each item associated with the buyer
    // Each item has the properties ItemName, Price, BuyerName and Weight

    public class Item
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public string BuyerName { get; set; }
        public decimal Weight { get; set; }

        public Item(string name, decimal price, string bname, decimal weight)
        {
            ItemName = name;
            Price = price;
            BuyerName = bname;
            Weight = weight;
        }
    }
}
