using Mono.Options;
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

namespace tq.NET {
    class Program {
        static void Main(string[] args) {

            var option_set = new OptionSet();
            var queries = new List<Query>();
            var test = new List<string>();
            string featured = null;
            string topgame = null;
            string search = null;
            var channel = false;
            var stream = false;

            option_set.Add("?|help|h", "Prints this help message",
                option => show_help("Usage:", option_set));

            option_set.Add("f|F|featured", "Shows the featured Streams",
                option => test.Add(featured = "featured"));

            option_set.Add("t|T|top|top-games",
                "Shows the Top Games sorted by viewers",
                option => test.Add(topgame = "topgame"));

            option_set.Add("s=|search=", "Searches for a stream",
                option => test.Add(search = "search"));

            option_set.Add("C=|channel=", "Retrieve information about a channel",
                option => channel = option != null);

            option_set.Add("S=|stream=", "Retrieve information about a stream",
                option => stream = option != null);

            try {
                option_set.Parse(args);
            }
            catch (OptionException) {
                show_help("Error - usage is:", option_set);
            }

            foreach (var arg in test) {
                if (arg == "featured")
                    queries.Add(new FeaturedStream());
                else if (arg == "topgame")
                    queries.Add(new TopGame());
                else if (arg == "search")
                    queries.Add(new SearchStream("starcraft"));

            }

            
            foreach (var query in queries) {
                var streams = query.get_streamlist();
                Console.WriteLine("");
                try {
                    foreach (var s in streams) {
                        s.printInfo();
                    }
                } catch (NotImplementedException){
                    Console.WriteLine("{0,35}", "Feature not implemented yet!");
                }
                Console.WriteLine("");
            }
        }

        public static void show_help(string message, OptionSet option_set) {
            Console.Error.WriteLine(message);
            option_set.WriteOptionDescriptions(Console.Error);
            Environment.Exit(-1);
        }
    }
}
