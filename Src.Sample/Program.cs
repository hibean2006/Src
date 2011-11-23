using System;
using System.Collections.Generic;
using System.Text;

namespace Src.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            //var check = new string[] { "100", "200" };
            //args = new string[] { "100" };
            //P.ThrowAndLog()
            //    .When(args, "args").IsNull()
            //    .Or(args.Length, "args.Length").IsLessThan(1)
            //    .Or().IsLessOrEqualThan(1)
            //    .Or().IsGreaterThan(2)
            //    .Or().IsGreaterOrEqualThan(1)
            //    .Or(args[0], "args[0]").InArray(new string[] { "100", "200" })
            //    .Or().IsEqual("100")
            //    .Or().NotInArray(new string[] { "100", "200" });
            XmlParser parser = new XmlParser("~/Src.Sample.exe.config");
            string val1;
            var test = new Test();
            parser
                .AddParse("/configuration/appSettings/add", r => val1 = r.GetAttribute("value"))
                .AddParse("/configuration/test", r =>
                                                    {
                                                        test.pp = r.GetAttribute("pp");
                                                        test.cc = r.ReadAttrToBoolean("cc");
                                                    })
                .AddParse("/configuration/test/abc", r => test.abcs.Add(new abc()
                                                                           {
                                                                               Value = r.ReadAttrToInt32("value")
                                                                           }))
                .Parse();

        }

        class Test
        {
            public Test()
            {
                abcs = new List<abc>();
            }
            public string pp { get; set; }
            public bool cc { get; set; }
            public IList<abc> abcs { get; private set; }
        }
        class abc
        {
            public int Value { get; set; }
        }
    }
}
