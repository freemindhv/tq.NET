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
using Mono.Options;

namespace tq.NET {

    public class ArgParser {
        OptionSet options = new OptionSet();
        List<Query> queries = new List<Query>();

        public ArgParser() {
            var limit = Properties.Settings.Default.limit;

            options.Add("?|help|h", "Print this help",
                option => show_help("Usage is:", options));

            options.Add("featured|f", "Query featured streams.",
                option => queries.Add(new FeaturedStream(limit)));

            options.Add("top|t", "Get a list of the currently top played games.",
                option => queries.Add(new TopGame(limit)));

            options.Add("search-streams|s=", "Search for streams.",
                option => queries.Add(new SearchStream(option, limit)));

            options.Add("search-game|g=", "Search for streams categories under GAME.",
               option => queries.Add(new SearchGameStream(option, limit)));

            options.Add("channels|C=", "Retrieve information about a channel.",
                option => queries.Add(new ChannelInfo(option, limit)));

            options.Add("streams|S=", "Retrieve information about a steam. Stream must be live.",
                option => queries.Add(new StreamInfo(option, limit)));

            options.Add("add-bookmark|a=", "Add a new bookmark.",
                option => Bookmarks.add(option));

            options.Add("check-bookmarks|b", "Check which bookmarks are streaming.",
                option => Bookmarks.get_bookmarks().ForEach(item => queries.Add(new StreamInfo(item, limit))));

            options.Add("open|o=", "Open a stream with livestreamer.",
                option => StreamOpener.open(option));

        }

        public List<Query> parse(IEnumerable<string> args) {
            try {
                options.Parse(args);
            }
            catch {
                show_help("Error - usage is:", options);
            }
            return queries;
            
        }

        private void show_help(string message, OptionSet options) {
            Console.Error.WriteLine(message);
            options.WriteOptionDescriptions(Console.Error);
            Environment.Exit(-1);
        }
    }
}