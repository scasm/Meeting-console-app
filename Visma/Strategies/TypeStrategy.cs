namespace Visma
{
    public class TypeStrategy : IFilterStrategy
    {
        public List<Meeting> Filter(string? key, List<Meeting> meetings)
        {
            Enum.TryParse(key, out Type cKey);
            List<Meeting> result = new List<Meeting>();
            foreach (Meeting meeting in meetings)
                if (meeting.Type == cKey)
                    result.Add(meeting);
            return result;
        }
    }
}