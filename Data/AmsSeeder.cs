using ams_finstek_dotnet.Data.Entities;
using System.Text.Json;

namespace ams_finstek_dotnet.Data
{
    public class AmsSeeder
    {
        private readonly AmsContext _ctx;
        private readonly IWebHostEnvironment _env;
        public AmsSeeder(AmsContext ctx, IWebHostEnvironment env)
        {
            _ctx = ctx;
            _env = env;
        }

        public IWebHostEnvironment Env { get; }

        public void Seed()
        {
            _ctx.Database.EnsureCreated();

            if (!_ctx.Users.Any())
            {
                //need to crete sample data
                var filePath = Path.Combine(_env.ContentRootPath,"Data/art.json");
                var json = File.ReadAllText(filePath);
                var users = JsonSerializer.Deserialize<IEnumerable<User>>(json);

                _ctx.Users.AddRange(users);
                
                _ctx.SaveChanges();
            }
        }
    }
}
