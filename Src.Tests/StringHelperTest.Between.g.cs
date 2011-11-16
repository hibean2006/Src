// <auto-generated>
// This file contains automatically generated unit tests.
// Do NOT modify this file manually.
// 
// When Pex is invoked again,
// it might remove or update any previously generated unit tests.
// 
// If the contents of this file becomes outdated, e.g. if it does not
// compile anymore, you may delete this file and invoke Pex again.
// </auto-generated>
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework.Generated;

namespace Src
{
    public partial class StringHelperTest
    {
[TestMethod]
[PexGeneratedBy(typeof(StringHelperTest))]
[PexRaisedException(typeof(ArgumentNullException))]
public void BetweenThrowsArgumentNullException818()
{
    string s;
    s = this.Between("", (string)null, (string)null);
}
[TestMethod]
[PexGeneratedBy(typeof(StringHelperTest))]
[PexRaisedException(typeof(ArgumentNullException))]
public void BetweenThrowsArgumentNullException807()
{
    string s;
    s = this.Between("", "", (string)null);
}
[TestMethod]
[PexGeneratedBy(typeof(StringHelperTest))]
[PexRaisedException(typeof(ArgumentNullException))]
public void BetweenThrowsArgumentNullException396()
{
    string s;
    s = this.Between("", "\0\0", (string)null);
}
[TestMethod]
[PexGeneratedBy(typeof(StringHelperTest))]
[PexRaisedException(typeof(ArgumentNullException))]
public void BetweenThrowsArgumentNullException589()
{
    string s;
    s = this.Between("\0", "\0", (string)null);
}
[TestMethod]
[PexGeneratedBy(typeof(StringHelperTest))]
[PexRaisedException(typeof(ArgumentOutOfRangeException))]
public void BetweenThrowsArgumentOutOfRangeException167()
{
    string s;
    s = this.Between("\0", "\0", "\0");
}
    }
}