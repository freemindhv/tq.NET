using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tq.NET {
    class ArgParser {
        public static List<Query> parse(string[] args) {
            string arg;
            var querylist = new List<Query>();

            for (int i = 0; i < args.Length; i++) {
                if (args[i] == "/s") {
                    arg = args[i] + " " + args[i + 1];
                    i++;
                }
                else {
                    arg = args[i];
                }
                if (arg == "/f") {
                    querylist.Add(new FeaturedStream());
                }
                else if (arg == "/t") {
                    querylist.Add(new TopGame());
                }
                else if (arg == "/D") {
                    foreach (var flag in args) {
                        Console.WriteLine(flag);
                    }
                }
                else if (arg.StartsWith("/s")) {
                    var flags = arg.Split(' ');
                    querylist.Add(new Search(flags[1]));
                }
            }
            return querylist;
        }
    }
}
