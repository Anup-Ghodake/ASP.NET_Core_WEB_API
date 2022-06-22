namespace my_bookAPI.Data.Model
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        //Navigation properties
        public List<Book_Author> Book_Author { get; set; }
    }
}
