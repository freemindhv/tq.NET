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
                if (args[i] == "/S") {
                    arg = args[i] + " " + args[i + 1];
                    i++;
                }
                else {
                    arg = args[i];
                }
                if (arg == "/F") {
                    querylist.Add(new FeaturedStream());
                }
                else if (arg == "/T") {
                    querylist.Add(new TopGame());
                }
            }
            return querylist;
        }
    }
}
