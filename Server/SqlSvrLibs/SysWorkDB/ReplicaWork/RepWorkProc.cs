using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysWorkDB.GeneralWork
{
    /// <summary>
    /// Run on the SERVER
    /// </summary>
    public class RepWorkProc
    {
        public RepWorkProc()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public string QueueCheck()
        {
            string results = "";

            // query for queue records that have taken too long to complete


            // for each record found


                // log the problem with the record


                // increment reset count 
                // (will let queue record run once more, and if it fails a second time, it will be disabled)



            return results;
        }
    }
}
