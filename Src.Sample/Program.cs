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
            string val1;
            var test = new Test();

            using (var reader = XmlReaderEx.CreateFromFile("~Src.Sample.exe.config"))
            {
                reader
                    .AddParse("/configuration/appSettings/add", () => val1 = reader.GetAttribute("value"))
                    .AddParse("/configuration/test", () =>
                                                         {
                                                             test.pp = reader.GetAttribute("pp");
                                                             test.cc = reader.ReadAttrToBoolean("cc");
                                                         })
                    .AddParse("/configuration/test/abc", () =>
                                                             {
                                                                 var abc = new abc();
                                                                 abc.Value = reader.ReadAttrToInt32("value");
                                                                 reader.Attach(abc);
                                                                 test.abcs.Add(abc);
                                                             })
                    .AddParse("/configuration/test/abc/a", () =>
                                                               {
                                                                   var abc = (abc) reader.GetAttach();
                                                                   var a = new a();
                                                                   a.a0 = reader.ReadAttrToInt32("a0");
                                                                   abc.A = a;
                                                               })
                    .AddParse("/configuration/test/abc/b", () =>
                                                               {
                                                                   var abc = (abc) reader.GetAttach();
                                                                   var b = new b();
                                                                   b.b0 = reader.ReadAttrToInt32("b0");
                                                                   abc.B = b;
                                                               })
                    .AddParse("/configuration/test/abc/c", () =>
                                                               {
                                                                   var abc = (abc) reader.GetAttach();
                                                                   var c = new c();
                                                                   c.c0 = reader.ReadAttrToInt32("c0");
                                                                   abc.C = c;
                                                               })
                    .Go();
            }


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
            public a A { get; set; }
            public b B { get; set; }
            public c C { get; set; }

        }
        class a
        {
            public int a0 { get; set; }
        }
        class b
        {
            public int b0 { get; set; }
        }
        class c
        {
            public int c0 { get; set; }
        }
    }
}
