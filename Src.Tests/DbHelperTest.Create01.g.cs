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
[ExpectedException(typeof(ArgumentNullException))]
public void Create01ThrowsArgumentNullException434()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      IDbHelper iDbHelper;
      iDbHelper = this.Create01((string)null, (string)null);
      disposables.Add((IDisposable)iDbHelper);
      disposables.Dispose();
    }
}
[TestMethod]
[PexGeneratedBy(typeof(DbHelperTest))]
[ExpectedException(typeof(ArgumentException))]
public void Create01ThrowsArgumentException976()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      IDbHelper iDbHelper;
      iDbHelper = this.Create01("", (string)null);
      disposables.Add((IDisposable)iDbHelper);
      disposables.Dispose();
    }
}
[TestMethod]
[PexGeneratedBy(typeof(DbHelperTest))]
[ExpectedException(typeof(ArgumentException))]
public void Create01ThrowsArgumentException113()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      IDbHelper iDbHelper;
      iDbHelper = this.Create01("\0", (string)null);
      disposables.Add((IDisposable)iDbHelper);
      disposables.Dispose();
    }
}
    }
}