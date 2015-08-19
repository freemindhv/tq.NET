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
