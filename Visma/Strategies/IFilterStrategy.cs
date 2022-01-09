namespace Visma
{
    public interface IFilterStrategy
    {
        List<Meeting> Filter(string? key, List<Meeting> meetings);
    }
}