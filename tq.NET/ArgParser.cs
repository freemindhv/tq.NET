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
    public static class ArgParser {
        public class Option {
            public bool has_optionvalue;
            public string Name { get;  private set; }
            private List<string> optionvalues = new List<string>();


            public Option(bool has_value, string name) {
                this.has_optionvalue = has_value;
                this.Name = name;
            }

            public void add_option(string value) {
                if (!this.has_optionvalue) {
                    throw new Exception("Option cannot have a option value");
                }
                else {
                    this.optionvalues.Add(value);
                }
            }

            public List<string> get_options() {
                return this.optionvalues;
            }

        }
        private static Dictionary<string, Option> optionset = new Dictionary<string, Option>();
        private static List<Option> optionlist = new List<Option>();
        public enum option_type {
            FLAG,
            OPTION,
            VALUE,
            INVALID,
        };


        public static void add_option(string stroptions, option_type type) {
            try {
                var delimiter = new char[] { '|', '|' };
                string[] options = stroptions.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

                foreach (var opt in options) {
                    if (type == option_type.FLAG) {
                        optionset.Add(opt, new Option(false, opt));
                    }
                    else if (type == option_type.OPTION) {
                        optionset.Add(opt, new Option(true, opt));
                    }
                }
            }
            catch (Exception e) {
                throw (e);
            }
        }


        public static List<Option> parse(string[] args) {

            for (int i = 0; i < args.Length; i++) {
                if (args[i].Contains("-") || args[i].Contains("/")) {

                    var optiontype = check_option(args[i]);
                    var option = trim_option(args[i]);

                    if (optiontype == option_type.FLAG) {
                        optionlist.Add(optionset[option]);

                    }
                    else if (optiontype == option_type.OPTION) {
                        var opt = optionset[option];
                        for (int x = i + 1; x < args.Length; x++) {
                            if (check_option(args[x]) == option_type.VALUE) {
                                opt.add_option(args[x]);
                            }
                            else {
                                break;
                            }
                        }
                        optionlist.Add(opt);

                    }
                    else if (optiontype == option_type.INVALID) {
                        Console.WriteLine("Invalid Option --- show usage");
                        Environment.Exit(-1);
                    }

                }
            }
            return optionlist;
        }


        private static option_type check_option(string arg) {

            var option = trim_option(arg);


            try {
                if (optionset[option].has_optionvalue) {
                    return option_type.OPTION;
                }
                else {
                    return option_type.FLAG;
                }
            }
            catch {
                if (arg.Contains("-") || arg.Contains("/")) {
                    return option_type.INVALID;
                }
                else {
                    return option_type.VALUE;
                }
            }
        }


        private static string trim_option(string arg) {
            var option = arg.Trim();
            option = option.Replace("--", "");
            option = option.Replace("-", "");
            option = option.Replace("/", "");

            return option;
        }
    }
}