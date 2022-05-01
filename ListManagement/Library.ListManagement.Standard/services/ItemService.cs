using Library.ListManagement.helpers;
using Library.ListManagement.Standard.DTO;
using Library.ListManagement.Standard.utilities;
using ListManagement.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListManagement.services
{
    public class ItemService
    {
        private ObservableCollection<ItemDTO> items;
        private ListNavigator<ItemDTO> listNav;
        private string persistencePath;
        private JsonSerializerSettings serializerSettings
            = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        static private ItemService instance;

        public bool ShowComplete { get; set; }
        public ObservableCollection<ItemDTO> Items {
            get
            {
                var payload = JsonConvert
                    .DeserializeObject<List<ItemDTO>>(new WebRequestHandler()
                    .Get("http://localhost:5436/Item").Result);
                items.Clear();
                payload.ForEach(items.Add);

                return items;
            }
        }

        public string Query { get; set; }

        public ObservableCollection<ItemDTO> MVMFilteredItems { get; set; }
        public ObservableCollection<ItemDTO> IncompleteItems { get; set; }
        public ObservableCollection<ItemDTO> GetFilteredItems(string Query)
        {
            Query = Query.Replace("\n", "");
            var results = items.Where(i => (i?.Name?.Replace("\n", "").ToUpper()?.Contains(Query.ToUpper()) ?? false)
            //i is any item and its name contains the query
            || (i?.Description?.Replace("\n", "").ToUpper()?.Contains(Query.ToUpper()) ?? false)
            //or i is any item and its description contains the query
            || ((i as AppointmentDTO)?.Attendees?.Select(t => t.ToUpper().Replace("\n", ""))?.Contains(Query.ToUpper()) ?? false));
            //or i is an appointment and has the query in the attendees list
            var filteredResults = new ObservableCollection<ItemDTO>(results);
            if (MVMFilteredItems != null) { MVMFilteredItems.Clear(); }
            foreach (var i in filteredResults)
            {

                MVMFilteredItems.Add(i);
            }
            return filteredResults;
        }
        //public IEnumerable<ItemDTO> FilteredItems
        //{
        //    get
        //    {
        //        var incompleteItems = Items.Where(i =>
        //        (!ShowComplete && !((i as ToDoDTO)?.IsCompleted ?? true)) //incomplete only
        //        || ShowComplete);
        //        //show complete (all)

        //        var searchResults = incompleteItems.Where(i => string.IsNullOrWhiteSpace(Query)
        //        //there is no query
        //        || (i?.Name?.ToUpper()?.Contains(Query.ToUpper()) ?? false)   
        //        //i is any item and its name contains the query
        //        || (i?.Description?.ToUpper()?.Contains(Query.ToUpper()) ?? false)                                        
        //        //or i is any item and its description contains the query
        //        ||((i as AppointmentDTO)?.Attendees?.Select(t => t.ToUpper())?.Contains(Query.ToUpper()) ?? false));         
        //        //or i is an appointment and has the query in the attendees list
        //        return searchResults;
        //    }
        //}

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
            items = new ObservableCollection<ItemDTO>();
            persistencePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SaveData.json";
            try
            {
                LoadFromServer();
            }
            catch (Exception)
            {
                LoadFromDisk();
            }
            MVMFilteredItems = new ObservableCollection<ItemDTO>();
            IncompleteItems = new ObservableCollection<ItemDTO>();
        }
        public void Load(string path)
        {
            persistencePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SaveData.json";
            try
            {
                LoadFromServer();
            }
            catch (Exception)
            {
                LoadFromDisk();
            }
        }
        private void LoadFromServer()
        {
            var payload = JsonConvert
            .DeserializeObject<List<ItemDTO>>(new WebRequestHandler()
            .Get("http://localhost:5436/Item").Result);

            payload.ToList().ForEach(items.Add);

            //listNav = new ListNavigator<ItemDTO>(FilteredItems, 2);
        }

        private void LoadFromDisk()
        {

            if (File.Exists(persistencePath))
            {
                try
                {
                    var state = File.ReadAllText(persistencePath);
                    if (state != null)
                    {
                        items = JsonConvert
                        .DeserializeObject<ObservableCollection<ItemDTO>>(state, serializerSettings) ?? new ObservableCollection<ItemDTO>();
                    }
                }
                catch (Exception e)
                {
                    File.Delete(persistencePath);
                    items = new ObservableCollection<ItemDTO>();
                }
            }
        }
        public async Task<ToDoDTO> AddUpdate(ItemDTO i)
        {
            var toDoStr = await new WebRequestHandler().Post("http://localhost:5436/ToDo/AddOrUpdate", i);
            var todo = JsonConvert.DeserializeObject<ToDoDTO>(toDoStr);
            //var item = DataContext as ToDo;
            //if(items.Any(j => j.Id == i.Id))
            //{
            //    var itemToUpdate = items.FirstOrDefault(j => j.Id == i.Id);
            //    var index = items.IndexOf(itemToUpdate);
            //    items.RemoveAt(index);
            //    items.Insert(index, i);
            //}
            //else
            //{
            //    ItemService.Current.Add(DataContext as ToDo);
            //}
            return todo;
        }

        public async Task<ItemDTO> Remove(int id)
        {
            var deletedToDoStr = await new WebRequestHandler().Post("http://localhost:5436/ToDo/Delete", new DeleteItemDTO { IdToDelete = id });
            return JsonConvert.DeserializeObject<ItemDTO>(deletedToDoStr);
        }
        //public async Task<ObservableCollection<ItemDTO>> Search(string Query)
        //{
        //    string searchResult = await new WebRequestHandler().Post("http://localhost:5436/Item/Search", Query);
        //    return JsonConvert.DeserializeObject<ObservableCollection<ItemDTO>>(searchResult);
        //}


        public async Task<AppointmentDTO> AddUpdateApp(ItemDTO i)
        {
            var appStr = await new WebRequestHandler().Post("http://localhost:5436/Appointment/AddOrUpdate", i);
            var app = JsonConvert.DeserializeObject<AppointmentDTO>(appStr);
            return app;
        }
        public async Task<ItemDTO> DeleteApp(int id)
        {
            var deletedToDoStr = await new WebRequestHandler().Post("http://localhost:5436/Appointment/Delete", new DeleteItemDTO { IdToDelete = id });
            return JsonConvert.DeserializeObject<ItemDTO>(deletedToDoStr);
        }

        public void Save()
        {
            //first save to disk (pass-through cache)
            var listJson = JsonConvert.SerializeObject(Items, serializerSettings);
            if (File.Exists(persistencePath))
            {
                File.Delete(persistencePath);
            }
            File.WriteAllText(persistencePath, listJson);

            //post request to add each of these items to the list-- COMMENTING OUT FOR PROGRAMMING ASSIGNMENT 3
            foreach (var i in Items)
            {
                if (i is ToDo)
                {
                    JsonConvert.DeserializeObject<List<Item>>(
                    new WebRequestHandler().Post("http://localhost:5436/ToDo/AddOrUpdate", i).Result);
                }
            }
        }


        //public void Add(Item i)
        //{
        //    if (i.Id <= 0)
        //    {
        //        i.Id = NextId;
        //    }
        //    items.Add(i);
        //}

        //public void Remove(Item i)
        //{
        //    items.Remove(i);
        //}

        //public void Save()
        //{

        //    var listJson = JsonConvert.SerializeObject(Items, serializerSettings);
        //    if (File.Exists(persistencePath))
        //    {
        //        File.Delete(persistencePath);
        //    }
        //    File.WriteAllText(persistencePath, listJson);
        //}

        public Dictionary<object, ItemDTO> GetPage()
        {
            var page = listNav.GetCurrentPage();
            if (listNav.HasNextPage)
            {
                //page.Add("N", new Item { Name = "Next" });
            } if (listNav.HasPreviousPage)
            {
                //page.Add("P", new Item { Name = "Previous" });
            }
            return page;
        }

        public Dictionary<object, ItemDTO> NextPage()
        {
            return listNav.GoForward();
        }

        public Dictionary<object, ItemDTO> PreviousPage()
        {
            return listNav.GoBackward();
        }

        public int NextId {
            get
            {
                if(Items.Any())
                {
                    return Items.Select(i => i.Id).Max() + 1;
                }
                return 1;
            }
        }
    }
}
