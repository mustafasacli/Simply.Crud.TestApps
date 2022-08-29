using SimplyCrud.Project.Entity;
using System;
using System.Text;
using static System.DateTime;

namespace SimplyCrud.Project.Entites
{
    public class EntityBuilder
    {
        // Instantiate random number generator.
        private readonly Random random = new Random();

        public Person CreatePersonObject()
        {
            Random random = new Random();
            int year = random.Next(1900, Now.Year);
            int month = random.Next(1, 12);
            int day = random.Next(1, month == 2 ? 28 : 30);
            string fname = RandomString(40);
            string lname = RandomString(40);
            char gender = random.Next(0, 1) == 1 ? 'E' : 'K';

            return new Person
            {
                FirstName = fname,
                LastName = lname,
                Gender = gender,
                BirthDate = new DateTime(year, month, day),
                SsNo = RandomString(18)
            };
        }

        // Generates a random string with a given size.
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.

            // char is a single Unicode character
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26

            for (var i = 0; i < size; i++)
            {
                var @char = (char)random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}