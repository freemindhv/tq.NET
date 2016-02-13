using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tq.NET {
    class TwitchAPIHandler {
        List<Query> querylist = new List<Query>();

        public void add_query(Query query) {
            this.querylist.Add(query);
        }

        public List<Query> get_queries() {

            return this.querylist;
        }

        public IEnumerable<Result> handle_queries() {

            /* implement feature that checks for multiple occurances
            of the same type and concatenate them to speed things up 
            Also the http clint should be moved here so we can reuse
            the same connection multiple times                       */
            List<Result> resultlist = new List<Result>();

            foreach (var query in this.querylist) {
                foreach (var result in query.get_streamlist()) {
                    resultlist.Add(result);
                }
            }
            return resultlist;
        }
    }
}
