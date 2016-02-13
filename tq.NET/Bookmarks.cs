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
    static class Bookmarks {

        //add bookmark
        public static void add(String bookmark) {
            List<string> bookmarklist = new List<string>();
            try {
                bookmarklist = System.IO.File.ReadLines(@"bookmarks.txt").ToList();
            } catch (System.IO.FileNotFoundException ) {
                //no bookmark file found, this is okay, go on
            }
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
            List<string> bookmarklist = new List<string>();
            try {
                bookmarklist = System.IO.File.ReadLines(@"bookmarks.txt").ToList();
            }
            catch (System.IO.FileNotFoundException) {
                //no bookmark file found, this is okay, go on
            }
            return bookmarklist;
        }
    }

}
