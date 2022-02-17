using Library.ListManagement.helpers;
using ListManagement.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListManagement.services
{
    public class ItemService
    {
        private List<Item> items;
        private ListNavigator<Item> listNav;
        private string persistencePath;
        public string Query { get; set; }
        //private string Query;
        private JsonSerializerSettings serializerSettings
            = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        static private ItemService? instance;

        public bool ShowComplete { get; set; }
        public List<Item> Items {
            get {
                return items;
            }
        }

        public IEnumerable<Item> FilteredItems
        {
            get
            {
                //return Items.Where(i =>
                //(!ShowComplete && !((i as ToDo)?.IsCompleted ?? true)) //incomplete only
                //|| ShowComplete);

                var incompleteItems = Items.Where(i =>
                (!ShowComplete && !((i as ToDo)?.IsCompleted ?? true))
                || ShowComplete);
                
                Console.WriteLine(incompleteItems); Console.WriteLine("cat");
                var searchResults = incompleteItems.Where(i => string.IsNullOrWhiteSpace(Query)
                || (i?.Name?.Replace("\n", "").ToUpper()?.Contains(Query.ToUpper()) ?? false)
                || (i?.Description?.ToUpper()?.Contains(Query.ToUpper()) ?? false)
                || ((i as Appointment)?.Attendees?.Select(t => t.ToUpper())?.Contains(Query.ToUpper()) ?? false));

                return searchResults;
            }
        }

        public static ItemService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ItemService();
                }
                return instance;
            }
        }

        private ItemService()
        {
            items = new List<Item>();

            persistencePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SaveData.json";
            if (File.Exists(persistencePath))
            {
                try
                {
                    var state = File.ReadAllText(persistencePath);
                    if (state != null)
                    {
                        items = JsonConvert.DeserializeObject<List<Item>>(state, serializerSettings) ?? new List<Item>();
                    }
                } catch (Exception e)
                {
                    File.Delete(persistencePath);
                    items = new List<Item>();
                }
            }

            listNav = new ListNavigator<Item>(FilteredItems, 5);
        }

        public void Add(Item i)
        {
            if (i.Id <= 0)
            {
                i.Id = nextId;
            }
            items.Add(i);
        }

        public void Remove(Item i)
        {
            items.Remove(i);
        }

        public void Save()
        {

            var listJson = JsonConvert.SerializeObject(Items, serializerSettings);
            if (File.Exists(persistencePath))
            {
                File.Delete(persistencePath);
            }
            File.WriteAllText(persistencePath, listJson);
        }

        public void Search(string Q)
        {
            Query = Q;
        }

        public Dictionary<object, Item> GetPage()
        {
            var page = listNav.GetCurrentPage();
            if (listNav.HasNextPage)
            {
                page.Add("N", new Item { Name = "Next" });
            } if (listNav.HasPreviousPage)
            {
                page.Add("P", new Item { Name = "Previous" });
            }
            return page;
        }

        public Dictionary<object, Item> NextPage()
        {
            try
            {
                return listNav.GoForward();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return listNav.GetCurrentPage();
            }
        }

        public Dictionary<object, Item> PreviousPage()
        {
            try
            {
                return listNav.GoBackward();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return listNav.GetCurrentPage();
            }
        }

        private int nextId {
            get
            {
                if (items.Count > 0)
                {
                    return Items.Select(i => i.Id).Max() + 1;
                }
                else
                {
                    return 1;
                }
            }
        }
    }
}
