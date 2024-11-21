using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Euromonitor.International.Book.Core
{
    public class BookSubscriptionResponse
    {
        public int SubscriptionId {get; set;}

        public BookEntity Book { get; set; }

        public DateTime SubscriptionDate {get; set;}

        public DateTime? UnsubscriptionDate {get; set;}

        
    }
}