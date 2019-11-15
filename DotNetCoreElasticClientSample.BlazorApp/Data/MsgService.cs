using DotNetCoreElasticClientSample.Lib;
using System;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace DotNetCoreElasticClientSample.BlazorApp.Data
{
    public class MsgService
    {
        private readonly IElasticClient _client;

        public MsgService(IElasticClient client)
        {
            _client = client;
        }

        public async Task<Msg[]> GetMsgList(string keyword)
        {
            var request = new SearchRequest<Msg>(Indices.All)
            {
                From = 0,
                Size = 100,
                Query = new MatchQuery
                {
                    Field = Infer.Field<Msg>(m => m.Message),
                    Query = keyword
                }
            };

            var ret = await _client.GetIndexSettings();

            var response = await _client.SearchAsync<Msg>(request);
            return response.Documents.ToArray();
        }
    }
}
