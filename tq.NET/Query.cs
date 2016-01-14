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
    public class Query {
        protected string queryoptions;
        public static HttpClient client = new HttpClient();

        public Query() {
            client.BaseAddress = new Uri("https://api.twitch.tv/kraken/");
            client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.twitchtv.v3+json"));
            client.DefaultRequestHeaders.Add("Client-ID", "tq.net");
        }

        protected Newtonsoft.Json.Linq.JObject get_json() {
            var response = client.GetAsync(this.queryoptions).Result;
            var dataobjects = response.Content.ReadAsStringAsync().Result; 

            return JsonConvert.DeserializeObject<dynamic>(dataobjects);
            
        }
        public virtual IEnumerable<Result> get_streamlist() {
            throw new NotImplementedException();
        }
    }


    public class FeaturedStream : Query {
        public FeaturedStream(int limit) {
            queryoptions = "streams/featured?limit=" + limit;
        }
        public override IEnumerable<Result> get_streamlist() {
            dynamic json = get_json();
            var resultlist = new List<Result>();

            foreach (var entry in json.featured) {
                var channel = entry.stream.channel.display_name.ToString();
                var viewers = entry.stream.viewers.ToString();
                var game = new Game(entry.stream.game.ToString());
                var stream = new Stream(channel, game: game, strviewers: viewers);
                resultlist.Add(stream);
            }

            return resultlist;
        }
    }


    public class TopGame : Query {
        public TopGame(int limit) {
            queryoptions = "games/top?limit=" + limit;
        }
        public override IEnumerable<Result> get_streamlist() {
            dynamic json = get_json();
            var resultlist = new List<Result>();

            foreach (var entry in json.top) {
                var game = new Game(entry.game.name.ToString(), entry.viewers.ToString());
                resultlist.Add(game);
            }
            return resultlist;
        }
    }


    public class SearchStream : Query {
        public SearchStream(string searchstring, int limit) {
            queryoptions = "search/streams?q=" + searchstring + "&limit=" + limit;
        }
        public override IEnumerable<Result> get_streamlist() {
            dynamic json = get_json();
            var resultlist = new List<Result>();

            foreach (var entry in json.streams) {
                var channel = entry.channel.display_name.ToString();
                var viewers = entry.viewers.ToString();
                var game = new Game(entry.game.ToString());
                var stream = new Stream(channel, game: game, strviewers: viewers);
                resultlist.Add(stream);
            }
            return resultlist;
        }
    }


    public class ChannelInfo : Query {
        public ChannelInfo(string searchstring, int limit) {
            queryoptions = "channels/" + searchstring;
        }

        public override IEnumerable<Result> get_streamlist() {
            dynamic json = get_json();
            var resultlist = new List<Result>();

            if (json.error != null) {
                var error = new Error(json.message.ToString());
                resultlist.Add(error);
                return resultlist;
            }

            var channame = json.display_name.ToString();
            Game game = null;

            if (json.game != null) {
                game = new Game(json.game.ToString());
            }
            else {
                game = new Game("n/a");
            }
            var url = json.url.ToString();
            var channel = new Channel(channame, game, url);
            resultlist.Add(channel);

            return resultlist;
        }
    }


    public class StreamInfo : Query {
        public StreamInfo(string streamname, int limit) {
            queryoptions = "streams/" + streamname;
        }

        public override IEnumerable<Result> get_streamlist() {
            dynamic json = get_json();
            var resultlist = new List<Result>();

            if (json.stream != null) {
                var name = json.stream.channel.display_name.ToString();
                var game = new Game(json.stream.game.ToString());
                var viewers = json.stream.viewers.ToString();

                var stream = new Stream(name, game, viewers);
                resultlist.Add(stream);
            }
            else if (json.error != null) {
                resultlist.Add(new Error(json.message.ToString()));
            }
            else {
                resultlist.Add(new Error("Stream is offline"));
            }
            
            return resultlist;
        }
    }
}
