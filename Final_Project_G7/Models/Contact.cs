using System.ComponentModel.DataAnnotations;


//----------------Final_Project_G7------------------------

//This code belongs to the Final_Project_G7.Models namespace
namespace Final_Project_G7.Models
{
    public class Contact
    {
        //Unique identifier for each contact
        public int ContactId { get; set; }

        //user ID from AspNetUser table.
        //This is used to link the contact to a specific user in the application
        public string? OwnerId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        //The status of the contact, represented by the ContactStatus enum
        public ContactStatus Status { get; set; }
    }

    //It is representing the status of a contact
    public enum ContactStatus
    {
        //The contact has been submitted but not yet processed
        Submitted,

        //The contact has been approved
        Approved,

        //The contact has been rejected
        Rejected
    }
}
