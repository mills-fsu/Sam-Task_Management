using Library.ListManagement.Standard.DTO;
using ListManagement.models;
using ListManagement.services;
using ListManagement.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UWPListManagement.services;

namespace UWPListManagement.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ItemServiceProxy itemService = new ItemServiceProxy();
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private JsonSerializerSettings serializerSettings
            = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        //private ItemService itemService = ItemService.Current;

        public MainViewModel()
        {
            MVMFilteredItems = new ObservableCollection<ItemViewModel>();
            //IncompleteItems = new ObservableCollection<ItemViewModel>();
        }

        public MainViewModel(string path)
        {
            //Load(path);
            MVMFilteredItems = new ObservableCollection<ItemViewModel>();
            //IncompleteItems = new ObservableCollection<ItemViewModel>();
        }

        //public ObservableCollection<Item> Items_List
        //{
        //    get
        //    {
        //        return itemService.Items;
        //    }
        //}
        public ObservableCollection<ItemViewModel> Items
        {
            get
            {
                return itemService.Items;
            }
        }

        public string Query { get; set; }

        public ObservableCollection<ItemViewModel> MVMFilteredItems { get; set; }
        public void GetFilteredItems(string Query)
        {
            MVMFilteredItems = itemService.FilteredItems(Query); ;
            NotifyPropertyChanged("MVMFilteredItems");
            //return itemService.FilteredItems(Query);
        }
        //public ObservableCollection<Item>GetFilteredItems(string Query)
        //{
        //     Query = Query.Replace("\n", "");
        //     var results = Items.Where(i => (i?.Name?.Replace("\n", "").ToUpper()?.Contains(Query.ToUpper()) ?? false)
        //     //i is any item and its name contains the query
        //     || (i?.Description?.Replace("\n", "").ToUpper()?.Contains(Query.ToUpper()) ?? false)
        //     //or i is any item and its description contains the query
        //     || ((i as Appointment)?.Attendees?.Select(t => t.ToUpper().Replace("\n", ""))?.Contains(Query.ToUpper()) ?? false));
        //     //or i is an appointment and has the query in the attendees list
        //     var filteredResults = new ObservableCollection<Item>(results);
        //    if (MVMFilteredItems != null) { MVMFilteredItems.Clear(); }
        //    foreach(var i in filteredResults)
        //    {

        //        MVMFilteredItems.Add(i);          
        //     }
        //     return filteredResults;
        //}
        //public ObservableCollection<Item> IncompleteItems { get; set; }
        //public ObservableCollection<Item> GetIncompleteItems()
        //{
        //    var results = Items.Where(i =>
        //        !((i as ToDo)?.IsCompleted ?? true));
        //    var incompleteResults = new ObservableCollection<Item>(results);
        //    if (IncompleteItems != null) { IncompleteItems.Clear(); }
        //    foreach (var i in incompleteResults)
        //    {

        //        IncompleteItems.Add(i);
        //    }
        //    return incompleteResults;
        //}
        public ItemViewModel SelectedItem
        {
            get; set;
        }

        public async void Add(ItemViewModel item)
        {
            if (item.BoundItem is ToDoDTO)
            {
                await itemService.AddUpdate(item);
            }
            else
            {
                await itemService.AddUpdateApp(item);
            }
            
            Refresh();
        }
        public void Refresh()
        {
            NotifyPropertyChanged("Items");
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
        public void Save()
        {

            //var mvmJson = JsonConvert.SerializeObject(this, serializerSettings);
            //if (File.Exists(path))
            //{
            //    File.Delete(path);
            //}
            //File.WriteAllText(path, mvmJson);
            itemService.Save();
        }
        public void Delete(int id)
        {
            //if (item.BoundItem is ToDoDTO)
            //{
            //    await itemService?.Delete(id);
            //}
            //else
            //{
            //    await itemService?.Delete(id);
            //}

            //Refresh();
            //if (item.BoundItem is ToDoDTO)
            //{
            //    itemService?.Delete(id); Refresh();
            //}
            //else
            //{
            //    itemService?.DeleteApp(id); Refresh();
            //}

            //Refresh();
            itemService?.Delete(id); Refresh();
        }
        public void DeleteApp(int id)
        {
            itemService?.DeleteApp(id); Refresh();
        }
    }
}
