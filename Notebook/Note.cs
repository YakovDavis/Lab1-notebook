using System;
using System.Collections.Generic;
using System.Text;

namespace Notebook
{
    public class Note
    {
        private static int count;
        private int id;
        private string firstName;
        private string lastName;
        private string patronymicName;
        private string phoneNumber;
        private string country;
        private string dateOfBirth;
        private string organization;
        private string position;
        private string otherNotes;

        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PatronymicName { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string DateOfBirth { get; set; }
        public string Organization { get; set; }
        public string Position { get; set; }
        public string OtherNotes { get; set; }

        public string DetailedInfo()
        {
            string res = $"First name: {FirstName}\nLast name: {LastName}\n";
            if (PatronymicName != null)
                res += "Patronymic: " + PatronymicName + "\n";
            res += "Phone number: " + PhoneNumber + "\n";
            res += "Country: " + Country + "\n";
            if (DateOfBirth != null)
                res += "Date of birth: " + DateOfBirth + "\n";
            if (Organization != null)
                res += "Organization: " + Organization + "\n";
            if (Position != null)
                res += "Position: " + Position + "\n";
            if (OtherNotes != null)
                res += "Other Info: " + OtherNotes;
            return res;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} {PhoneNumber}";
        }

        public Note(string firstName, string lastName, string phoneNumber, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Country = country;
            Id = count;
            count++;
        }
    }
}
