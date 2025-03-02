namespace E.Application.CQRS.Users.DTOs;

public record struct RegisterDto
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Surname { get; set; }
    public string FatherName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public string MobilePhone { get; set; }
    public string CardNumber { get; set; }
    public string TableNumber { get; set; }
    public DateTime Birthdate { get; set; }
    public DateTime DateOfEmployment { get; set; }
    public DateTime DateOfDissmissal { get; set; }
    public string Note { get; set; }
    public int Gender { get; set; }
    public int UserType { get; set; }
}
