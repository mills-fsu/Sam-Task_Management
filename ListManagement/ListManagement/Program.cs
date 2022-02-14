
using ListManagement.models;
using System2 = System;

namespace ListManagement // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            var items = new List<ToDo>();
            Console.WriteLine("Welcome to the List Management App!");
            ToDo nextTodo = new ToDo();
            

            int input = -1;
            while (input != 7)
            {
                    PrintMenu();
                    if (int.TryParse(Console.ReadLine(), out input)) //==
                    {
                    nextTodo = new ToDo();
                    if (input == 1)
                    {
                        //C - Create
                        //ask for property values
                        Console.WriteLine("Give me a name:");
                        nextTodo.Name = Console.ReadLine();
                        Console.WriteLine("Describe the task:");
                        nextTodo.Description = Console.ReadLine();
                        Console.WriteLine("When is its deadline? (format MUST BE 'MM/DD/YYYY' )");
                        var dateString = Console.ReadLine();
                        try {
                            nextTodo.Deadline = DateTime.ParseExact(dateString, "d", System.Globalization.CultureInfo.InvariantCulture);
                        }
                        catch
                        {
                            Console.WriteLine("Invalid Date Format! Try again.");
                            continue;
                        }
                        Console.WriteLine("Is it completed? (true/false - lowercase) ");
                        var completion = Console.ReadLine();
                        if (completion == "true" || completion == "false")
                        {
                            bool isComplete;
                            bool.TryParse(completion, out isComplete);
                            nextTodo.IsCompleted = isComplete;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Completion Choice! Try again.");
                            continue;
                        }

                        items.Add(nextTodo);
                        Console.WriteLine("Successfully Added!");
                    }
                    else if (input == 2)
                    {
                        //D - Delete/Remove
                        Console.WriteLine("Which item should I delete? (select index, enter to cancel)");
                        int count = 0;
                        foreach (var item in items)
                        {   
                            Console.WriteLine($"[{count}] {item}"); count++;
                        }
                        int strIndex;

                        if (int.TryParse(Console.ReadLine(), out strIndex) && strIndex >= 0 && strIndex < items.Count)
                        {
                            items.RemoveAt(strIndex);
                            Console.WriteLine("Successfully Removed!");
                        }
                        else
                        {
                           Console.WriteLine("Invalid Item Index! Try again.");
                           continue;
                        }
                    } 
                    else if (input == 3)
                    {
                        //U - Update/Edit
                        Console.WriteLine("Which item should I edit? (select index)");
                        int count = 0;
                        foreach (var item in items)
                        {
                            Console.WriteLine($"[{count}] {item}"); count++;
                        }
                        int strIndex;

                        if (int.TryParse(Console.ReadLine(), out strIndex) && strIndex >= 0 && strIndex < items.Count)
                        {

                            Console.WriteLine("Give me a name:");
                            nextTodo.Name = Console.ReadLine();
                            Console.WriteLine("Describe the task:");
                            nextTodo.Description = Console.ReadLine();
                            Console.WriteLine("When is its deadline? (format MUST BE 'MM/DD/YYYY' )");
                            var dateString = Console.ReadLine();
                            var validDate = false;
                            try
                            {
                                nextTodo.Deadline = DateTime.ParseExact(dateString, "d", System.Globalization.CultureInfo.InvariantCulture);
                                validDate = true;
                            }
                            catch
                            {
                                Console.WriteLine("Invalid Date Format! Try again.");
                                continue;
                            }
                            Console.WriteLine("Is it completed? (true/false - lowercase) ");
                            var completion = Console.ReadLine();
                            if (completion == "true" || completion == "false")
                            {
                                bool isComplete;
                                bool.TryParse(completion, out isComplete);
                                nextTodo.IsCompleted = isComplete;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Completion Choice! Try again.");
                                continue;
                            }

                            items.RemoveAt(strIndex);
                            items.Insert(strIndex, nextTodo);


                            Console.WriteLine("Successfully Changed!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Item Index! Try again.");
                            continue;
                        }
                    }
                    else if (input == 4)
                    {
                        //Complete Task
                        Console.WriteLine("Which task has been completed? (select index)");
                        int count = 0;
                        foreach (var item in items)
                        {
                            Console.WriteLine($"[{count}] {item}"); count++;
                        }
                        int strIndex;

                        if (int.TryParse(Console.ReadLine(), out strIndex) && strIndex >= 0 && strIndex < items.Count)
                        {
                            items[strIndex].IsCompleted = true;
                            Console.WriteLine("Successfully Completed!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Item Index! Try again.");
                            continue;
                        }
                    }
                    else if(input == 5)
                    {
                        //R - Read / List uncompleted tasks
                        int count = 0;
                        foreach (var todo in items)
                        {
                            if (todo.IsCompleted == false)
                            {
                                Console.WriteLine($"[{count}] {todo}"); count++;
                            }
                        }

                    } else if (input ==6)
                    {
                        //R - Read / List all tasks
                        int count = 0;
                        foreach (var todo in items)
                        {
                            Console.WriteLine($"[{count}] {todo}"); count++;
                        }
                    } else if (input == 7)
                    {
                        Console.WriteLine("Exiting Program. Bye!");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("I don't know what you mean");
                    }
                }
                else
                {
                    Console.WriteLine("User did not specify a valid int!");
                }
            }

            Console.ReadLine();
        }

        public static void PrintMenu()
        {
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. Delete Item");
            Console.WriteLine("3. Edit Item");
            Console.WriteLine("4. Complete Item");
            Console.WriteLine("5. List All Incomplete Tasks");
            Console.WriteLine("6. List All Tasks");
            Console.WriteLine("7. Exit");
        }
    }
}