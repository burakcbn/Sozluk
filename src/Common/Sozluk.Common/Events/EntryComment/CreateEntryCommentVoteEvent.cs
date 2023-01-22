using Sozluk.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Common.Events.EntryComment
{
    public class CreateEntryCommentVoteEvent
    {
        public string EntryCommentId { get; set; }
        public Guid UserId{ get; set; }
        public VoteType VoteType{ get; set; }
    }
}
