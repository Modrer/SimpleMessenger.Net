using System;
using System.Collections.Generic;

namespace DatabaseWorker;

public partial class Message
{
    public int Id { get; set; }

    public int ChatId { get; set; }

    public int SenderId { get; set; }

    public string Message1 { get; set; } = null!;

    public virtual Chat Chat { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
