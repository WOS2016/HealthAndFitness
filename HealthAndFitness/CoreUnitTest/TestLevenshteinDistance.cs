using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using ReposUtilities.Algorithms;

namespace CoreUnitTest
{
    [TestClass]
    public class TestLevenshteinDistance
    {
        [TestMethod]
        public void TestDamerauLevenshteinDistance()
        {
            var extionsionname = "pDf";
            var regex = new Regex(@".*\.pdf?$");

            var inputFileName = new string[] { "abc", "abc.PdF.pdf.pdf", "abc.pddf", ".pdf", "pdf", "abc.pd", "abcpdf", "abc.pdf", "abc.PDF", "abc.PdF", "abc.pd", "abc.pf", "abc.df" };
            foreach (var item in inputFileName)
            {
                var result = LevenshteinDistance.DamerauLevenshteinDistance(item, extionsionname);
                //StringAssert.Matches(result, regex);
                //Assert.IsTrue(myCollection.Any(a => a > min));
                Assert.IsTrue(result < 200);
            }
        }
    }
}
