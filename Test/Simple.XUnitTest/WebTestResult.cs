using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.XUnitTest
{
    public class WebTestResult<T>
    {
        public int statusCode { get; set; }
        public T data { get; set; }
        public bool succeeded { get; set; }
    }
}
