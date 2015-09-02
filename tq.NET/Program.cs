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
            List<Query> queries = new List<Query>();

            var parser = new ArgParser();
            parser.add_option("featured||f||F", ArgParser.option_type.FLAG);
            parser.add_option("topgames||t||T", ArgParser.option_type.FLAG);
            parser.add_option("debug||D", ArgParser.option_type.FLAG);
            parser.add_option("search||s", ArgParser.option_type.OPTION);
            parser.add_option("print||p||P", ArgParser.option_type.OPTION);
            parser.add_option("channel||C", ArgParser.option_type.OPTION);
            parser.add_option("stream||S", ArgParser.option_type.OPTION);
            parser.add_option("limit||l", ArgParser.option_type.OPTION);
            var options = parser.parse(args);

            foreach (var opt in options) {
                if (opt.Name == "featured" || opt.Name == "f" || opt.Name == "F") {
                    queries.Add(new FeaturedStream());
                } else if (opt.Name == "topgames" || opt.Name == "t" || opt.Name == "T") {
                    queries.Add(new TopGame());
                }
                else if (opt.Name == "channel" || opt.Name == "C") {
                    foreach (var channel in opt.get_options()) {
                        queries.Add(new ChannelInfo(channel));
                    }
                } else if (opt.Name == "stream" || opt.Name == "S") {
                    foreach (var stream in opt.get_options()) {
                        queries.Add(new ChannelInfo(stream));
                    }
                } else if (opt.Name == "search" || opt.Name == "s") {
                    foreach (var searchstring in opt.get_options()) {
                        queries.Add(new SearchStream(searchstring));
                    }
                }
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
    }
}
