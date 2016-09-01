using Microsoft.EntityFrameworkCore;

namespace LoginRegWithEF.Models
{
    public partial class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        { }
        
        public virtual DbSet<User> User { get; set; }
    }
}