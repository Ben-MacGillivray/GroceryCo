The program takes the name of the catalogue file as a command line argument but the file can be modified while the program is running and the next checkout will recognize those changes. The "basket" of items being checked out is a file provided by the user while the program is running. 

I ran the program with the following commands:

1. mcs kiosk.cs foodItem.cs 
2. mono kiosk.exe catalogue.txt

As you can see in the commit history, I initially had a slightly different implementation for this problem. After some thought I decided to create a class to represent the items in the catalogue rather than using a dictionary. While this isn't strictly necessary to meet the requirements of the assignment, and it may be marginally more expensive, it hopefully makes the code easier to expand in the future.


The expected format of the catalogue file is: 

item,price
item,price,saleprice  if applicable

with no whitespace at the bottom

Apologies in advance if any of the syntax is not up to standard, this is my first time writing c#.

Thanks for the opportunity to interview!

