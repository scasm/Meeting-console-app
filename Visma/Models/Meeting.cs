using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visma
{
    public enum Category
    {
        CodeMonkey = 1,
        Hub = 2,
        Short = 3,
        TeamBuilding = 4
    }
    public enum Type
    {
        Live = 1,
        InPerson = 2
    }
    public class Meeting
    {
        public string Name { get; set; }
        public string ResponsiblePerson { get; set; }
        public string Description { get; set; }

        public Category Category { get; set; }
        public Type Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string> Attendees { get; set; }

        public Meeting (string name, string respPerson, string description, Category category, Type type, DateTime startDate, DateTime endDate, List<string> attendees)
        {
            Name = name;
            ResponsiblePerson = respPerson;
            Description = description;
            Category = category;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            Attendees = attendees;

        }

    }
}
