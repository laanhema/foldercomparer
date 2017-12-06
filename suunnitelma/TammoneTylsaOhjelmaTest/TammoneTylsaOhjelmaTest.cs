using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass()]
	public  class TestTammoneTylsaOhjelma
	{
		[TestMethod()]
		public  void testInvalidPath171()
		{
			string path = "kissa";
			Assert.AreEqual( true, TammoneTylsaOhjelma.InvalidPath(path) , "in method InvalidPath, line 173");
			path = "X:\\kissa\\koira\\kana";
			Assert.AreEqual( true, TammoneTylsaOhjelma.InvalidPath(path) , "in method InvalidPath, line 175");
			path = "C:\\Users\\Late\\Desktop";
			Assert.AreEqual( false, TammoneTylsaOhjelma.InvalidPath(path) , "in method InvalidPath, line 177");
		}
	}

