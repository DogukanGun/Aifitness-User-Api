namespace Aifitness_User_Api.Service.Models
{
    public class PasswordChangeRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
