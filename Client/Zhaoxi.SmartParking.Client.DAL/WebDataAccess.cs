using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.SmartParking.Client.DAL
{
    public class WebDataAccess
    {
        private string _domain = "localhost:5000/api/";

        public async Task<string> GetDatas(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var httpResponse = await client.GetAsync(_domain + url);

                return await httpResponse.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> PostDatas(string url, HttpContent httpContent)
        {
            using (HttpClient client = new HttpClient())
            {
                var httpResponse = await client.PostAsync(_domain + url, httpContent);

                return await httpResponse.Content.ReadAsStringAsync();
            }
        }
    }
}
