using Entities.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Infrastructure.Test
{
    public class UnitTest1
    {
        [Fact]
        public void InsertUser()
        {
            string name = "Test";
            string email = "nicholas.nascimento@gmail.com";

            var user = new User
            {
                Name = name,
                Email = email
            };

        }
    }
}