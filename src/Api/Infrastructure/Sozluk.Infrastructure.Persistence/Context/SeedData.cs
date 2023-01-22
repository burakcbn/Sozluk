using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sozluk.Api.Domain.Models;
using Sozluk.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Infrastructure.Persistence.Context
{
    internal class SeedData
    {
        private static List<User> GetUsers()
        {
            var result = new Faker<User>("tr")
                .RuleFor(u => u.Id, u => new Guid())
                .RuleFor(u => u.CreatedDate, u => u.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(u => u.FirstName, u => u.Person.FirstName)
                .RuleFor(u => u.LastName, u => u.Person.LastName)
                .RuleFor(u => u.EmailAddress, u => u.Internet.Email())
                .RuleFor(u => u.UserName, u => u.Internet.UserName())
                .RuleFor(u => u.Password, u => PasswordEncryptor.Encrpt(u.Internet.Password()))
                .RuleFor(u => u.EmailConfirmed, u => u.PickRandom(true, false))
                    .Generate(500);
            return result;
        }
        public async Task SeedAsync(IConfiguration configuration)
        {
            var dbOptions = new DbContextOptionsBuilder();
            dbOptions.UseSqlServer(configuration["ConnectionStrings"]);

            var context = new SozlukContext(dbOptions.Options);

            var users = GetUsers();
            var userIds = users.Select(u => u.Id);
            await context.User.AddRangeAsync(users);


            var guids = Enumerable.Range(0, 150).Select(i => Guid.NewGuid()).ToList();
            int counter = 0;

            var entries = new Faker<Entry>("tr")
                .RuleFor(u => u.Id, u => guids[counter++])
                .RuleFor(u => u.CreatedDate, u => u.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(u => u.Subject, u => u.Lorem.Sentence(5, 5))
                .RuleFor(u => u.Content, u => u.Lorem.Paragraph(2))
                .RuleFor(u => u.CreatedById, u => u.PickRandom(userIds))
                .Generate(150);

            await context.Entries.AddRangeAsync(entries);

            var comments = new Faker<EntryComment>("tr")
                .RuleFor(u => u.Id, u => Guid.NewGuid())
                .RuleFor(u => u.CreatedDate, u => u.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(u => u.Content, u => u.Lorem.Paragraph(2))
                .RuleFor(u => u.CreatedById, u => u.PickRandom(userIds))
                .RuleFor(u => u.EntryId, u => u.PickRandom(guids))
                .Generate(1000);

            await context.EntryComments.AddRangeAsync(comments);
            await context.SaveChangesAsync();

        }
    }
}
