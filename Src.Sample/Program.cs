using System;
using System.Collections.Generic;
using System.Text;

namespace Src.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var check = new string[] { "100", "200" };
            args = new string[] { "100" };
            P.ThrowAndLog()
                .When(args, "args").IsNull()
                .Or(args.Length, "args.Length").IsLessThan(1)
                .Or().IsLessOrEqualThan(1)
                .Or().IsGreaterThan(2)
                .Or().IsGreaterOrEqualThan(1)
                .Or(args[0], "args[0]").InArray(new string[] { "100", "200" })
                .Or().IsEqual("100")
                .Or().NotInArray(new string[] { "100", "200" });
        }
    }
}
