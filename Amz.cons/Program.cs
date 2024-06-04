using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Amz.Library;

namespace Amz.cons
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool keepRunning = true;
            do{
            Console.WriteLine("Would you like to access the shop, inventory or quit? (S/I/Q)");
            string input = Console.ReadLine() ?? "S";
            var itemSVC = InventoryServiceProxy.Current;
            switch (input){
            case "I":
            case "i":
            bool keepManaging = true;
            do{
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("(A)dd an item.");
            Console.WriteLine("(V)iew items in the inventory.");
            Console.WriteLine("(U)pdate an item in the inventory.");
            Console.WriteLine("(R)emove an item from the inventory.");
            Console.WriteLine("(Q)uit");
            string response = Console.ReadLine() ?? "A";
                switch(response){
                    case "A":
                    case "a":
                        bool addMore = true;
                        do{
                            Item item = new Item(); 
                            Console.WriteLine("What is the name of your item?");
                            item.Name = Console.ReadLine();
                            Console.WriteLine("What is the descripiton of this item?");
                            item.Description = Console.ReadLine();
                            Console.WriteLine("What is the price?");
                            string? priceInput = Console.ReadLine();
                            double price;
                            if(double.TryParse(priceInput, out price)){
                                item.Price = price; //need to try this
                            }
                            Console.WriteLine("How many items are available?");
                            string? quantInput = Console.ReadLine();
                            int quant;
                            if(int.TryParse(quantInput, out quant)){
                                item.Quantity = quant;
                            }
                            itemSVC?.invAdd(item);

                            itemSVC?.Items?.ToList().ForEach(Console.WriteLine); //ToList makes a deep copy, if you dont want a deep copy cant use tolist
                            Console.WriteLine("Would you like to add another item? (y/n)");
                            string answer = Console.ReadLine()?.ToLower() ?? "y";
                            if(answer != "yes" && answer != "y"){
                                addMore = false;
                            }
                    
                        }while (addMore);
                        break;
                    case "V":
                    case "v":
                        itemSVC?.Items?.ToList().ForEach(Console.WriteLine);
                        break;
                    case "U":
                    case "u":
                        bool updateMore = true;
                        do{
                        Console.WriteLine("Please enter the Id of the item you would like to update.");
                        string IdInput = Console.ReadLine() ?? "0";
                        int IdToUpdate;
                        if(int.TryParse(IdInput, out IdToUpdate))
                        {
                            var item = itemSVC?.FindById(IdToUpdate);
                            if(item != null){
                                Console.WriteLine("Please enter the updated Name");
                                item.Name = Console.ReadLine();
                                Console.WriteLine("Please enter the updated description");
                                item.Description = Console.ReadLine();
                                Console.WriteLine("What is the updated price of the item?");
                                string? priceInput = Console.ReadLine();
                                double price;
                                if(double.TryParse(priceInput, out price)){
                                    item.Price = price; 
                                }
                                Console.WriteLine("What is the updated quantity of the item?");
                                string? quantInput = Console.ReadLine();
                                int quant;
                                if(int.TryParse(quantInput, out quant)){
                                    item.Quantity = quant;
                                }
                                itemSVC?.invAdd(item);
                                Console.WriteLine("Here is your updated inventory!");
                                itemSVC?.Items?.ToList().ForEach(Console.WriteLine);
                            }
                            else{
                                Console.WriteLine("That is an invalid input");
                            }
                            Console.WriteLine("Would you like to update another item? (y/n)");
                            string answer = Console.ReadLine()?.ToLower() ?? "n";
                            if(answer != "yes" && answer != "y"){
                                updateMore = false;
                            }

                            }
                        }while(updateMore);
                        break;
                    case "r":
                    case "R":
                        bool deleteMore = true;
                        do{
                            Console.WriteLine("Please enter the ID of the item you would like to delete.");
                            string _idInput = Console.ReadLine() ?? "0";
                            int IdToDelete;
                            if(int.TryParse(_idInput, out IdToDelete)){
                                itemSVC?.Delete(IdToDelete);
                                Console.WriteLine("The item has been deleted.");
                            }
                            else{
                                Console.WriteLine("Invalid ID entered.");
                            }
                            Console.WriteLine("Would you like to remove another item? (y/n)");
                            string answer = Console.ReadLine()?.ToLower() ?? "n";
                            if(answer != "yes" && answer != "y"){
                                deleteMore = false;
                            }
                            
                        }while(deleteMore);
                        break;
                    case "q":
                    case "Q":
                        keepManaging = false;
                        break;
                
                };
            }while(keepManaging);
            break;
//===================================shop=================================
            case "S":
            case "s":
                bool keepShopping = true;
                do{
                    Console.WriteLine("What would you like to do?");
                    Console.WriteLine("(A)dd to cart");
                    Console.WriteLine("(R)emove from cart");
                    Console.WriteLine("(V)iew cart");
                    Console.WriteLine("(C)heckout");
                    Console.WriteLine("(Q)uit");
                    string response = Console.ReadLine() ?? "A";
                    var cartSVC = CartServiceProxy.Current;
                    switch(response){
                        case "a":
                        case "A":
                            keepShopping = true;
                            do{
                            Console.WriteLine("Here is the current inventory:");
                            itemSVC?.Items?.ToList().ForEach(Console.WriteLine);
                            Console.WriteLine("Please enter the ID of the item you would like to add to your cart.");
                            string itemToAdd = Console.ReadLine() ?? "0";
                            int _idToAdd;
                            if(int.TryParse(itemToAdd, out _idToAdd)){
                                Console.WriteLine("How many would you like to add to your cart?");
                                string newQuant = Console.ReadLine() ?? "1";
                                int _newQuant;
                                if(int.TryParse(newQuant, out _newQuant)){
                                    cartSVC?.CartAdd(_idToAdd, _newQuant);
                                    Console.WriteLine("Item has been added to your cart.");
                                }
                                else{
                                    Console.WriteLine("There is not enough quantity in inventory to do that.");
                                }
                            }
                            else{
                                Console.WriteLine("Item ID not found.");
                            }
                            Console.WriteLine("Would you like to add another item to your cart? (y/n)");
                            string? _shoppingAnswer = Console.ReadLine()?.ToLower() ?? "y";
                            if(_shoppingAnswer != "yes" && _shoppingAnswer != "y"){
                                keepShopping = false;
                            }
                            }while(keepShopping);
                            break;
                        case "r":
                        case "R":
                            bool deleteMore = true;
                            do{
                                Console.WriteLine("Please enter the Id of the item you would like to remove from your cart.");
                                string idToDelete = Console.ReadLine() ?? "0";
                                int _idToDelete;

                                if(int.TryParse(idToDelete, out _idToDelete)){
                                    Console.WriteLine("Please enter the quantity that you would like to remove.");
                                    string quantDelete = Console.ReadLine() ?? "1";
                                    int _quantDelete;
                                    if(int.TryParse(quantDelete, out _quantDelete)){
                                        cartSVC?.Delete(_idToDelete, _quantDelete);
                                        Console.WriteLine("Item removed from cart");
                                    }
                                    else{
                                        Console.WriteLine("Invalid quantity entered.");
                                    }
                                }
                                else{
                                    Console.WriteLine("Invalid Id entered");
                                }
                                Console.WriteLine("Would you like to remove another item? (y/n)");
                                string? toDelete = Console.ReadLine()?.ToLower() ?? "n";
                                if(toDelete != "yes" && toDelete != "y"){
                                    deleteMore = false;
                                }
                            }while(deleteMore);
                            break;
                        case "v":
                        case "V":
                            var cartItems = cartSVC?.CartItems;
                            if(cartItems == null || !cartItems.Any()){
                                Console.WriteLine("Your cart is empty");
                            }
                            else{
                                Console.WriteLine("Here is your cart:");
                                foreach(var cartItem in cartItems){
                                    Console.WriteLine(cartItem);
                                }
                            }
                            break;
                        case "c":
                        case "C":
                            cartSVC.Checkout();
                            break;
                        case "q":
                        case "Q":
                            keepShopping = false;
                            break;
                    }
                }while(keepShopping);
                
                break;
            case "q":
            case "Q":
                keepRunning = false;
                break;
            default:
                Console.WriteLine("Sorry that is not a valid option.");
                break;

            }
        }while(keepRunning);
        }
    }
}