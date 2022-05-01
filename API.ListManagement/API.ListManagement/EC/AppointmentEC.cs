using Api.ToDoApplication.Persistence;
using API.ListManagement.database;
using Library.ListManagement.Standard.DTO;
using ListManagement.models;
using ListManagement.services;
namespace API.ListManagement.EC
{
    public class AppointmentEC
    {
        public IEnumerable<AppointmentDTO> Get()
        {
            return Filebase.Current.Appointments.Select(t => new AppointmentDTO(t));
        }
        public AppointmentDTO AddOrUpdate(AppointmentDTO app)
        {
            if (app.Id <= 0)
            {
                //CREATE
                app.Id = ItemService.Current.NextId;
                Filebase.Current.AddOrUpdate(new Appointment(app));
            }
            else
            {
                //UPDATE
                var itemToUpdate = Filebase.Current.Appointments.FirstOrDefault(i => i.Id == app.Id);
                if (itemToUpdate != null)
                {
                    var index = FakeDatabase.Appointments.IndexOf(itemToUpdate);
                    Filebase.Current.Appointments.Remove(itemToUpdate);
                    Filebase.Current.Appointments.Insert(index, new Appointment(app));
                }
                else
                {
                    //CREATE -- Fall-Back
                    Filebase.Current.Appointments.Add(new Appointment(app));
                }
            }

            return app;
        }

        public AppointmentDTO Delete(int Id)
        {
            var appToDelete = Filebase.Current.Appointments.FirstOrDefault(i => i.Id == Id);
            if (appToDelete != null)
            {
                Filebase.Current.DeleteApp(Id);
            }
            else
            {
                return new AppointmentDTO();
            }
            return new AppointmentDTO(appToDelete);
        }
    }
}
