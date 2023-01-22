using MediatR;
using Sozluk.Api.Domain.Models;
using Sozluk.Common.Models.Page;
using Sozluk.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Queries.GetUserEntriesDetail
{
    public class GetUserEntriesDetailQueryRequest : BasePagedQuery, IRequest<PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        public GetUserEntriesDetailQueryRequest(Guid? userId, string userName, int page, int pageSize) : base(page, pageSize)
        {
            UserId = userId;
            UserName = userName;
        }

        public Guid? UserId { get; set; }
        public string UserName { get; set; }
    }
}
