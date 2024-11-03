namespace DatabaseWorker;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string Salt { get; set; } = null!;

    public string Image { get; set; } = null!;

    public virtual IEnumerable<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ICollection<Contact> ContactContactNavigations { get; set; } = new List<Contact>();

    public virtual ICollection<Contact> ContactOwners { get; set; } = new List<Contact>();

    public virtual ICollection<MemberList> MemberLists { get; set; } = new List<MemberList>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
