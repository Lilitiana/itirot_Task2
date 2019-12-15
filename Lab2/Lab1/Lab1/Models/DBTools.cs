using Lab1.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class DBTools
    {
        public static async Task WriteToDb(Message message)
        {
            using (Lab2Context db = new Lab2Context())
            {
                db.Messages.Add(message);
                await db.SaveChangesAsync();
            }
        }

        public static async Task<List<Message>> ReadDb()
        {
            using (Lab2Context db = new Lab2Context())
            {
                return await db.Messages.ToListAsync();
            }
        }
    }
}