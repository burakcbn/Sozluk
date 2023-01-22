using Sozluk.Api.Domain.Models.Common;
using Sozluk.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Domain.Models
{
    public class EntryCommentVote:BaseEntity
    {
        public Guid EntryCommentId { get; set; }
        public Guid CreateById{ get; set; }
        public VoteType VoteType{ get; set; }
        public virtual EntryComment EntryComment { get; set; }
    }
}
