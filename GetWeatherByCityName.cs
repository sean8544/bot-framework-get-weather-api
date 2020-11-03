using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections;

namespace Company.Function
{
    public static class GetWeatherByCityName
    {
        [FunctionName("GetWeatherByCityName")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get",  Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];
            
            string[] citys=new string[]{"beijing","shanghai","guangzhou","shenzhen"};
            
            
           if(Array.IndexOf(citys,name)==-1)
           {
               log.LogInformation($"city [{name}] not support,response 404.");

                return  await Task.Run<NotFoundObjectResult>( ()=>{
                  return new NotFoundObjectResult("This weather api only support beijing,shanghai,guangzhou,shenzhen.\r\n Example https://xxxxx/api/GetWeatherByCityName?name=beijing ");
                });


            }  



            return  await Task.Run<OkObjectResult>(() =>
                        {             
                        Weather item=new Weather();
                        item.Temperature=new Random().Next(10,40);
                        item.Humidity=new Random().Next(40,80);
                            return new OkObjectResult(item);
                        });

                    
                            
        }


        

        public class  Weather
        {
           public  double Temperature{get;set;}
        public double Humidity{get;set;}
            
        }
    }
}
