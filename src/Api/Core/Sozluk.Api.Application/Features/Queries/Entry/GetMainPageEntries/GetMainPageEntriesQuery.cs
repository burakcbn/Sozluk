using MediatR;
using Sozluk.Common.Models.Page;
using Sozluk.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Queries.Entry.GetMainPageEntries
{
    public class GetMainPageEntriesQuery:BasePagedQuery,IRequest<PagedViewModel<GetEntryDetailViewModel>>
    {
        public GetMainPageEntriesQuery(Guid userId, int page, int pageSize) : base(page, pageSize)
        {
            UserId= userId;
        }

        public Guid? UserId { get; set; }
    }
}
