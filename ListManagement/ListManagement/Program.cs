using Library.ListManagement.helpers;
using ListManagement.models;
using ListManagement.services;
using Newtonsoft.Json;
using System2 = System;

namespace ListManagement // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            var persistencePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\SaveData.json";
            var itemService = ItemService.Current;
            //var listNavigator = new ListNavigator<Item>(itemService.Items, 2);
            Console.WriteLine("Welcome to the List Management App");

            PrintMenu();

            int input = -1;
            //if(int.TryParse(Console.ReadLine(),out input)) {
                while (input != 9) //==
                {
                    string userChoice = Console.ReadLine();
                    int.TryParse(userChoice, out input);
                    ToDo nextTodo = new ToDo();
                    Appointment nextAppointment = new Appointment();
                    if (input == 1)
                    {

                    //C - Create
                    //ask for property values
                    Console.WriteLine("Is item an appointment or a task? (A/T)");
                    string taskChoice = Console.ReadLine();
                    if (taskChoice == "A")
                    {
                        if (FillCalendarProperties(nextAppointment))
                        {
                            //Console.WriteLine(nextTodo);
                            itemService.Add(nextAppointment);
                            Console.WriteLine("------\nSuccessfully Added Appointment!\n------");
                        }
                    }
                    else if (taskChoice == "T")
                    {
                        if (FillProperties(nextTodo))
                        {
                            //Console.WriteLine(nextTodo);
                            itemService.Add(nextTodo);
                            Console.WriteLine("------\nSuccessfully Added Task!\n------");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Choice");
                    }


                    

                    }
                    else if (input == 2)
                    {
                        //D - Delete/Remove
                        itemService.ShowComplete = true;
                        var userSelection = string.Empty;
                        while (userSelection != "E")
                        {
                            Console.WriteLine("Which item should I delete?");
                            foreach (var item in itemService.GetPage())
                            {
                                Console.WriteLine(item);
                            }
                            if (itemService.GetPage().Count() == 0)
                            {
                                Console.WriteLine("There are currently no tasks.");
                            }
                            Console.WriteLine("------\nN - Next Page\nP - Previous Page\nE - Exit to Menu\n------");
                            userSelection = Console.ReadLine();
                            if (userSelection == "N")
                            {
                                itemService.NextPage();
                            }
                            else if (userSelection == "P")
                            {
                                itemService.PreviousPage();
                            }
                            else if (userSelection == "E")
                            { }
                            else if (int.TryParse(userSelection, out int selection))
                            {
                                var selectedItem = itemService.Items[selection - 1];
                                Console.WriteLine(selectedItem);


                                if (selectedItem != null)
                                {
                                    itemService.Remove(selectedItem);
                                    Console.WriteLine("------\nSuccessfully Deleted!\n------");
                                }
                            }

                            else
                            {
                                Console.WriteLine("Sorry, I can't find that item!");
                            }

                        }
                    } 
                    else if (input == 3)
                    {
                        //U - Update/Edit
                        var userSelection = string.Empty;
                        while (userSelection != "E")
                        {
                            Console.WriteLine("Which item should I edit?");
                            foreach (var item in itemService.GetPage())
                            {
                                Console.WriteLine(item);
                            }
                            if (itemService.GetPage().Count() == 0)
                            {
                                Console.WriteLine("There are currently no tasks.");
                            }
                            Console.WriteLine("------\nN - Next Page\nP - Previous Page\nE - Exit to Menu\n------");
                            userSelection = Console.ReadLine();
                            if (userSelection == "N")
                            {
                                itemService.NextPage();
                            }
                            else if (userSelection == "P")
                            {
                                itemService.PreviousPage();
                            }
                            else if (userSelection == "E")
                            { }
                            else if (int.TryParse(userSelection, out int selection))
                            {

                                if (itemService.Items[selection - 1] is ToDo) {
                                    var selectedItem = itemService.Items[selection - 1] as ToDo;
                                    if (selectedItem != null)
                                        {
                                        FillProperties(selectedItem);
                                        Console.WriteLine("------\nSuccessfully Edited Task!\n------");
                                    }
                                }
                                else
                                {
                                    var selectedItem = itemService.Items[selection - 1] as Appointment;
                                    if (selectedItem != null)
                                    {
                                        FillCalendarProperties(selectedItem);
                                        Console.WriteLine("------\nSuccessfully Edited Appointment!\n------");
                                    }
                                }

                            }

                            else
                            {
                                Console.WriteLine("Sorry, I can't find that item!");
                            }

                        }

                    }
                    else if (input == 4)
                    {
                        //Complete Task
                        itemService.ShowComplete = false;
                        var userSelection = string.Empty;
                        while (userSelection != "E")
                        {
                            Console.WriteLine("Which item should I complete?");
                            foreach (var item in itemService.GetPage())
                            {
                                Console.WriteLine(item);
                            }
                            if (itemService.GetPage().Count() == 0)
                            {
                                Console.WriteLine("There are currently no tasks.");
                            }
                            Console.WriteLine("------\nN - Next Page\nP - Previous Page\nE - Exit to Menu\n------");
                            userSelection = Console.ReadLine();
                            if (userSelection == "N")
                            {
                                itemService.NextPage();
                            }
                            else if (userSelection == "P")
                            {
                                itemService.PreviousPage();
                            }
                            else if (userSelection == "E")
                            { }
                            else if (int.TryParse(userSelection, out int selection))
                            {
                                var selectedItem = itemService.Items[selection - 1] as ToDo;
                                Console.WriteLine(selectedItem);

                                if (selectedItem != null)
                                {
                                    selectedItem.IsCompleted = true;
                                    Console.WriteLine("------\nSuccessfully Completed!\n------");
                                }
                            }

                            else
                            {
                                Console.WriteLine("Sorry, I can't find that item!");
                            }

                        }
                    }
                    else if(input ==5)
                    {
                        //R - Read / List uncompleted tasks

                        //use LINQ
                        itemService.ShowComplete = false;
                        var userSelection = string.Empty;
                        while (userSelection != "E")
                        {
                            foreach (var item in itemService.GetPage())
                            {
                                Console.WriteLine(item);
                            }
                            if (itemService.GetPage().Count() == 0)
                            {
                                Console.WriteLine("There are no uncompleted tasks.");
                            }
                            Console.WriteLine("------\nN - Next Page\nP - Previous Page\nE - Exit to Menu\n------");
                            userSelection = Console.ReadLine();

                            if (userSelection == "N")
                            {
                                itemService.NextPage();
                            }
                            else if (userSelection == "P")
                            {
                                itemService.PreviousPage();
                            }
                        }

                    } else if (input ==6)
                    {
                        //R - Read / List all tasks
                        //itemService.Items.ForEach(Console.WriteLine);
                        itemService.ShowComplete = true;
                        var userSelection = string.Empty;
                        while(userSelection != "E")
                        {
                            foreach (var item in itemService.GetPage())
                            {
                                Console.WriteLine(item);
                            }
                            if (itemService.GetPage().Count() == 0)
                            {
                                Console.WriteLine("There are no tasks.");
                            }
                            Console.WriteLine("------\nN - Next Page\nP - Previous Page\nE - Exit to Menu\n------");
                            userSelection = Console.ReadLine();

                            if (userSelection == "N")
                            {
                                itemService.NextPage();
                            }
                            else if (userSelection == "P")
                            {
                                itemService.PreviousPage();
                            }
                        }
                        

                    } else if (input ==7)
                    {
                        itemService.Save();
                    } else if (input == 8)
                    {
                        itemService.ShowComplete = true;
                        Console.WriteLine("Enter Search Query:");
                        string? Query = Console.ReadLine();
                        itemService.Query = Query;
                        var userSelection = string.Empty;
                        while (userSelection != "E")
                        {
                            foreach (var item in itemService.GetPage())
                            {
                                Console.WriteLine(item);
                            }
                            if (itemService.GetPage().Count() == 0)
                            {
                                Console.WriteLine("0 results.");
                            }
                            Console.WriteLine("------\nN - Next Page\nP - Previous Page\nE - Exit to Menu\n------");
                            userSelection = Console.ReadLine();

                            if (userSelection == "N")
                            {
                                itemService.NextPage();
                            }
                            else if (userSelection == "P")
                            {
                                itemService.PreviousPage();
                            }
                        }
                        itemService.Search("");
                }
                    else if (input == 9)
                    {
                        
                    }
                    else
                    {
                        Console.WriteLine("I don't know what you mean");
                    }

                    PrintMenu();
                    
                }

            Console.WriteLine("Exiting Program.");
            Console.ReadLine();
        }

        public static void PrintMenu()
        {
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. Delete Item");
            Console.WriteLine("3. Edit Item");
            Console.WriteLine("4. Complete Task");
            Console.WriteLine("5. List Outstanding Tasks (Not Appointments)");
            Console.WriteLine("6. List All Tasks and Appointments");
            Console.WriteLine("7. Save");
            Console.WriteLine("8. Search");
            Console.WriteLine("9. Exit");
        }

        public static bool FillProperties(ToDo todo)
        {
            //C - Create
            //ask for property values
            Console.WriteLine("Give me a name:");
            todo.Name = Console.ReadLine();
            Console.WriteLine("Describe the task:");
            todo.Description = Console.ReadLine();
            Console.WriteLine("When is its deadline? (format MUST BE 'MM/DD/YYYY' )");
            var dateString = Console.ReadLine();
            try
            {
                todo.Deadline = DateTime.ParseExact(dateString, "d", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                Console.WriteLine("Invalid Date Format! Try again.");
                return false;
            }
            Console.WriteLine("Is it completed? (true/false - lowercase) ");
            var completion = Console.ReadLine();
            if (completion == "true" || completion == "false")
            {
                bool isComplete;
                bool.TryParse(completion, out isComplete);
                todo.IsCompleted = isComplete;
            }
            else
            {
                Console.WriteLine("Invalid Completion Choice! Try again.");
                return false;
            }
            return true;

        }




        public static bool FillCalendarProperties(Appointment app)
        {
            Console.WriteLine("Give me a Name");
            app.Name = Console.ReadLine();
            Console.WriteLine("Give me a Description");
            app.Description = Console.ReadLine()?.Trim();
            Console.WriteLine("When is its start date? (format MUST BE 'MM/DD/YYYY' )");
            var startString = Console.ReadLine();
            try
            {
                app.Start = DateTime.ParseExact(startString, "d", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                Console.WriteLine("Invalid Date Format! Try again.");
                return false;
            }
            Console.WriteLine("When is its end date? (format MUST BE 'MM/DD/YYYY' )");
            var endString = Console.ReadLine();
            try
            {
                app.End = DateTime.ParseExact(endString, "d", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                Console.WriteLine("Invalid Date Format! Try again.");
                return false;
            }
            Console.WriteLine("Who is coming (enter names until done, then enter 'E'):");
            string? attendeeName = "";
            while (attendeeName != "E")
            {
                attendeeName = Console.ReadLine();
                if (attendeeName == "")
                {
                    Console.WriteLine("Attendee name cannot be blank.");
                    continue;
                }
                else if (attendeeName == "E")
                {
                    continue;
                }
                else
                {
                    app.Attendees.Add(attendeeName);
                }
            }

            return true;
        }
        public static void AddString(List<string> strList, string str)
        {
            strList.Add(str);
        }
    }
}