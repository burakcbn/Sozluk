﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Common.Events.EntryComment
{
    public class EntryCommentFavEvent
    {
        public string EntryCommentId { get; set; }
        public Guid UserId { get; set; }
    }
}
