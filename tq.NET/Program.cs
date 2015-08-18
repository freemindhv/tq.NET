using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tq.NET {
    class Program {
        static void Main(string[] args) {

            List<Query> queries = ArgParser.parse(args);

            foreach (var query in queries) {
                var streams = query.get_streamlist();
                Console.WriteLine("");
                try {
                    foreach (var stream in streams) {
                        stream.printInfo();
                    }
                } catch (NotImplementedException){
                    Console.WriteLine("{0,35}", "Feature not implemented yet!");
                }
                Console.WriteLine("");
            }
        }
    }
}
