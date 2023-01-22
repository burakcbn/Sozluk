using MediatR;
using Sozluk.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Queries.GetUserDetail
{
    public class GetUserDetailQueryRequest:IRequest<UserDetailViewModel>
    {
        public GetUserDetailQueryRequest(Guid userId, string userName=null)
        {
            UserId = userId;
            UserName = userName;
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}
