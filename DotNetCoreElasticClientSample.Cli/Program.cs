using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCoreElasticClientSample.Lib;
using DotNetCoreElasticClientSample.Lib.Extensions;
using Nest;
using Newtonsoft.Json;

namespace DotNetCoreElasticClientSample.Cli
{
    class Program
    {
        static async Task Main()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("msg");

            var client = new ElasticClient(settings);

            var reader = new StreamReader(@".\msg.json", Encoding.UTF8);
            var msg = JsonConvert.DeserializeObject<List<Msg>>(reader.ReadToEnd());
            msg = msg.Distinct(_ => _.Message).ToList();

            //foreach (var m in msg)
            //{
            //    var response = client.IndexDocument(m);
            //    System.Console.WriteLine(response.Result);
            //}

            var request = new SearchRequest<Msg>(Indices.All)
            {
                From = 0,
                Size = 100,
                Query = new MatchQuery
                {
                    Field = Infer.Field<Msg>(m => m.Message),
                    Query = "アイスソード"
                }
            };

            var response2 = await client.SearchAsync<Msg>(request);
            foreach (var d in response2.Documents)
            {
                Console.WriteLine(d.Message);
            }

        }
    }
}
