using Microsoft.VisualStudio.TestTools.UnitTesting;
 
 
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using System;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;

 
using Microsoft.Extensions.Configuration;
using blasa.tarvel.DependencyInjectionContainer;

namespace blasa.travel.web.Tests
{
    [TestClass]
    public class Travel
    {
         private readonly TestServer server;
        private readonly HttpClient client;


        Travel() {


            var webHostBuilder = new WebHostBuilder()
                  .UseStartup<Startup>()
                  .ConfigureAppConfiguration((builderContext, config) =>
                  {
                      //IHostingEnvironment env = builderContext.HostingEnvironment;
                      //config.AddJsonFile("autofac.json")
                      //.AddEnvironmentVariables();
                  });
                 // .ConfigureServices(services => DependencyInjectionContainer.RegisterServicesTest(services));

            server = new TestServer(webHostBuilder);
            client = server.CreateClient();
        }
    

    [TestMethod]
        public void TestMethod1()

        {
            
            
             
        }


        private async Task Withdraw(string account, double amount)
        {
            var json = new
            {
                accountId = account,
                amount = amount,
            };

            string data = JsonConvert.SerializeObject(json);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await client.PatchAsync(" api/Travel/Post", content);
            string result = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
        }
    }
}
