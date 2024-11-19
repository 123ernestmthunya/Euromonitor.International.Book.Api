using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Euromonitor.International.Book.Core;
public record Subscription(
    int SubscriptionID,
    int UserID,
    int BookID,
    DateTime StartDate,
    DateTime EndDate,
    User User,
    Book Book
);
