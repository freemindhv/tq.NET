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
    class ArgParser {
        public static List<Query> parse(string[] args) {
            var option_set = new OptionSet();
            var help = false;
            var querylist = new List<Query>();

            option_set.Add("?|help|h", "Prints this help message",
                option => help = option != null);

            option_set.Add("f|F|featured", "Shows the featured Streams",
                option => querylist.Add(new FeaturedStream()));

            option_set.Add("t|T|top|top-games",
                "Shows the Top Games sorted by viewers",
                option => querylist.Add(new TopGame()));

            option_set.Add("s=|search=", "Searches for a stream",
                option => querylist.Add(new SearchStream(option)));

            option_set.Add("C=|channel=", "Retrieve information about a channel",
                option => querylist.Add(new SearchChannel(option)));

            try {
                option_set.Parse(args);
            }
            catch (OptionException) {
                show_help("Error - usage is:", option_set);
            }
            return querylist;
        }

        public static void show_help(string message, OptionSet option_set) {
            Console.Error.WriteLine(message);
            option_set.WriteOptionDescriptions(Console.Error);
            Environment.Exit(-1);
        }
    }
}
