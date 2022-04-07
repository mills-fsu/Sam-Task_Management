using ListManagement.models;
using ListManagement.services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPListManagement.ViewModels
{
    public class MainViewModel
    {
        private JsonSerializerSettings serializerSettings
            = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        private ItemService itemService = ItemService.Current;

        public MainViewModel()
        {
            MVMFilteredItems = new ObservableCollection<Item>();
            IncompleteItems = new ObservableCollection<Item>();
        }

        public MainViewModel(string path)
        {
            //Load(path);
            MVMFilteredItems = new ObservableCollection<Item>();
            IncompleteItems = new ObservableCollection<Item>();
        }

        public ObservableCollection<Item> Items
        {
            get
            {
                return itemService.Items;
            }
        }

        public string Query { get; set; }

        public ObservableCollection<Item> MVMFilteredItems { get; set; }
        
        public ObservableCollection<Item>GetFilteredItems(string Query)
        {
             Query = Query.Replace("\n", "");
             var results = Items.Where(i => (i?.Name?.Replace("\n", "").ToUpper()?.Contains(Query.ToUpper()) ?? false)
             //i is any item and its name contains the query
             || (i?.Description?.Replace("\n", "").ToUpper()?.Contains(Query.ToUpper()) ?? false)
             //or i is any item and its description contains the query
             || ((i as Appointment)?.Attendees?.Select(t => t.ToUpper().Replace("\n", ""))?.Contains(Query.ToUpper()) ?? false));
             //or i is an appointment and has the query in the attendees list
             var filteredResults = new ObservableCollection<Item>(results);
            if (MVMFilteredItems != null) { MVMFilteredItems.Clear(); }
            foreach(var i in filteredResults)
            {

                MVMFilteredItems.Add(i);          
             }
             return filteredResults;
        }
        public ObservableCollection<Item> IncompleteItems { get; set; }
        public ObservableCollection<Item> GetIncompleteItems()
        {
            var results = Items.Where(i =>
                !((i as ToDo)?.IsCompleted ?? true));
            var incompleteResults = new ObservableCollection<Item>(results);
            if (IncompleteItems != null) { IncompleteItems.Clear(); }
            foreach (var i in incompleteResults)
            {

                IncompleteItems.Add(i);
            }
            return incompleteResults;
        }
        public Item SelectedItem
        {
            get; set;
        }

        public void Add(Item item)
        {
            itemService.Add(item);
        }

        public void Load(string path)
        {
            MainViewModel mvm;
            if (File.Exists(path))
            {
                try
                {
                    mvm = JsonConvert
                    .DeserializeObject<MainViewModel>(File.ReadAllText(path), serializerSettings);

                    SelectedItem = mvm.SelectedItem;

                }
                catch (Exception)
                {
                    File.Delete(path);
                }

            }
        }
        public void Save(string path)
        {

            var mvmJson = JsonConvert.SerializeObject(this, serializerSettings);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.WriteAllText(path, mvmJson);
        }
    }
}
