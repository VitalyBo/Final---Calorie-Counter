using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;


//Vitalii Bobyr - 06/16/24
//Programming 120 - Code Practice - Final - Calorie Counter

namespace Сonsole
{

    #region MAIN_Prog
    class Program
    {

        static FoodItem[] foodItems = new FoodItem[14];

        public static void Main(string[] args)
        {
            Preload();
            Menu();
        }

        static void Preload()
        {
            foodItems[0] = new FoodItem("Apple",1,95,1);
            foodItems[1] = new FoodItem("Banana",1,105,2);
            foodItems[2] = new FoodItem("Apple", 1,95,1);
            foodItems[3] = new FoodItem("Banana",1,105,2);
            foodItems[4] = new FoodItem("Carrot",2,25,3);
            foodItems[5] = new FoodItem("Broccoli",2,55,2);
            foodItems[6] = new FoodItem("Chicken",3,165,1);
            foodItems[7] = new FoodItem("Beef",3,250,1);
            foodItems[8] = new FoodItem("Rice",4,205,1);
            foodItems[9] = new FoodItem("Bread",4,80,2);
            foodItems[10] = new FoodItem("Milk",5,150,1);
            foodItems[11] = new FoodItem("Cheese",5,110,2);


        }

        static void DisplayAllFoodItems()
        {
            foreach (var item in foodItems)
            {
                if (item != null)
                {
                    Console.WriteLine(item.DisplayInformation());
                    Console.WriteLine();
                }
            }
        }

        static FoodItem MakeNewItem()
        {
            Console.Write("Enter the name of the new food item: ");
            string name = Console.ReadLine();

            int category;
            while (true)
            {
                Console.WriteLine("Select a category of food item:");
                Console.WriteLine("0 - Fruit\n1 - Vegetable\n2 - Protein\n3 - Grain\n4 - Dairy");
                if (int.TryParse(Console.ReadLine(), out category) && category >= 0 && category <= 4)
                    break;
                else
                    Console.WriteLine("Invalid category. Please enter a number between 0 and 4.");
            }

            int calories;
            while (true)
            {
                Console.Write("Enter the number of calories: ");
                if (int.TryParse(Console.ReadLine(), out calories))
                    break;
                else
                    Console.WriteLine("Invalid input. Please enter a valid number.");
            }

            int quantity;
            while (true)
            {
                Console.Write("Enter the quantity: ");
                if (int.TryParse(Console.ReadLine(), out quantity))
                    break;
                else
                    Console.WriteLine("Invalid input. Please enter a valid number.");
            }

            return new FoodItem(name, category, calories, quantity);
        }

        static int FindEmptyIndex()
        {
            for (int i = 0; i < foodItems.Length; i++)
            {
                if (foodItems[i] == null)
                    return i;
            }
            return -1;
        }

        static void IncreaseArraySize()
        {
            FoodItem[] newArray = new FoodItem[foodItems.Length * 2];
            for (int i = 0; i < foodItems.Length; i++)
            {
                newArray[i] = foodItems[i];
            }
            foodItems = newArray;
        }

        public static void AddItem()
        {
            FoodItem newItem = MakeNewItem();

            int firstIndex = FindEmptyIndex();

            if (firstIndex == -1)
            {
                IncreaseArraySize();
                firstIndex = FindEmptyIndex();
            }

            foodItems[firstIndex] = newItem;
        }

        static double TotalCaloriesEaten()
        {
            double totalCalories = 0;
            foreach (var item in foodItems)
            {
                if (item != null)
                    totalCalories += item.TotalCalories();
            }
            return totalCalories;
        }

        static double AverageCaloriesEaten()
        {
            double totalCalories = 0;
            int itemCount = 0;
            foreach (var item in foodItems)
            {
                if (item != null)
                {
                    totalCalories += item.TotalCalories();
                    itemCount++;
                }
            }
            return itemCount > 0 ? totalCalories / itemCount : 0;
        }

        static void DisplayByCategory()
        {
            int category;
            while (true)
            {
                Console.WriteLine("Select a category to display:");
                Console.WriteLine("0 - Fruit\n1 - Vegetable\n2 - Protein\n3 - Grain\n4 - Dairy");
                if (int.TryParse(Console.ReadLine(), out category) && category >= 0 && category <= 4)
                    break;
                else
                    Console.WriteLine("Invalid category. Please enter a number between 0 and 4.");
            }

            foreach (var item in foodItems)
            {
                if (item != null && item.Category == category)
                {
                    Console.WriteLine(item.DisplayInformation());
                    Console.WriteLine();
                }
            }
        }

        static void DisplayItemWithName(string foodName)
        {
            bool found = false;
            foreach (var item in foodItems)
            {
                if (item != null && item.Name.Equals(foodName, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(item.DisplayInformation());
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Console.WriteLine($"The name '{foodName}' doesn't exist.");
            }
        }
        #endregion

        #region Menu
        static void Menu()
        {
             
            
                start:
                while (true)
                {
                  try
                  {

                    Console.WriteLine("Menu Options:");
                    Console.WriteLine("1. Display all the calories you have eaten");
                    Console.WriteLine("2. Add New Items");
                    Console.WriteLine("3. Calculate your total calories eaten");
                    Console.WriteLine("4. Calculate the average calories of an item you've eaten");
                    Console.WriteLine("5. Display all food eaten of a certain category");
                    Console.WriteLine("6. Search for a food item by name");
                    Console.WriteLine("7. Exit");
                    Console.Write("Please select an option (1-7): ");

                    int choice;
                    if (int.TryParse(Console.ReadLine(), out choice))
                    {
                        switch (choice)
                        {
                            case 1: DisplayAllFoodItems(); break;
                            case 2: AddItem(); break;
                            case 3: Console.WriteLine($"Your total calories are {TotalCaloriesEaten()}"); break;
                            case 4: Console.WriteLine($"Your average calories are {AverageCaloriesEaten()}"); break;
                            case 5: DisplayByCategory(); break;
                            case 6: Console.Write("Please enter a food name: "); string foodName = Console.ReadLine(); DisplayItemWithName(foodName); break;
                            case 7: return;
                            default: Console.WriteLine("Invalid option. Please select a number between 1 and 7."); break;
                        }
                    }
                    else { Console.WriteLine("Invalid input. Please enter a number between 1 and 7."); }
                  }

                  catch
                  {
                    Console.WriteLine("OOooooooopsssss something going wrong try again!!!!");
                    goto start;
                  }


                }
            

            
        }
        #endregion
    }


    #region FoodItem_Class
    class FoodItem
    {
            public string Name;
            public int Category;
            public int Calories;
            public int Quantity;

            public FoodItem()
            {
                Name = "No Item Listed";
                Category = -1;
                Calories = -1;
                Quantity = -1;
            }

            public FoodItem(string name, int category, int calories, int quantity)
            {
                Name = name;
                Category = category;
                Calories = calories;
                Quantity = quantity;
            }

            public double TotalCalories()
            {
                return Calories * Quantity;
            }

            public string CategoryName()
            {
                switch (Category)
                {
                    case 0: return "Fruit";
                    case 1: return "Vegetable";
                    case 2: return "Protein";
                    case 3: return "Grain";
                    case 4: return "Dairy";
                    default: return "No Category Chosen";
                }
            }

            public string DisplayInformation()
            {
                return $"Name: {Name}\nCategory: {CategoryName()}\nCalories: {Calories}\nQuantity: {Quantity}\nTotal Calories: {TotalCalories()}";
            }
    }
    #endregion
}


