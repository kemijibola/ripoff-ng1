using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ripoffnigeria.DTO;
using ripoffnigeriaonline.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ripoffnigeriaonline.Helpers
{
    public class ConsumeWebApi
    {
        public  async Task<PhotoViewModel> GetReportPhoto(Report report)
        {
            var client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("http://rip-offnigeria.com/api/reportimage?id=" + report.Id).Result;
            if (response.IsSuccessStatusCode)
            {
                var data =await  response.Content.ReadAsStringAsync();

                dynamic a = JValue.Parse(data.ToString());
                
                foreach (var d in a)
                {
                    var obj = new JObject(d);
                    foreach (var o in obj)
                    {
                        return  JsonConvert.DeserializeObject<PhotoViewModel>(o.Value.ToString());
                    }                                   
                }                                    
                return null;              
            }
            return null;
        }
    }
}
