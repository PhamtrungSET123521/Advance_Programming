using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestProject1.Data
{
    public class RestService2
    {
        HttpClient httpClient;

        //StringContent content = new StringContent("xin chao",UnicodeEncoding.UTF8,"application/json");
        public RestService2()
        {
            
        }
        public async Task<string> GetResponse2(string uri)
        {
            httpClient = new HttpClient(DependencyService.Get<IHTTPClientHandlerCreationService>().GetInsecureHandler());
            Uri requestUri = new Uri(uri);
            //Send the GET request asynchronously and retrieve the response as a string.
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            string httpResponseBody = "";
            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                //Console.WriteLine(httpResponseBody);
                
                return httpResponseBody;
            }
            catch 
            {
                return null;
            }
        }

        public async Task PostResponse2(string uri)
        {
            httpClient = new HttpClient(DependencyService.Get<IHTTPClientHandlerCreationService>().GetInsecureHandler());
            try
            {
                // Construct the HttpClient and Uri. This endpoint is for test purposes only.
                /*https://10.0.2.2:5001/home/send?command=off&&roomNumber=1 */
                Uri postUri = new Uri(uri);
                Debug.WriteLine(uri);
                // Construct the JSON to post.
                StringContent content = new StringContent("");

                // Post the JSON and wait for a response.
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(postUri, content);

                // Make sure the post succeeded, and write out the response.
                httpResponseMessage.EnsureSuccessStatusCode();
                var httpResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                Debug.WriteLine(httpResponseBody);
            }
            catch (Exception ex)
            {
                // Write out any exceptions.
                Debug.WriteLine("ERRRORRR"+ex);
            }
        }
    }
}

