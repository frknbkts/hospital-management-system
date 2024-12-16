using prolabson.Models;
using prolabson.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prolabson.Test
{
    class TestClass
    {
        public static void Main(string[] args)
        {
            // Display the number of command line arguments.
           DoktorData doktor = new DoktorData();
            doktor.Read(1);

        }
    }

}