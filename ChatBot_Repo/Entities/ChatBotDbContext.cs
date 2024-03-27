using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Repo.Entities
{
    public class ChatBotDbContext : DbContext
    {
        //    public ChatBotDbContext()
        //    {

        //    }
        //    public ChatBotDbContext(DbContextOptions<ChatBotDbContext> options) : base() { }

        //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    {
        //        optionsBuilder.UseSqlServer(GetConnectionString());
        //    }
        //    private string GetConnectionString()
        //    {
        //        return "Data Source=(local);database=ChatBotDb2024;uid=sa;pwd=12345;TrustServerCertificate=True;MultipleActiveResultSets=True";
        //    }
        //    public DbSet<Conversation> Conversations { get; set; }  
        //    public DbSet<Message> Messages { get; set; }
        //    protected override void OnModelCreating(ModelBuilder modelBuilder)
        //    {
        //        base.OnModelCreating(modelBuilder);
        //        modelBuilder.Entity<Conversation>()
        //            .HasMany(e => e.Messages)
        //            .WithOne(e => e.Conversation)
        //            .OnDelete(DeleteBehavior.Cascade);

        //    }

    }
    
}
