using Lab1.Models;
using System.Data.Entity;

namespace Lab2.Models
{
    public class Lab2Context : DbContext
    {
        public Lab2Context() : base("DefaultConnection")
        {
        }
        public DbSet<Message> Messages { get; set; }
    }
}