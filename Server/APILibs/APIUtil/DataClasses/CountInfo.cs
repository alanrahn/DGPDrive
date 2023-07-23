using System;

namespace ApiUtil.DataClasses
{
    public class CountInfo
    {
        public CountInfo()
        {

        }
        
        public long DestSrcCount { get; set; }
        public long maxDestSrcID { get; set; }
        public long SrcTotalCount { get; set; }
        public long SrcDestCount { get; set; }
    }
}
