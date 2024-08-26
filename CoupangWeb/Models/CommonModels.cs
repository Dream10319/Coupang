using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoupangWeb.Auth
{
    public class CPWErrorInfo
    {
        public string code { get; set; }
        public string message { get; set; }
    }


    public class CPWCommonApiResult<T> {
        public T data { get; set; }
        public CPWErrorInfo error { get; set; }
        public string code { get; set; }
    }


    public class CPWBoolAgreement
    {
        public bool agree { get; set; }
    }

    public class CPWAdvertisingInfo
    {
        public bool call { get; set; }
        public bool email { get; set; }
        public bool text { get; set; }
    }


    public class CPWShortImageDto
    {
        public long id { get; set; }
        public string imagePath { get; set; }
        public object fileName { get; set; }
        public int exposeOrder { get; set; }
    }

    public class CPWLongImageDto
    {
        public int imageId { get; set; }
        public string imagePath { get; set; }
        public string imageFullPath { get; set; }
        public string fileName { get; set; }
        public int exposeOrder { get; set; }
    }
}
