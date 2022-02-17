namespace ams_finstek_dotnet.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Location { get; set; }

        public string Email { get; set; }

        public bool isActive { get; set; }
    }
}
