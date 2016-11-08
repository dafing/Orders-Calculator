# Orders-Calculator
This is a simple program I created to help me and my friends calculate who pays what amount for a group purchase.  Feel free to use it!

How to use:

Enter numerical amounts in Shipping, Customs and Handling fee.

There is an item object with 4 fields:
- item name
- buyer name (required field)
- item price (required field)
- weight of item (required field)

When done providing info for 1 item, click add and item count will automatically increment

Shipping per person is calculated by total_order_weight/total_overall_weight * shipping price
Customs is calculated by total_order_price/total_overall_price * custom fee
Handling is split evenly between the number of people in the order

This program does not save any data after closing.
