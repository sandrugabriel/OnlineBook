namespace OnlineBookShop.Books.Dto
{
    public class CreateBookRequest
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string NameCategory { get; set; }

        public int PublicationYear { get; set; }

        public int AvailableCopies { get; set; }
    }
}
