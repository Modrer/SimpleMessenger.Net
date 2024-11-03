namespace DatabaseWorker;

public partial class Chat
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int OwnerId { get; set; }

    public string Image { get; set; } = null!;

    public virtual ICollection<MemberList> MemberLists { get; set; } = new List<MemberList>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual User Owner { get; set; } = null!;
}
