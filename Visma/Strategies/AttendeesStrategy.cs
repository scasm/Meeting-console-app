namespace Visma
{
    public class AttendeesStrategy : IFilterStrategy
    {

        private Quantity _quantity;
        private string? _key2;
        public AttendeesStrategy(Quantity quantity, string? key)
        {
            _quantity = quantity;
            _key2 = key;
        }
        public List<Meeting> Filter(string? key, List<Meeting> meetings)
        {
            List<Meeting> result = new List<Meeting>();
            switch (_quantity)
            {
                case Quantity.LESS:
                    foreach (Meeting meeting in meetings)
                        if (meeting.Attendees.Count < Convert.ToInt32(key))
                        {
                            result.Add(meeting);
                        }
                    break;
                case Quantity.MORE:
                    foreach (Meeting meeting in meetings)
                        if (meeting.Attendees.Count > Convert.ToInt32(key))
                            result.Add(meeting);
                    break;
                case Quantity.BETWEEN:

                    foreach (Meeting meeting in meetings)
                        if (meeting.Attendees.Count > Convert.ToInt32(key) && meeting.Attendees.Count < Convert.ToInt32(_key2))
                            result.Add(meeting);
                    break;
            }

            return result;
        }
    }
    public enum Quantity
    {
        LESS = 1,
        BETWEEN = 2,
        MORE = 3
    }
}