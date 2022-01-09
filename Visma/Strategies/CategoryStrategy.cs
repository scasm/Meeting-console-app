namespace Visma
{   
    public class CategoryStrategy : IFilterStrategy
    {
        public List<Meeting> Filter(string? key, List<Meeting> meetings)
        {
            Enum.TryParse(key, out Category cKey);
            List<Meeting> result = new List<Meeting>();
            foreach (Meeting meeting in meetings)
                if(meeting.Category == cKey)
                    result.Add(meeting);
            return result;
        }
    }
}