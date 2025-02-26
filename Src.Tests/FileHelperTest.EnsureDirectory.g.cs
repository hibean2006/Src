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
    public partial class FileHelperTest
    {
[TestMethod]
[PexGeneratedBy(typeof(FileHelperTest))]
[ExpectedException(typeof(ArgumentNullException))]
public void EnsureDirectoryThrowsArgumentNullException20()
{
    this.EnsureDirectory((string)null);
}
[TestMethod]
[PexGeneratedBy(typeof(FileHelperTest))]
public void EnsureDirectory331()
{
    this.EnsureDirectory("~");
}
[TestMethod]
[PexGeneratedBy(typeof(FileHelperTest))]
[PexRaisedException(typeof(ArgumentException))]
public void EnsureDirectoryThrowsArgumentException435()
{
    this.EnsureDirectory("\0\0");
}
[TestMethod]
[PexGeneratedBy(typeof(FileHelperTest))]
[ExpectedException(typeof(ArgumentException))]
public void EnsureDirectoryThrowsArgumentException339()
{
    this.EnsureDirectory("~\0");
}
[TestMethod]
[PexGeneratedBy(typeof(FileHelperTest))]
[ExpectedException(typeof(ArgumentException))]
public void EnsureDirectoryThrowsArgumentException153()
{
    this.EnsureDirectory("\0");
}
[TestMethod]
[PexGeneratedBy(typeof(FileHelperTest))]
[ExpectedException(typeof(ArgumentException))]
public void EnsureDirectoryThrowsArgumentException937()
{
    this.EnsureDirectory("");
}
    }
}
