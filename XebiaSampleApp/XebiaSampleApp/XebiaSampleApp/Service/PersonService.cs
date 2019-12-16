using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XebiaSampleApp.Model;
using System.Linq;

namespace XebiaSampleApp.Service
{

    // This the place use the get the data from could

    public class PersonService : IPersonService
    {

        public Task<Person> GetPersonDetailsAsync(int personId)
        {
            var person = PersonsDB.FirstOrDefault(i => i.Id == personId);
            return Task.FromResult(person);
        }

        public Task<ObservableCollection<Person>> GetPersonsAsync()
        {
            return Task.FromResult(PersonsDB);
        }

        public Task<bool> DeletePersonAsync(int personId)
        {
            bool isDeleted = false;
            var personToBeDeleted = PersonsDB.FirstOrDefault(i => i.Id == personId);
            if (personToBeDeleted != null)
            {
                PersonsDB.Remove(personToBeDeleted);
                isDeleted = true;
            }
            return Task.FromResult(isDeleted);
        }

        public Task<Person> SavePersonAsync(Person person)
        {
            PersonDetail personDetail = person as PersonDetail;
            if (personDetail != null)
            {
                if (person.Id == 0)
                {
                    person.Id = PersonsDetailDB.Count + 1;
                    PersonsDetailDB.Add(person);
                }
                else
                {
                    var personExists = PersonsDetailDB.FirstOrDefault(i => i.Id == person.Id) as PersonDetail;
                    if (personExists != null)
                    {
                        personExists.FirstName = person.FirstName;
                        personExists.LastName = person.LastName;
                        personExists.profile = person.profile;
                        personExists.JobTitle = personDetail.JobTitle;
                        personExists.YearsOfExp = personDetail.YearsOfExp;
                        personExists.Salary = personDetail.Salary;
                        personExists.ContactNumber = personDetail.ContactNumber;
                        personExists.Address = personDetail.Address;
                    }
                }
            }

            return Task.FromResult(person);
        }

        

        private  ObservableCollection<Person> PersonsDB
        {
            get
            {
                return new ObservableCollection<Person>(PersonsDetailDB);
            }
        }


        ObservableCollection<Person> _personsDetailDB;
        private ObservableCollection<Person> PersonsDetailDB
        {
            get
            {
                if (_personsDetailDB == null)
                {
                    _personsDetailDB = new ObservableCollection<Person>();

                   // _personsDetailDB.Add(new PersonDetail() { Id = 1, FirstName = "Rohit", LastName = "Sharma", Address="Mumabai",ContactNumber="9199922276",YearsOfExp=5, JobTitle="SSE" });

                   // _personsDetailDB.Add(new PersonDetail() { Id = 2, FirstName = "Rahul", LastName = "Logesh", Address ="BLR" , ContactNumber = "9199922276", YearsOfExp = 5, JobTitle = "SSE" });

                  //  _personsDetailDB.Add(new PersonDetail() { Id = 3, FirstName = "Virat", LastName = "Kholi", Address="Delhi", ContactNumber = "9199922276", YearsOfExp = 5, JobTitle = "SSE" });

                  //  _personsDetailDB.Add(new PersonDetail() { Id = 4, FirstName = "Rishab", LastName = "Pant", Address = "Delhi", ContactNumber = "9199922276", YearsOfExp = 5, JobTitle = "SSE" });

                   // _personsDetailDB.Add(new PersonDetail() { Id = 5, FirstName = "Seriyas", LastName = "Iyer", Address = "Mumabai" , ContactNumber = "9199922276", YearsOfExp = 5, JobTitle = "SSE" });
                }

                return _personsDetailDB;
            }
        }



    }
}
