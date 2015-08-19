/* 
tq.NET - simple commandline twitch client
Copyright (C) 2015  Dennis Greiner

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

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
    class Query {
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
            String dataobjects = null;

            if (response.IsSuccessStatusCode) {
                dataobjects = response.Content.ReadAsStringAsync().Result;
            }
            return JsonConvert.DeserializeObject<dynamic>(dataobjects);
        }
        public virtual IEnumerable<Result> get_streamlist() {
            throw new NotImplementedException();
        }
    }


    class FeaturedStream : Query {
        public FeaturedStream() {
            this.queryoptions = "streams/featured";
        }
        public override IEnumerable<Result> get_streamlist() {
            dynamic json = this.get_json();
            var streamlist = new List<Result>();

            foreach (var entry in json.featured) {
                var channel = entry.stream.channel.display_name.ToString();
                var viewers = entry.stream.viewers.ToString();
                var game = new Game(entry.stream.game.ToString());
                var stream = new Stream(channel, game: game, strviewers: viewers);
                streamlist.Add(stream);
            }

            return streamlist;
        }
    }


    class TopGame : Query {
        public TopGame() {
            this.queryoptions = "games/top";
        }
        public override IEnumerable<Result> get_streamlist() {
            dynamic json = this.get_json();
            var streamlist = new List<Result>();

            foreach (var entry in json.top) {
                var game = new Game(entry.game.name.ToString(), entry.viewers.ToString());
                streamlist.Add(game);
            }
            return streamlist;
        }
    }


    class Search : Query {
        public Search(string searchstring) {
            this.queryoptions = "search/streams?q=" + searchstring;
        }
        public override IEnumerable<Result> get_streamlist() {
            dynamic json = this.get_json();
            var streamlist = new List<Result>();

            foreach (var entry in json.streams) {
                var channel = entry.channel.display_name.ToString();
                var viewers = entry.viewers.ToString();
                var game = new Game(entry.game.ToString());
                var stream = new Stream(channel, game: game, strviewers: viewers);
                streamlist.Add(stream);
            }
            return streamlist;
        }
    }


    class Streams : Query {
        public Streams(List<string>options) {
            this.queryoptions = "streams?";
            foreach (var option in options) {
                this.queryoptions += option;
            }
        }
    }
}
