namespace AspNetCoreAddin.Data.ViewModels
{
    public class BlogImageViewModel
    {
        public int Id { get; set; }

        public int BlogId { get; set; }

        public string Path { get; set; }

        public string Caption { get; set; }
    }
}