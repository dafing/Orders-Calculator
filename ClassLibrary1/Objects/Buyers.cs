using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersCalculatorUtilities
{
    public class Buyers
    {
        public string Name { get; set; }
        public List<Item> Order { get; set; }

        // calculated on list adding
        public decimal ItemWeight { get; set; }
        public decimal ItemCost { get; set; }

        // calculated on final calc
        public decimal ShippingPercent { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalCost { get; set; }

        public decimal ItemPercent { get; set; }
        public decimal CustomsCost { get; set; }

        public Buyers(string name)
        {
            Name = name;
            Order = new List<Item>();
            ItemWeight = 0;
            ItemCost = 0;
            ShippingPercent = 0;
            ShippingCost = 0;
            TotalCost = 0;
            ItemPercent = 0;
            CustomsCost = 0;
        }
        public void CalculateShippingPercent(decimal weight)
        {
            ShippingPercent = Math.Round(ItemWeight / weight, 2, MidpointRounding.AwayFromZero);
        }

        public void CalculateShippingCost(decimal shipping)
        {
            ShippingCost = Math.Round(shipping * ShippingPercent, 2, MidpointRounding.AwayFromZero);
        }

        public void CalculateTotalCost()
        {
            TotalCost = ItemCost + ShippingCost;
        }

        public void CalculateItemPercent(decimal totalcost)
        {
            ItemPercent = Math.Round(ItemCost / totalcost, 2, MidpointRounding.AwayFromZero);
        }

        public void CalculateCustoms(decimal customs)
        {
            CustomsCost = Math.Round(customs * ItemPercent, 2, MidpointRounding.AwayFromZero);
        }
    }
}
