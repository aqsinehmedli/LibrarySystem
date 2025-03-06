using B.Domain.Entities;
using B.Domain.Enums;

namespace E.Application.CQRS.Books.DTOs;

public record struct CreateBookDto
{
    public string Author { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Guid? CoverPhoto { get; set; }
    public User UserId { get; set; }
    public bool? ShowOnFirstScreen { get; set; }
    public int? Language { get; set; }
}
