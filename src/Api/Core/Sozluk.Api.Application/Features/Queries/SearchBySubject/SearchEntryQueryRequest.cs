using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sozluk.Common.Models.Queries;

namespace Sozluk.Api.Application.Features.Queries.SearchBySubject
{
    public class SearchEntryQueryRequest:IRequest<List<SearchEntryViewModel>>
    {
        public string SearchText { get; set; }

        public SearchEntryQueryRequest(string searchText)
        {
            SearchText = searchText;
        }
    }
}
