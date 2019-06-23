using System;
using MakingSense.Blogging.WebAPI.DataAccess;

namespace MakingSense.Blogging.Test
{
    public static class DbContextExtensions
    {
        public static void Seed(this DataContext dbContext)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash("123456", out passwordHash, out passwordSalt);

            dbContext.Users.Add(new WebAPI.Entities.User
            {
                FirstName = "Fernando",
                LastName = "Rossi",
                Username = "frossi",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            });

            dbContext.Posts.AddRange(
                new WebAPI.Entities.Post
                {
                    IdOwner = 1,
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now,
                    Title = "Primer Post público",
                    Content = "Texto del primer post.",
                    State = WebAPI.Enums.States.Public
                },
                new WebAPI.Entities.Post
                {
                    IdOwner = 1,
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now,
                    Title = "Segundo Post",
                    Content = "También publico pero acá lo pongo sin acento",
                    State = WebAPI.Enums.States.Public
                },
                 new WebAPI.Entities.Post
                 {
                     IdOwner = 1,
                     CreationDate = DateTime.Now,
                     LastModificationDate = DateTime.Now,
                     Title = "post privado",
                     Content = "privado",
                     State = WebAPI.Enums.States.Private
                 }


                );

            dbContext.SaveChanges();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }


}



