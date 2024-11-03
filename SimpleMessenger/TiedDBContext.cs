using DatabaseWorker;

namespace SimpleMessenger
{
    public class TiedDBContext : SimpleMessengerContext
    {
        public TiedDBContext() : base()
        {
        }

        public override bool AddUser(string name, string email, string passwordHash, string salt)
        {
            Console.WriteLine("override");
            return false;
        }
    }
}
