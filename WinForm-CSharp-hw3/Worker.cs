using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm_CSharp_hw3
{
    public class Worker
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FatherName { get; set; }
        public DateTime Birthday { get; set; }
        public string PlaceOfResidence { get; set; }
        private DateTime DateofCreate { get; set; }

        public Worker()
        {
            DateofCreate = DateTime.Now;
        }
        public Worker(string name, string surname, string fatherName, DateTime birthday, string placeOfResidence)
        {
            Name = name;
            Surname = surname;
            FatherName = fatherName;
            Birthday = birthday;
            PlaceOfResidence = placeOfResidence;
            DateofCreate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Name} {Surname} {FatherName}";
        }

    }
}
