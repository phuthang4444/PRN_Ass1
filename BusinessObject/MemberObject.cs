using System;

namespace BusinessObject
{
    public class MemberObject
    {
        // Field / Data
        private int _MemberID;
        private string _Password;

        // Properties / Attributes
        public int MemberID { get => _MemberID; set => _MemberID = value; }
        public string MemberName { get; set; }
        public string Email { get; set; }
        public string Password { get => _Password; set => _Password = value; }
        public string City { get; set; }
        public string Country { get; set; }

        // Constructors
        public MemberObject() { }
        public MemberObject(int memberID, string memberName, string email, string password, string city, string country)
        {
            MemberID = memberID;
            MemberName = memberName;
            Email = email;
            Password = password;
            City = city;
            Country = country;
        }
    }
}
