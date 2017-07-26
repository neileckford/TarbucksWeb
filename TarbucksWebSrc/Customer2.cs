using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TarbucksWeb
{
    public class Customer2
    {
        private string custID;
        private string str;

        public Customer2(string id, string s)
        {
            custID = id;
            str = s;
        }

        public string getName()
        {
            return str;
        }
        public override string ToString() { return str; }
    }
}