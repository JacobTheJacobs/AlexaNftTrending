using Alexa.NET.CustomerProfile;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AlexaNftTrending.Controllers
{
  

    [ApiController]
    [Route("[controller]")]
    public class AlexaController : Controller
    {
       

        [HttpPost,Route("/process")]
        public async Task<SkillResponse> IndexAsync(SkillRequest input)
        {
            SkillResponse output = new SkillResponse();
            output.Version = "1.0";
            output.Response = new ResponseBody();


            string URL = "https://nfttrendingapi.azurewebsites.net/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            IList<Res> collectionList = new List<Res>();
            string name ;
            string image_url ;
            string created_date;
            string description ;
            string external_url ;
            string count;
            Decimal market_cap;
            string num_owners ;
            Decimal floor_price ;
            Decimal total_volume ;
            Decimal average_price ;
            string sales;
            Decimal usdVolume;
            HttpResponseMessage response = client.GetAsync(URL).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                dynamic json = JsonConvert.DeserializeObject(data);
                //iterate over stats and print cellection name
                foreach (var item in json)
                {
                     name = item.name;
                     image_url = item.image_url;
                     created_date = item.created_date;
                     description = item.description;
                     external_url = item.external_url;
                     count = item.count;
                     market_cap = item.market_cap;
                     num_owners = item.num_owners;
                     floor_price = item.floor_price;
                     total_volume = item.total_volume;
                     average_price = item.average_price;
                     sales = item.sales;
                     usdVolume = item.usdVolume;

                    collectionList.Add(new Res(name,
                        image_url, created_date, 
                        description, external_url, 
                        count, market_cap, num_owners, 
                        floor_price, total_volume,
                        average_price, sales, usdVolume));

                    Console.WriteLine(name);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            try
            {

                switch (input.Request.Type)
                {
                    case "LaunchRequest":
                        output.Response.OutputSpeech = new PlainTextOutputSpeech("Hello Human! Are you ready to explore the nft universe?. Try saying top trending nft.");
                        output.Response.ShouldEndSession = false;
                        break;

                    case "IntentRequest":
                        IntentRequest intentRequest = (IntentRequest)input.Request;
                        switch (intentRequest.Intent.Name)
                        {
                            case "top_trending_nft":
                                name = collectionList[0].name;
                                image_url = collectionList[0].image_url;
                                created_date = collectionList[0].created_date;
                                description = collectionList[0].description;
                                external_url = collectionList[0].external_url;
                                count = collectionList[0].count;

                                market_cap = Decimal.Round(collectionList[0].market_cap);
                                num_owners = collectionList[0].num_owners;
                                floor_price = Decimal.Round(collectionList[0].floor_price);
                                total_volume = Decimal.Round(collectionList[0].total_volume);
                                average_price = collectionList[0].average_price;
                                sales = collectionList[0].sales;
                                usdVolume = Decimal.Round(collectionList[0].usdVolume);
                                output.Response.OutputSpeech = new PlainTextOutputSpeech(
                                    "The Top NFT, for today. It is called . " + name + " .    " +
                                     description + " . " +
                                     " Has a Number, in sales of, " + sales + " in usd dollars." +
                                    " Number of owners, of the collection, is, " + num_owners + " . "
                                    + " Market Cap, of this collection, is, " + market_cap + "$  . "
                                    + " Total available units, of this collection, is " + count + " . "
                                    + " The Average Price, of this collection, is " + average_price + "$ . "
                                    + " Total volume, of this collection, in USD dollars is, " + usdVolume +
                                    "$. Would you like, to hear another one?." +
                                    "Say, what other nft out there");

                                output.Response.ShouldEndSession = false;

                                break;

                            case "give_me_another_one":
                                Random r = new Random();
                                int number = r.Next(2, 19);
                                name = collectionList[number].name;
                                image_url = collectionList[number].image_url;
                                created_date = collectionList[number].created_date;
                                description = collectionList[number].description;
                                external_url = collectionList[number].external_url;
                                count = collectionList[number].count;
                                market_cap = Decimal.Round(collectionList[number].market_cap);
                                num_owners = collectionList[number].num_owners;
                                floor_price = Decimal.Round(collectionList[number].floor_price);
                                total_volume = Decimal.Round(collectionList[number].total_volume);
                                average_price = collectionList[number].average_price;
                                sales = collectionList[number].sales;
                                usdVolume = Decimal.Round(collectionList[number].usdVolume);
                                output.Response.OutputSpeech = new PlainTextOutputSpeech(
                                    "The Top NFT, in the list today. It is called . " + name + " .    " +
                                     description + " . " +
                                     " Has a Number, in sales of, " + sales + " in usd dollars." +
                                    " Number of owners, of the collection, is, " + num_owners + " . "
                                    + " Market Cap, of this collection, is, " + market_cap + "$  . "
                                    + " Total available units, of this collection, is " + count + " . "
                                    + " The Average Price, of this collection, is " + average_price + "$ . "
                                    + " Total volume, of this collection, in USD dollars is, " + usdVolume +
                                    "$. Would you like, to hear another one?." +
                                    "Say, what other nft out there");

                                output.Response.ShouldEndSession = false;
                                break;
                    
                            case "AMAZON.StopIntent":
                                output.Response.OutputSpeech =
                                         new PlainTextOutputSpeech("Goodbye! See you soon.");
                                output.Response.ShouldEndSession = true;
                                break;
                            case "AMAZON.FallbackIntent":
                                output.Response.OutputSpeech =
                                    new PlainTextOutputSpeech("Sorry! can you repeat that?");
                                output.Response.ShouldEndSession = false;
                                break;
                            case "AMAZON.NavigateHomeIntent	":
                                output.Response.OutputSpeech =
                                    new PlainTextOutputSpeech("Goodbye! See you soon.");
                                output.Response.ShouldEndSession = true;
                                break;
                            case "AMAZON.CancelIntent":
                                output.Response.OutputSpeech =
                                    new PlainTextOutputSpeech("Goodbye! See you soon.");
                                output.Response.ShouldEndSession = true;
                                break;
                            case "AMAZON.HelpIntent":
                                output.Response.OutputSpeech = new PlainTextOutputSpeech("Hello Human! Are you ready to explore the nft universe?. Try saying, what is the top nft for today.");
                                output.Response.ShouldEndSession = false;
                                break;
                            default:
                                output.Response.OutputSpeech =
                                         new PlainTextOutputSpeech("Sorry! I am still learning that. Please try later!");
                                output.Response.ShouldEndSession = false;
                                break;
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                output.Response.OutputSpeech =
                                    new PlainTextOutputSpeech("Sorry I could not process that. Please try again after some time!");
                output.Response.ShouldEndSession = true;
            }
            //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            // client.Dispose();
            return output;
        }
    }
}
