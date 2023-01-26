using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Common.Events.Entry
{
    public class CreateEntryFavEvent
    {
        public string EntryId { get; set; }
        public Guid CreatedById{ get; set; }

    }
}
