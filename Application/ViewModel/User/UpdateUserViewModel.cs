namespace Application.ViewModel.User;

public class UpdateUserViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Email { get; set; }

    public int UserTypeEnum { get; set; }
}
