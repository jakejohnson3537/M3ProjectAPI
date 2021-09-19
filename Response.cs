using System;
using System.Collections.Generic;

namespace M3webAPI
{
    public class Response
    {
        public string result { get; set; }
        public string message { get; set; }
        public List<Products> products { get; set; }
    }
}