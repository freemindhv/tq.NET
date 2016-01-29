using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tq.NET {
    static class Bookmarks {

        //add bookmark
        public static void add(String bookmark) {
            var bookmarklist = System.IO.File.ReadLines(@"bookmarks.txt").ToList();
            if (bookmarklist.Contains(bookmark)) {
                Console.WriteLine("Bookmark already exists");
            } else {
                bookmarklist.Add(bookmark);
                try {
                    bookmarklist.Sort();
                    System.IO.File.WriteAllLines(@"bookmarks.txt", bookmarklist);
                    Console.WriteLine("Bookmark successfully saved");
                }
                catch (UnauthorizedAccessException e) {
                    Console.WriteLine(e.Message);
                }
            }
        }
        //get bookmarks
        public static List<String> get_bookmarks() {
            var bookmarklist = System.IO.File.ReadLines(@"bookmarks.txt").ToList();
            return bookmarklist;
        }
    }

}
