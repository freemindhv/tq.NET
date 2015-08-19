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

    abstract class Result {
        public virtual void printInfo() {
            throw new NotImplementedException();
        }
    }


    class Stream : Result {
        public string name { get; set; }
        public string game { get; set; }
        public string URL { get; set; }
        public string viewers { get; set; }


        public Stream(string strtitle, Game game = null, string strviewers = "n/a") {
            this.name = strtitle;
            this.viewers = strviewers;
            if (game != null) {
                this.game = game.name;
            }
            else {
                this.game = "n/a";
            }
        }
        public override void printInfo() {
            Console.WriteLine("{0,35}    Viewers: {1,6}    Game: {2}", this.name, this.viewers, this.game);
        }
    }


    class Game : Result {
        public string name { get; set; }
        public string viewers { get; set; }

        public Game(string strgame, string strviewers = "0") {
            this.name = strgame;
            this.viewers = strviewers;
        }
        public override void printInfo() {
            Console.WriteLine("{0,35}    Viewers: {1,6}", this.name, this.viewers);
        }
    }


    class Channel : Result {
        public string name { get; set; }
        public string game { get; set; }
        public string url { get; set; }

        public Channel(string chname, Game game, string chURL) {
            this.name = chname;
            this.game = game.name;
            this.url = chURL;
        }
        public override void printInfo() {
            Console.WriteLine("{0,35}    Game: {1}    URL: {2}", this.name, this.game, this.url);
        }
    }
}
