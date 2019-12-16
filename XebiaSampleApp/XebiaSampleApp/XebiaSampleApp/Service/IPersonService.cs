using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XebiaSampleApp.Model;

namespace XebiaSampleApp.Service
{
    public interface IPersonService
    {

        Task<ObservableCollection<Person>> GetPersonsAsync();
        Task<Person> GetPersonDetailsAsync(int personId);
        Task<bool> DeletePersonAsync(int personId);

        Task<Person> SavePersonAsync(Person person);

   


    }
}
