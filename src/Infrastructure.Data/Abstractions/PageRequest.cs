namespace Infrastructure.Data.Abstractions
{
    public class PageRequest
        : Pageable
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public Pageable First()
        {
            throw new System.NotImplementedException();
        }

        public int GetOffset()
        {
            throw new System.NotImplementedException();
        }

        public int GetPageNo()
        {
            throw new System.NotImplementedException();
        }

        public int GetPageSize()
        {
            throw new System.NotImplementedException();
        }

        public Sort GetSort()
        {
            throw new System.NotImplementedException();
        }

        public bool HasPrevious()
        {
            throw new System.NotImplementedException();
        }

        public Pageable Next()
        {
            throw new System.NotImplementedException();
        }

        public Pageable PreviousOrFirst()
        {
            throw new System.NotImplementedException();
        }
    }
}
