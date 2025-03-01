namespace DemoBookApp.Core;

public class Author
{
    public long Id { get; set; }    
    public string Name { get; set; } = "";
    public string Surname { get; set; } = "";
    public DateOnly DateOfBirth { get; set; }
    public List<Book> IssuedBooks { get; set; } = new();
};
