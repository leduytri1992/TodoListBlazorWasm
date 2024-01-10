namespace TodoList.Models.SeedWork
{
    public class PageList<T>
    {
        public MetaData MetaData { get; set; }
        public List<T> Items { get; set; }

        public PageList() { }
        public PageList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            Items = items;
        }
    }
}
