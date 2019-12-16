using System;
namespace XebiaSampleApp.Model
{
    public class Person
    {

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Byte[] profile { get; set; }

    }


    public class PersonDetail : Person
    {

        public string ContactNumber { get; set; }

        public string JobTitle { get; set; }

        public int YearsOfExp { get; set; }

        public double Salary { get; set; }

        public string Address  { get; set; }

     

    }

}
