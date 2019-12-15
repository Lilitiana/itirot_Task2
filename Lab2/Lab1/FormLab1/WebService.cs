using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Lab1.Models;
using Newtonsoft.Json;

namespace FormLab1
{
    public class WebService
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<bool> Login(string login,string password)
        {
            var response = await client.PostAsync("http://localhost:50734/api/lab2/login/?login=" + login+"&password="+password, null);
            return Convert.ToBoolean(await response.Content.ReadAsStringAsync());
        }

        public static async Task<bool> CheckUpdates(string login)
        {
            var responseString = await client.GetStringAsync("http://localhost:50734/api/lab2/isupdate/?login=" + login);
            return Convert.ToBoolean(responseString);
        }

        public static async Task<List<Message>> GetWall(string login)
        {
            var responseString = await client.GetStringAsync("http://localhost:50734/api/lab2/getwall/?login=" + login);
            return JsonConvert.DeserializeObject<List<Message>>(responseString);
        }

        public static async Task SendMessage(string message, string login)
        {
            var response = await client.PostAsync("http://localhost:50734/api/lab2/sendmessage/?message=" + message + "&login=" + login, null);
            await response.Content.ReadAsStringAsync();
        }
    }
}
