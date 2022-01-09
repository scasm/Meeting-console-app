namespace Visma
{
    public class Filter
    {
        private List<Meeting> _meetings;
        public IFilterStrategy? Strategy { get; set; }

        public Filter(List<Meeting> meetings)
        {
            _meetings = new List<Meeting>();
            foreach (Meeting meeting in meetings)
                _meetings.Add(meeting);
        }
        public List<Meeting> FilterMeeting(IFilterStrategy strategy, string? key)
        {
            return strategy.Filter(key, _meetings);
        }

    }
}