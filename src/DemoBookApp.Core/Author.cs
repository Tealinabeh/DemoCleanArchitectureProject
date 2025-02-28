namespace DemoBookApp.Core;

public record Author
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "Jane";
    public string Surname { get; set; } = "Doe";
    public DateOnly DateOfBirth { get; set; }
    public List<Book>? IssuedBooks { get; set; }
}
