using System;
using System.Collections.Generic;

namespace DatabaseWorker;

public partial class MemberList
{
    public int Id { get; set; }

    public int ChatId { get; set; }

    public int UserId { get; set; }

    public string Role { get; set; } = null!;

    public int LastReadMessageId { get; set; }

    public virtual Chat Chat { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
