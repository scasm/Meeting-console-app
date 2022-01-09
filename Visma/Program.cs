using Newtonsoft.Json;


namespace Visma
{
    public class Program
    {
        public static List<Meeting> meetings = new List<Meeting>();
        public static string? currentUsername;

        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Today.AddDays(-1) > DateTime.Today);
            Console.WriteLine(DateTime.Today.AddDays(-2));
            Console.WriteLine(DateTime.Today.AddDays(-4));
            Console.Write("Hello, write your name:");
            currentUsername = Console.ReadLine();
            Console.WriteLine($"Hello, {currentUsername}");



            var file = File.ReadAllText(@"..\..\..\json.json");
            var data = JsonConvert.DeserializeObject<List<Meeting>>(file);
            if(data != null)
                meetings = data.ToList<Meeting>();

            int a = -1;
            while (a != 0)
            {
                var validInput = false;
                while (!validInput)
                {
                    Console.WriteLine("Enter:\n" +
                                "1. To create a meeting\n" +
                                "2. To Delete a meeting\n" +
                                "3. To add a person to a meeting\n" +
                                "4. To remove a person from a meeting\n" +
                                "5. To list all meetings\n" +
                                "0. To exit.");
                    validInput = Int32.TryParse(Console.ReadLine(), out a);
                }


                switch (a)
                {
                    case 1:
                        CreateMeeting();
                        break;
                    case 2:
                        DeleteMeeting();
                        break;
                    case 3:
                        AddPersonToMeeting();
                        break;
                    case 4:
                        RemovePersonToMeeting();
                        break;
                    case 5:
                        ListAllMeetings();
                        break;
                }
            }
        }
        private static void CreateMeeting()
        {
            Console.WriteLine("Enter Meeting's name");
            string? name = Console.ReadLine();

            while(string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Name cannot be empty");
                name = Console.ReadLine();
            }

            Console.WriteLine("Enter Responsible person's name");
            var respPerson = Console.ReadLine();

            while (string.IsNullOrEmpty(respPerson))
            {
                Console.WriteLine("Responsible person's name cannot be empty");
                respPerson = Console.ReadLine();
            }

            Console.WriteLine("Enter meeting's description");
            var desc = Console.ReadLine();

            Category category = Category.CodeMonkey;
            var validInput = false;
            while (!validInput)
            {
                Console.WriteLine($"Choose the category: {Category.CodeMonkey} / {Category.Hub} / {Category.Short} / {Category.TeamBuilding}");
                validInput = Enum.TryParse<Category>(Console.ReadLine(), out category);
            }

            Type type = Type.InPerson;
            validInput = false;
            while (!validInput)
            {
                Console.WriteLine($"Choose the type: {Type.Live} / {Type.InPerson}");
                validInput = Enum.TryParse<Type>(Console.ReadLine(), out type);
            }

            DateTime startDate = DateTime.MinValue;
            validInput = false;
            while (!validInput)
            {
                Console.WriteLine("Enter the start date YYYY-MM-DD");
                validInput = DateTime.TryParse(Console.ReadLine(), out startDate);
            }

            DateTime endDate = DateTime.MaxValue;
            validInput = false;
            while (!validInput)
            {
                Console.WriteLine("Enter the end date YYYY-MM-DD");
                validInput = DateTime.TryParse(Console.ReadLine(), out endDate);
            }

            var attendees = new List<string>();
            attendees.Add(respPerson);
            meetings.Add(new Meeting(name, respPerson, desc, category, type, startDate, endDate, attendees));

            SaveToFile();


        }
        private static void DeleteMeeting()
        {
            Console.WriteLine("Enter Meeting's name, which you want to delete");
            var name = Console.ReadLine();

            var meetingToDelete = meetings.SingleOrDefault(x => x.Name == name);
            if (meetingToDelete != null)
            {
                if (meetingToDelete.ResponsiblePerson == currentUsername)
                {
                    meetings.Remove(meetingToDelete);

                    Console.WriteLine("Meeting deleted succesfully");
                    SaveToFile();
                }
                else
                    Console.WriteLine("You're not the responsible person");
            }
            else
                Console.WriteLine("There is no such meeting");

        }
        private static void AddPersonToMeeting()
        {
            Console.WriteLine("Enter Meeting's name, to which you want to add a person");
            var name = Console.ReadLine();

            var meetingToAdd = meetings.SingleOrDefault(x => x.Name == name);
            if (meetingToAdd != null)
            {
                Console.WriteLine("Enter Person's name, which will be added");
                var pName = Console.ReadLine();

                if (meetingToAdd.Attendees.Contains(pName))
                    Console.WriteLine("This person is already added to a meeting");
                else
                {
                    meetingToAdd.Attendees.Add(pName);
                    Console.WriteLine("Person added succesfully");
                }
            }
            else
                Console.WriteLine("There is no such meeting");
            SaveToFile();
        }
        private static void RemovePersonToMeeting()
        {
            Console.WriteLine("Enter Meeting's name, from which you want to remove a person");
            var name = Console.ReadLine();



            var meetingToRemove = meetings.SingleOrDefault(x => x.Name == name);
            if (meetingToRemove != null)
            {
                Console.WriteLine("Enter Person's name, which will be removed");
                string? pName = Console.ReadLine();

                if (meetingToRemove.ResponsiblePerson == pName)
                    Console.WriteLine("This person cannot be removed");
                else if (!meetingToRemove.Attendees.Contains(pName))
                    Console.WriteLine("There is no such person in the meeting");
                else
                {
                    meetingToRemove.Attendees.Remove(pName);
                    Console.WriteLine("Person removed succesfully");
                }
            }
            else
                Console.WriteLine("There is no such meeting");
            SaveToFile();
        }

        private static void ListAllMeetings()
        {
            int filterChoice = 0;
            var validInput = false;
            while (!validInput)
            {
                Console.WriteLine("Select parameter to filter the meetings\n" +
                                   "Filter by:\n" +
                                   "1. Description\n" +
                                   "2. Responsible person\n" +
                                   "3. Category\n" +
                                   "4. Type\n" +
                                   "5. Date\n" +
                                   "6. Number of attendees\n" +
                                   "0. Don't filter");
                validInput = Int32.TryParse(Console.ReadLine(), out filterChoice);
            }

            
                var filter = new Filter(meetings);
                var filtered = new List<Meeting>();
                switch (filterChoice)
                {
                    case 1:

                        Console.WriteLine("Enter a key to filter");
                        var key = Console.ReadLine();
                        filtered = filter.FilterMeeting(new DescriptionStrategy(), key);
                        PrintMeetings(filtered);
                        break;
                    case 2:

                        Console.WriteLine("Enter a key to filter");
                        key = Console.ReadLine();
                        filtered = filter.FilterMeeting(new ResponsiblePersonStrategy(), key);
                        PrintMeetings(filtered);
                        break;
                    case 3:
                        Category category = Category.CodeMonkey;
                        validInput = false;
                        while (!validInput)
                        {
                            Console.WriteLine($"Choose the category: {Category.CodeMonkey} / {Category.Hub} / {Category.Short} / {Category.TeamBuilding}");
                            validInput = Enum.TryParse<Category>(Console.ReadLine(), out category);
                        }
                        filtered = filter.FilterMeeting(new CategoryStrategy(), category.ToString());
                        PrintMeetings(filtered);
                        break;
                    case 4:
                        Type type = Type.InPerson;
                        validInput = false;
                        while (!validInput)
                        {
                            Console.WriteLine($"Choose the type: {Type.InPerson} / {Type.Live}");
                            validInput = Enum.TryParse<Type>(Console.ReadLine(), out type);
                        }
                        filtered = filter.FilterMeeting(new TypeStrategy(), type.ToString());
                        PrintMeetings(filtered);
                        break;
                    case 5:
                        validInput = false;
                        When when = When.BEFORE;
                        while (!validInput)
                        {
                            Console.WriteLine($"Filter the date: {When.BEFORE} / {When.BETWEEN} / {When.AFTER}");
                            validInput = Enum.TryParse<When>(Console.ReadLine()?.ToUpper(), out when);
                        }

                        DateTime startDate = DateTime.MinValue;
                        validInput = false;
                        while (!validInput)
                        {
                            Console.WriteLine($"Enter the date filter (YYYY-MM-DD) {when.ToString()}:");
                            validInput = DateTime.TryParse(Console.ReadLine(), out startDate);
                        }

                        DateTime key2 = DateTime.MaxValue;
                        if (when.Equals(When.BETWEEN)) {
                            validInput = false;
                            while (!validInput)
                            {
                                Console.WriteLine($"Enter the date to filter (YYYY-MM-DD) BETWEEN {startDate.ToString("yyyy-MM-dd")} AND :");
                                validInput = DateTime.TryParse(Console.ReadLine(), out key2);
                            }
                        }
                        

                        filtered = filter.FilterMeeting(new DateStrategy(when, key2), startDate.ToString("yyyy-MM-dd"));
                        PrintMeetings(filtered);
                        break;
                    case 6:
                        validInput = false;
                        Quantity quantity = Quantity.LESS;
                        while (!validInput)
                        {
                            Console.WriteLine($"How much attendees: {Quantity.LESS} / {Quantity.BETWEEN} / {Quantity.MORE}");
                            validInput = Enum.TryParse(Console.ReadLine()?.ToUpper(), out quantity);
                        }
                        int count = 0;
                        validInput = false;
                        while(!validInput)
                        {
                            Console.WriteLine($"Enter the quantity {quantity.ToString()}");
                            validInput = Int32.TryParse(Console.ReadLine(), out count);
                        }
                        int key21 = 0;
                        if (quantity == Quantity.BETWEEN)
                        {
                            validInput = false;
                            while (!validInput)
                            {
                                Console.WriteLine($"Enter the number of attendees to filter BETWEEN {count} AND :");
                                validInput = Int32.TryParse(Console.ReadLine(), out key21);
                            }
                        }
                        
                        filtered = filter.FilterMeeting(new AttendeesStrategy(quantity, key21.ToString()), count.ToString());
                        PrintMeetings(filtered);
                        break;
                    default:
                        PrintMeetings(meetings);
                        break;

                }
            
        }




 

        public static void SaveToFile()
        {
            var serialized = JsonConvert.SerializeObject(meetings.ToArray());

            File.WriteAllText(@"..\..\..\json.json", serialized);
        }

        public static void PrintMeetings(List<Meeting> meetings)
        {
            if (!meetings.Any())
                Console.WriteLine("There are no meetings");
            else
            {
                Console.WriteLine("Name            | Description               | Responsible Person        | Category     | Type     | StartDate  | EndDate    | Number of attendees");
                foreach (Meeting meeting in meetings)
                    Console.WriteLine("{0,-15} | {1,-25} | {2, -25} | {3, -12} | {4, -8} | {5,-10} | {6,-10} | {7,-3}",meeting.Name, meeting.Description, meeting.ResponsiblePerson, meeting.Category, meeting.Type, meeting.StartDate.ToString("yyyy-MM-dd"), meeting.EndDate.ToString("yyyy-MM-dd"), meeting.Attendees.Count);
            }

        }
    }
    
}