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
                option => querylist.Add(new Search(option)));

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
