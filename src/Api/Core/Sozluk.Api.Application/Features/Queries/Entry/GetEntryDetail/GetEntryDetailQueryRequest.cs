using MediatR;
using Sozluk.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Queries.Entry.GetEntryDetail
{
    public class GetEntryDetailQueryRequest : IRequest<GetEntryDetailViewModel>
    {
        public GetEntryDetailQueryRequest(string? entryId, Guid? userId)
        {
            EntryId = entryId;
            UserId = userId;
        }

        public string? EntryId { get; set; }
        public Guid? UserId { get; set; }
    }
}
