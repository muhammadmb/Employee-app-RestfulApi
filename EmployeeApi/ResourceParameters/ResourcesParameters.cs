namespace EmployeeApi.ResourceParameters
{
    public class ResourcesParameters
    {
        const int maxPageSize = 15;

        public int PageNumber { get; set; } = 1;

        private int _PageSize = 5;

        public int PageSize
        {
            get => _PageSize;
            set => _PageSize = (value > maxPageSize) ? maxPageSize : value;
        }
        public string SearchQuery { get; set; }

        public string Fields { get; set; }
        public virtual string OrderBy { get; set; } = "Name";
    }
}
