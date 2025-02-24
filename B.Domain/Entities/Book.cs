using B.Domain.BaseEntities;
using B.Domain.Enums;

namespace B.Domain.Entities;

public class Book : BaseEntity
{
    public string Author { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Guid? CoverPhoto {  get; set; }
    public User UserId { get; set; }
    public bool? ShowOnFirstScreen { get; set; }
    public Languages Language { get; set; }
}
