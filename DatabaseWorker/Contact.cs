using System;
using System.Collections.Generic;

namespace DatabaseWorker;

public partial class Contact
{
    public int Id { get; set; }

    public int OwnerId { get; set; }

    public int ContactId { get; set; }

    public virtual User ContactNavigation { get; set; } = null!;

    public virtual User Owner { get; set; } = null!;
}
