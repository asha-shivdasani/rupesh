using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstMVCCore
{
    public class AppConfiguration
    {
        public string Company { get; set; }

        public string Location { get; set; }

        public string Message { get; set; }

        public Address Address { get; set; }

        public string ProjectName { get; set; }

        public string ProjectManager { get; set; }

        public string Duration { get; set; }

        public Courses Courses { get; set; }

        public string AuthorName { get; set; }

    }

    public class Address
    {
        public string City { get; set; }

        public string Building { get; set; }
    }

    public class Courses
    {
        public string Title { get; set; }

        public string NoOfParticipants { get; set; }
    }
}
