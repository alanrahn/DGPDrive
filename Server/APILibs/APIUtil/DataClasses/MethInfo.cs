

namespace ApiUtil.DataClasses
{
    public class MethInfo
    {
        private string _fullname;

        public MethInfo()
        {

        }

        public string ApiName { get; set; }

        public string FullName
        {
            get { return _fullname; }
            set
            {
                _fullname = value;

                if (!string.IsNullOrEmpty(value))
                {
                    string[] apiparts = _fullname.Split(new char[] { '.' });
                    if (apiparts.Length >= 1) ApiName = apiparts[0];
                }
            }
        }
        public bool Authorized { get; set; }
    }
}
