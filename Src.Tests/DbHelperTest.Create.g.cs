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
using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Src
{
    public partial class DbHelperTest
    {
[TestMethod]
[PexGeneratedBy(typeof(DbHelperTest))]
[Ignore]
[PexDescription("the test state was: path bounds exceeded")]
public void Create02()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      IDbHelper iDbHelper;
      iDbHelper = this.Create();
      disposables.Add((IDisposable)iDbHelper);
      disposables.Dispose();
    }
}
    }
}