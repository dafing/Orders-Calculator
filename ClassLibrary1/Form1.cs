using OrdersCalculatorUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary1
{
    public partial class Form1 : Form
    {
        public static List<Buyers> buyer = new List<Buyers>();

        public static void Main()
        {
            Application.Run(new Form1());
        }
        public Form1()
        {
            InitializeComponent();
            shippingcost.Text = "0";
            numitem.Text = "0";
            customs.Text = "0";
            handling.Text = "0";
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            label5.BackColor = Color.Transparent;
            label6.BackColor = Color.Transparent;
            label7.BackColor = Color.Transparent;
            label8.BackColor = Color.Transparent;
            label9.BackColor = Color.Transparent;
            label10.BackColor = Color.Transparent;
            numitem.BackColor = Color.Transparent;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Add item button to increment list
        // This button when clicked should create a new Item object and add to the items list
        // Before creating the object, should check if the user input value is proper
        // - if proper, proceed to create object
        // - else, output error message and clear fields
        private void button2_Click(object sender, EventArgs e)
        {
            // check the required fields;
            string name = itemname.Text;
            decimal price;
            string bname;
            decimal w;
            if (itemprice.Text != "")
            {
                if (Decimal.TryParse(itemprice.Text, out price))
                {
                    goto CheckBName;
                }
                else
                {
                    richTextBox1.Text = "Error : Please use numerical values in Item Price field.";
                    goto Exit;
                }
            }
            else
            {
                richTextBox1.Text = "Error : Please do not leave item price empty.";
                goto Exit;
            }

            CheckBName:

            if (buyername.Text != "")
            {
                bname = buyername.Text;
                goto CheckWeight;
            }
            else
            {
                richTextBox1.Text = "Error : Please do not leave the buyer's name empty.";
                goto Exit;
            }

            CheckWeight:

            if (weight.Text != "")
            {
                if (Decimal.TryParse(weight.Text, out w))
                {
                    goto AddItem;
                }
                else
                {
                    richTextBox1.Text = "Error : Please use numerical values in Weight field.";
                    goto Exit;
                }
            }
            else
            {
                richTextBox1.Text = "Error : Please do no leave the item weight empty.";
                goto Exit;
            }

            AddItem:
            Item i = new Item(name, price, bname, w);
            // check if buyer exists
            for (int j = 0; j < buyer.Count; j++)
            {
                if (buyer[j].Name == bname)
                {
                    buyer[j].Order.Add(i);
                    buyer[j].ItemWeight += w;
                    buyer[j].ItemCost += price;
                    numitem.Text = (Convert.ToInt32(numitem.Text) + 1).ToString();
                    goto Exit;
                }
            }

            // if it reaches here, that mean need to create new buyer
            Buyers b = new Buyers(bname);

            b.Order.Add(i);
            b.ItemWeight += w;
            b.ItemCost += price;
            buyer.Add(b);
            numitem.Text = (Convert.ToInt32(numitem.Text) + 1).ToString();

            Exit:
            itemname.Text = "";
            itemprice.Text = "";
            buyername.Text = "";
            weight.Text = "";

        }

        // Calculate button for final calculation
        // This button is used to do final shipping share calculation
        // Will output results the item value, shipping share, and total value onto the textbox
        // Results will be sorted by name
        private void button3_Click(object sender, EventArgs e)
        {
            decimal shipping;
            decimal c;
            decimal totalw = 0;
            decimal totalc = 0;
            bool calcc = false;
            // first check if shipping amount is valid
            if (decimal.TryParse(shippingcost.Text, out shipping))
            {
                // find out the total weight of the items
                for (int i = 0; i < buyer.Count; i++)
                {
                    totalw += buyer[i].ItemWeight;
                    totalc += buyer[i].ItemCost;
                }

                // check if customs is 0 or not
                if (decimal.TryParse(customs.Text, out c))
                {
                    if (c > 0)
                        calcc = true;
                }

                // after finding total weight, loop through each buyer in the buyer list
                // calculate their shipping percentage, shipping cost, and total $ value
                for (int j = 0; j < buyer.Count; j++)
                {
                    // calculate shipping percent, etc through object method
                    buyer[j].CalculateShippingPercent(totalw);
                    buyer[j].CalculateShippingCost(shipping);
                    buyer[j].CalculateTotalCost();

                    if (calcc)
                    {
                        buyer[j].CalculateItemPercent(totalc);
                        buyer[j].CalculateCustoms(c);
                    }
                }
                PrintResults(totalw, shipping, calcc);
            }
            else
            {
                richTextBox1.Text = "Error : Please enter a numerical amount for shipping.";
            }

        }

        // Restart everything button
        // This button wipes all current lists and resets all fields to their default.
        private void button1_Click(object sender, EventArgs e)
        {
            // Delete all items in the lists
            buyer.Clear();

            // clear or reset all fields in the form
            numitem.Text = "0";
            shippingcost.Text = "0";

        }
        
        private void PrintResults(decimal totalw, decimal shipping, bool calcc)
        {
            string output;
            decimal h;
            bool addh = false;
            output = "Shipping Cost : " + shipping.ToString() + "\n" + "Total weight : " + totalw.ToString() + "\n";

            if (decimal.TryParse(handling.Text, out h))
            {
                if (h > 0)
                {
                    h = h / buyer.Count;
                    addh = true;
                }
            }

            // loop through buyers and append info to output.

            for (int i = 0; i < buyer.Count; i++)
            {
                output += "\n" + "Name : " + buyer[i].Name + "\n";
                output += "Order Cost : " + buyer[i].ItemCost + "\n";
                output += "Order Weight : " + buyer[i].ItemWeight.ToString() + "\n";
                output += "Shipping Share Percent : " + (buyer[i].ShippingPercent * 100).ToString() + "%" + "\n";
                output += "Shipping Cost : " + buyer[i].ShippingCost.ToString() + "\n";
                output += "Total Cost : " + buyer[i].TotalCost.ToString() + "\n";

                if (calcc)
                {
                    output += "Items Percent : " + (buyer[i].ItemPercent * 100).ToString() + "%" + "\n";
                    output += "Customs Cost : $" + buyer[i].CustomsCost.ToString() + "\n";
                }

                if (addh)
                {
                    output += "Handling Cost : $" + h.ToString() + "\n";
                }
            }

            richTextBox1.Text = output;
        }

        // Clear button to clear up the textbox
        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Refresh();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
