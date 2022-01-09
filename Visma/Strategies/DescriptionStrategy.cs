namespace Visma
{
    public class DescriptionStrategy : IFilterStrategy
    {
      
        public List<Meeting> Filter(string? key, List<Meeting> meetings)
        {
            List<Meeting> result = new List<Meeting>();

            foreach (Meeting meeting in meetings)
                if(meeting.Description.ToLower().Contains(key?.ToLower()))
                    result.Add(meeting);

            return result;
        }
    }
}