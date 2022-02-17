namespace ams_finstek_dotnet.Data.Entities
{
    public class Access
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }

        public string UserServiceLogin { get; set; }

        public bool HasAccess { get; set; }
    }
}
