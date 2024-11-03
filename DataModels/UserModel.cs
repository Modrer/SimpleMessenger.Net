namespace DataModels
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password_Hash { get; set; }
        public string Salt { get; set; }
        public string Image { get; set; }
    }
}
