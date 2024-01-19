namespace BankServer.Models
{
    public class ChangePasswordInput
    {
        public string? CurrentPassword { get; set; }

        public string? NewPassword { get; set; }
    }
}
