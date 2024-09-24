namespace PersonalInfoWebAPI.Models
{
    public class PersonalInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ResidentialAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public string Occupation { get; set; }
        public string AadharCardNumber { get; set; }
        public string PANNumber { get; set; }
    }
}
