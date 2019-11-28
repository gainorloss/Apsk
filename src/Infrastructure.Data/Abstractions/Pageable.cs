namespace Infrastructure.Data.Abstractions
{
    public interface Pageable
    {
        int GetPageNo();
        int GetPageSize();
        int GetOffset();
        Sort GetSort();
        Pageable Next();
        Pageable PreviousOrFirst();
        Pageable First();
        bool HasPrevious();
    }
}