using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tq.NET {
    class Bookmarks {
        private List<Bookmark> bookmarklist = new List<Bookmark>();
        public Bookmarks() {
            
        }

        //add bookmark
        public void add(Bookmark bookmark) {
            bookmarklist.Add(bookmark);
        }
        //get bookmarks
        public List<Bookmark> get() {
            return bookmarklist;
        }
        //write to disk
        public bool save() {

            return true;
        }

        //read from disk
        public List<Bookmark> load() {

            return bookmarklist;
        }

    }

}
