using Dapper;
using Sozluk.Common.Events.User;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Projections.UserService.Services
{
    public class UserService
    {
        private readonly string conStr;
        public UserService(IConfiguration configuration)
        {

            conStr = configuration["ConnectionStrings"];
        }

        public async Task<Guid> CreateEmailConfirmation(UserEmailChangeEvent @event)
        {
            var guid = Guid.NewGuid();
            using var connection = new SqlConnection(conStr);
            await connection.ExecuteAsync("INSERT INTO EMAILCONFIRMATION (Id,CreatedDate,OldEmailAddress,NewEmailAddress) VALUES(@Id,GETDATE(),@OldEmailAddress,@NewEmailAddress)",
                new
                {
                    Id = guid,
                    OldEmailAddress = @event.OldEmailAddress,
                    NewEmailAddress = @event.NewEmailAddress
                });

            return guid;
        }

    }
}
