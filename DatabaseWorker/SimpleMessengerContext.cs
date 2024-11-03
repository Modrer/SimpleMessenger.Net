using System.Configuration;
using DatabaseWorker.FlatModels;
using Microsoft.EntityFrameworkCore;


namespace DatabaseWorker;

public partial class SimpleMessengerContext : DbContext
{
    public SimpleMessengerContext()
    {
    }

    public SimpleMessengerContext(DbContextOptions<SimpleMessengerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<MemberList> MemberLists { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = ConfigurationManager.ConnectionStrings["Default"];

        if (connectionString == null)
        {
            throw new Exception("Do not have connection string");
        }

        optionsBuilder.UseMySql(
            connectionString.ConnectionString,
            ServerVersion.Parse("8.0.35-mysql")
            );
    }
    //Chat section 
    #region Chat
    public virtual IEnumerable<PersonalChat> GetAllChats(int userId)
    {
        return Database.SqlQueryRaw<PersonalChat>("call GetAllChats({0});", userId);
    }

    public virtual FlatChat? GetChat(int chatId)
    {

        return Database.SqlQueryRaw<FlatChat>("call GetChat({0});", chatId)
            .AsEnumerable().FirstOrDefault();
    }
    public virtual FlatChat? CreateChat(int ownerId, string name, string image = "default.png")
    {

        return Database.SqlQueryRaw<FlatChat>("call CreateChat({0},{1},{2});", name, ownerId, image)
            .AsEnumerable()
            .FirstOrDefault();
    }

    public virtual FlatChat? UpdateChat(int chatId, string name, string? image)
    {

        return image switch
        {
            null => Database.SqlQueryRaw<FlatChat>("call UpdateChatName({0},{1});", chatId, name)
            .AsEnumerable().FirstOrDefault(),
            _ => Database.SqlQueryRaw<FlatChat>("call UpdateChat({0},{1},{2});", chatId, name, image)
            .AsEnumerable().FirstOrDefault()
        };
            
    }

    public virtual bool DeleteChat(int chatId, int ownerId)
    {
        try
        {
            Database.SqlQueryRaw<FlatChat>("call DeleteChat({0},{1});", chatId, ownerId);
            return true;
        }
        catch {
            return false;
        }
        
    }
    #endregion Chat

    #region Members
    public virtual IEnumerable<PublicUser> GetMembers(int chatId)
    {
        return Database.SqlQueryRaw<PublicUser>("call GetAllMembers({0});", chatId);
    }
    public virtual bool AddMember(int chatId, int userId)
    {
        try
        {
            Database.ExecuteSqlRaw("call AddChatMember({0},{1});", chatId, userId);
        }
        catch {
            return false;
        }
        return true;
    }
    public virtual bool RemoveMember(int chatId, int userId)
    {
        try
        {
            Database.ExecuteSqlRaw("call RemoveChatMember({0},{1});", chatId, userId);
        }
        catch
        {
            return false;
        }
        return true;
        
    }
    #endregion Members

    #region Messages
    public virtual void SetLastReadMessage(int chatId, int userId, int messageId)
    {

        Database.ExecuteSqlRaw("call SetLastReadedMessage({0},{1},{2});", chatId, userId, messageId);
    }
    public virtual IEnumerable<FlatMessage> GetMessages(int chatId)
    {
        return Database.SqlQueryRaw<FlatMessage>("call GetMessages({0});", chatId);
    }
    public virtual FlatMessage? SendMessage(int senderId, int chatId, string text)
    {

        return Database.SqlQueryRaw<FlatMessage>
            ("call SendMessage({0},{1},{2});", senderId, chatId, text)
            .AsEnumerable().FirstOrDefault();
    }

    #endregion Messages

    #region Contacts
    public virtual IEnumerable<PublicUser> GetContacts(int userId)
    {
        return Database.SqlQueryRaw<PublicUser>("call GetAllContacts({0});", userId);
    }
    public virtual FlatContact? AddContact(int ownerId, int contactId)
    {

        return Database
            .SqlQueryRaw<FlatContact>("call AddContact({0},{1});", ownerId, contactId)
            .AsEnumerable().First();
    }
    public virtual void RemoveContact(int ownerId, int contactId)
    {

        Database.ExecuteSqlRaw("call RemoveContact({0},{1});", ownerId, contactId);
    }
    #endregion Contacts

    #region Users
    public virtual IEnumerable<PublicUser> GetUsersByName(string name)
    {
        return Database.SqlQueryRaw<FlatUser>("call FindUsersByName({0});", name).AsEnumerable()
            .Select(x => new PublicUser
            {
                Name = x.Name,
                Image = x.Image,
                Id = x.Id
            });
    }
    public virtual FlatUser? GetUserByName(string name)
    {
        return Database.SqlQueryRaw<FlatUser>("call FindUserByName({0});", name).AsEnumerable().FirstOrDefault();
    }
    public virtual PublicUser? GetUserById(int id)
    {
        var user = Database.SqlQueryRaw<FlatUser>("call FindUserById({0});", id)
            .AsEnumerable().First();

        if (user == null)
        {
            return null;
        }

        return new PublicUser
        {
            Id = user.Id,
            Name = user.Name,
            Image = user.Image
        };

    }
    public virtual bool ContainUser(string userName)
    {
        return
            Database
            .SqlQueryRaw<int>("Select 1 from Users where Name = {0};", userName)
            .AsEnumerable().Any();

    }
    public virtual bool AddUser(string name, string email, string passwordHash, string salt)
    {
        try
        {

            Database.ExecuteSqlRaw("call AddUser({0}, {1}, {2}, {3});",
                name, email, passwordHash, salt);
        }
        catch (Exception)
        {
            return false;
        }
        return true;

    }
    public virtual bool UpdateUser(int id, string name, string email, string image)
    {
        try
        {
            Database
                .ExecuteSqlRaw("call UpdateUser({0}, {1}, {2}, {3});",
                id, name, email, image);
        }
        catch (Exception _e)
        {
            return false;
        }
        return true;

    }
    public virtual bool ChangePassword(int id, string passwordHash, string salt)
    {
        try
        {
            Database
                .ExecuteSqlRaw("call ChangePassword({0}, {1}, {2});",
                id, passwordHash, salt);
        }
        catch (Exception _e)
        {
            return false;
        }
        return true;

    }
    #endregion Users
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Chat");

            entity.HasIndex(e => e.Id, "Id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.OwnerId, "Owner_idx");

            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .HasDefaultValueSql("'default.png'")
                .HasColumnName("image");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Owner).WithMany(p => p.Chats)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Admin");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.ContactId, "Contact_idx");

            entity.HasIndex(e => e.Id, "Id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.OwnerId, "Owner_idx");

            entity.HasOne(d => d.ContactNavigation).WithMany(p => p.ContactContactNavigations)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Contact");

            entity.HasOne(d => d.Owner).WithMany(p => p.ContactOwners)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Owner");
        });

        modelBuilder.Entity<MemberList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("MemberList");

            entity.HasIndex(e => e.ChatId, "ChatMessage_idx");

            entity.HasIndex(e => e.Id, "Id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.UserId, "Member_idx");

            entity.Property(e => e.Role)
                .HasMaxLength(45)
                .HasDefaultValueSql("'User'");

            entity.HasOne(d => d.Chat).WithMany(p => p.MemberLists)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ChatMessage");

            entity.HasOne(d => d.User).WithMany(p => p.MemberLists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Member");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.ChatId, "Chat_idx");

            entity.HasIndex(e => e.Id, "Id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.SenderId, "Sender_idx");

            entity.Property(e => e.Message1)
                .HasMaxLength(1024)
                .HasColumnName("Message");

            entity.HasOne(d => d.Chat).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Chat");

            entity.HasOne(d => d.Sender).WithMany(p => p.Messages)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Sender");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.Email, "Email_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Name, "Name_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Image)
                .HasMaxLength(45)
                .HasDefaultValueSql("'default.png'")
                .HasColumnName("image");
            entity.Property(e => e.Name).HasMaxLength(16);
            entity.Property(e => e.Password).HasMaxLength(64);
            entity.Property(e => e.Salt).HasMaxLength(88);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
