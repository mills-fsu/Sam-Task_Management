using API.ListManagement.database;
using API.ListManagement.EC;
using Library.ListManagement.Standard.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace API.ListManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;

        public ItemController(ILogger<ToDoController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public IEnumerable<ItemDTO> Get()
        {
            List<ItemDTO> results = new List<ItemDTO>();
            results.AddRange(new ToDoEC().Get().ToList());
            results.AddRange(new AppointmentEC().Get());
            return results;
        }
        //[HttpPost("Search")]
        //public ObservableCollection<ItemDTO> Search([FromBody] string Query)
        //{
        //    Query = Query.Replace("\n", "");

        //    var ToDoResults = FakeDatabase.ToDos.Where(i => (i?.Name?.Replace("\n", "").ToUpper()?.Contains(Query.ToUpper()) ?? false)
        //    //i is any item and its name contains the query
        //    || (i?.Description?.Replace("\n", "").ToUpper()?.Contains(Query.ToUpper()) ?? false));
        //    //or i is any item and its description contains the query
            

        //    var AppResults = FakeDatabase.Appointments.Where(i => (i?.Name?.Replace("\n", "").ToUpper()?.Contains(Query.ToUpper()) ?? false)
        //    //i is any item and its name contains the query
        //    || (i?.Description?.Replace("\n", "").ToUpper()?.Contains(Query.ToUpper()) ?? false)
        //    //or i is any item and its description contains the query
        //    || ((new AppointmentDTO(i)?.Attendees?.Select(t => t.ToUpper().Replace("\n", ""))?.Contains(Query.ToUpper()) ?? false)));
        //    //or i is an appointment and has the query in the attendees list
        //    var results = ToDoResults.Concat(AppResults);
        //    results = results.AsEnumerable();
        //    var filteredResults = new ObservableCollection<ItemDTO>();
        //    foreach (var i in results)
        //    {

        //        filteredResults.Add(new ItemDTO(i));
        //    }
        //    return filteredResults;
        //}
    }
}
