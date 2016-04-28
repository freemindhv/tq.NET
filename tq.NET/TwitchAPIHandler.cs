/* 
tq.NET - simple commandline twitch client
Copyright (C) 2015  Dennis Greiner

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace tq.NET {
    class TwitchAPIHandler : IQuery {
        List<Query> querylist = new List<Query>();
        HttpClient client = new HttpClient();
        
        public TwitchAPIHandler() {
            client.BaseAddress = new Uri("https://api.twitch.tv/kraken/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.twitchtv.v3+json"));
            client.DefaultRequestHeaders.Add("Client-ID", "tq.net");
        }

        public string querystring { get; set; }

        public string queryoptions { get; set; }

        public string querygroupoptions { get; set; }

        public void add_query(Query query) {
            this.querylist.Add(query);
        }

        public List<Query> get_queries() {

            return this.querylist;
        }

        public IEnumerable<Result> handle_queries() {

            /* implement feature that checks for multiple occurances
            of the same type and concatenate them to speed things up 
            Also the http clint should be moved here so we can reuse
            the same connection multiple times                       */
            List<Result> resultlist = new List<Result>();
            Dictionary<Type, List<Query>> grouped_queries = new Dictionary<Type, List<Query>>();

            foreach (var query in this.querylist) {
                var querytype = query.GetType();
                if (grouped_queries.ContainsKey(querytype)) {
                    grouped_queries[querytype].Add(query);
                } else {
                    var tmplist = new List<Query>();
                    tmplist.Add(query);
                    grouped_queries.Add(querytype, tmplist);
                }

            }

            foreach (var entry in this.querylist) {
                var querytype = entry.GetType();
                if (grouped_queries.ContainsKey(querytype)) {
                    foreach (var query in grouped_queries[querytype]) {
                        // now handle the grouped queries as one
                        if (query.querygroupoptions != null) {
                            queryoptions = query.querygroupoptions;
                            if (this.querystring != null) {
                                this.querystring = String.Join(",", this.querystring, query.querystring);
                            }
                            else {
                                this.querystring = query.querystring;
                            }
                        }
                        else {
                            foreach (var result in entry.get_streamlist()) {
                                resultlist.Add(result);
                            }
                        }
                    }
                    grouped_queries.Remove(querytype);
                } else {
                    continue;
                }
            }
            string querystr = queryoptions + System.Net.WebUtility.UrlEncode(this.querystring);
            var response = client.GetAsync(querystr).Result;
            var dataobjects = response.Content.ReadAsStringAsync().Result;
            var jsonresponse = JsonConvert.DeserializeObject<dynamic>(dataobjects);
            if (jsonresponse != null) {
                foreach (var jsonstream in jsonresponse.streams) {
                    if (jsonstream != null) {
                        var name = jsonstream.channel.display_name.ToString();
                        var game = new Game(jsonstream.game.ToString());
                        var viewers = jsonstream.viewers.ToString();

                        var stream = new Stream(name, game, viewers);
                        resultlist.Add(stream);
                    }
                    else if (jsonstream.error != null) {
                        resultlist.Add(new Error(jsonstream.message.ToString()));
                    }
                }

            }
            else {
                jsonresponse = new Error("Invalid JSON received from twitch");
                return JsonConvert.SerializeObject(jsonresponse);
            }
            return resultlist;
        }
    }
}
