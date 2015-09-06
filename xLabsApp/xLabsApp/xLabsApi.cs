using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace xLabsApp
{
    public static class xLabsApi
    {
        const string baseApiUrl = "https://xlab.bluemix.net";
        
        public static async void GetPersonalityInfo(User user)
        {            
            var bio = user.bio;
            string statusMessages = "";
            foreach (var item in user.posts.data)
	        {
                statusMessages += item.message;		 
            }
            var inputText = bio + ". " + statusMessages;
            using(var httpClient = new HttpClient())
	        {
                var stringData = new StringContent(JsonConvert.SerializeObject(new { text = inputText}),Encoding.UTF8,"application/json");
                var response = await httpClient.PostAsync(baseApiUrl + "/map", stringData);
                var json = await response.Content.ReadAsStringAsync();
                user.personality = json;
                //Console.WriteLine("Personality : " + json);
                /*
                 * .ContinueWith(r =>
                {
                    string json = r.Result.GetResponseText();
                    user.personality = json;
                });
                 */
            }
        }
    }
}
