using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Euromonitor.International.Book.Core;
public class SubscriptionEntity{
    public int SubscriptionID{get;set;}
    public int UserID {get;set;}
    public int BookID {get;set;}
    public DateTime StartDate {get; set;}
    public DateTime EndDate {get; set;}
    public RegisterEntity User {get; set;}
    public BookEntity Book {get; set;}
}
