using UniSystem.Models.Common;
using UniSystem.Models.Enums;

namespace UniSystem.Models.Entities.User
{
    public class User : IEntity, IAuditable
    {
        public User(string firstName, string middleName, string lastName, DateTime birthDate, Gender gender)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            BirthDate = birthDate;
            Gender = gender;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreatorUserId { get; set; }
    }
}