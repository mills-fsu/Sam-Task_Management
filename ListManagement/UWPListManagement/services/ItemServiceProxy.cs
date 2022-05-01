using Library.ListManagement.Standard.DTO;
using ListManagement.services;
using ListManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPListManagement.services
{
    public class ItemServiceProxy
    {
        private ItemService itemService;

        public ItemServiceProxy ()
        {
            itemService = ItemService.Current;
        }


        public ObservableCollection<ItemViewModel> Items
        {
            get
            {
                return new ObservableCollection<ItemViewModel>
                    (itemService.Items.Select(i => new ItemViewModel(i)));
            }
        }

        public ObservableCollection<ItemViewModel> FilteredItems(string Query)
        {
            itemService.GetFilteredItems(Query);
            var answer = new ObservableCollection<ItemViewModel>();
                foreach (ItemDTO item in itemService.MVMFilteredItems)
                {
                    answer.Add(new ItemViewModel(item));
                }
            return answer;
            //return new ObservableCollection<ItemViewModel>(itemService.MVMFilteredItems);

        }

        public async Task<ToDoDTO> AddUpdate(ItemViewModel item)
        {
            return await itemService.AddUpdate(item.BoundItem);
        }

        public async Task<ItemDTO> Delete(int id)
        {
            return await itemService.Remove(id);
        }

        public async Task<AppointmentDTO> AddUpdateApp(ItemViewModel item)
        {
            var test = item.BoundItem;
            return await itemService.AddUpdateApp(item.BoundItem);
        }
        public async Task<ItemDTO> DeleteApp(int id)
        {
            return await itemService.DeleteApp(id);
        }

        public void Save()
        {
            itemService.Save();
        }
        public void Load(string path)
        {
            itemService.Load(path);
        }
    }
}
