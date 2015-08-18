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
}
