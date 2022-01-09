namespace Visma
{
    public class DateStrategy : IFilterStrategy
    {

        private When _when;
        private DateTime _key2;
        public DateStrategy(When when, DateTime key2)
        {
            _when = when;
            _key2 = key2;
        }

        public List<Meeting> Filter(string? key, List<Meeting> meetings)
        {
            List<Meeting> result = new List<Meeting>();
            switch(_when)
            {
                case When.BEFORE:
                    foreach (Meeting meeting in meetings)
                        if (meeting.StartDate < Convert.ToDateTime(key) || meeting.EndDate < Convert.ToDateTime(key))
                            result.Add(meeting);
                    break;
                case When.AFTER:
                    foreach(Meeting meeting in meetings)
                        if(meeting.StartDate > Convert.ToDateTime(key) || meeting.EndDate > Convert.ToDateTime(key))
                            result.Add(meeting);
                    break;
                case When.BETWEEN:
                    
                    foreach(Meeting meeting in meetings)
                        if((meeting.StartDate > Convert.ToDateTime(key) && meeting.StartDate < _key2) 
                            || (meeting.EndDate > Convert.ToDateTime(key) && meeting.EndDate < _key2) 
                            || (meeting.StartDate < Convert.ToDateTime(key) && meeting.EndDate > _key2 ))
                            result.Add(meeting);
                    break;
            }

            return result;
        }
    }
    public enum When
    {
        BEFORE = 1,
        BETWEEN = 2,
        AFTER = 3
    }
}