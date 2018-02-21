using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static System.String;

namespace ReposUtilities.Algorithms
{
    public static class LevenshteinDistance
    {
        ///// <summary>
        ///// Static classes cannot have instance constructors
        ///// </summary>
        //public LevenshteinDistance()
        //{
        //}

        /// <summary>
        /// https://www.dotnetperls.com/levenshtein
        /// Compute the distance between two strings.
        /// </summary>
        public static int LevenshteinDistanceCompute(string s, string t)
        {
            var n = s.Length;
            var m = t.Length;
            var d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (var i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (var j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (var i = 1; i <= n; i++)
            {
                //Step 4
                for (var j = 1; j <= m; j++)
                {
                    // Step 5
                    var cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }

        //  http://mihkeltt.blogspot.ca/2009/04/dameraulevenshtein-distance.html
        public static int DamerauLevenshteinDistance(string string1, string string2)
        {
            if (IsNullOrEmpty(string1))
            {
                return !IsNullOrEmpty(string2) ? string2.Length : 0;
            }

            if (IsNullOrEmpty(string2))
            {
                return !IsNullOrEmpty(string1) ? string1.Length : 0;
            }

            var length1 = string1.Length;
            var length2 = string2.Length;

            var d = new int[length1 + 1, length2 + 1];

            for (var i = 0; i <= d.GetUpperBound(0); i++)
                d[i, 0] = i;

            for (var i = 0; i <= d.GetUpperBound(1); i++)
                d[0, i] = i;

            for (var i = 1; i <= d.GetUpperBound(0); i++)
            {
                for (var j = 1; j <= d.GetUpperBound(1); j++)
                {
                    var cost = string1[i - 1] == string2[j - 1] ? 0 : 1;

                    var del = d[i - 1, j] + 1;
                    var ins = d[i, j - 1] + 1;
                    var sub = d[i - 1, j - 1] + cost;

                    d[i, j] = Math.Min(del, Math.Min(ins, sub));

                    if (i > 1 && j > 1 && string1[i - 1] == string2[j - 2] && string1[i - 2] == string2[j - 1])
                        d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost);
                }
            }

            return d[d.GetUpperBound(0), d.GetUpperBound(1)];
        }

        private static int CountWords(string s)
        {
            MatchCollection collection = Regex.Matches(s, @"[\S]+");
            return collection.Count;
        }

        public static bool IsSimilarTo(this string string1, string string2)
        {
            string1 = string1.Replace("-", " ").Replace("_", " ").ToLower();
            string2 = string2.Replace("-", " ").Replace("_", " ").ToLower();

            if (string1.Equals(string2)) { return true; }

            //float distance = DamerauLevenshteinDistance(string1, string2) / ((CountWords(string1) + CountWords(string2)) / 2);
            float distance = DamerauLevenshteinDistance(string1, string2) / ((CountWords(string1) + CountWords(string2)) / 2);

            return distance <= 1.1 ? true : false;
        }

        public static string FormatFileExtension(string filePathName, string extensionName)
        {
            var rtnFilePathName = "";
            try
            {
                var idxDot = filePathName.ToLower().LastIndexOf(".", StringComparison.Ordinal);
                if (idxDot > 0)    // has .
                {
                    var idxExt = filePathName.ToLower().LastIndexOf("." + extensionName.ToLower(), StringComparison.Ordinal);
                    if (idxExt > 0)
                        rtnFilePathName = filePathName.Remove(idxDot) + "." + extensionName.ToLower();    // *.pdf
                    else
                    {
                        var arrFilename = filePathName.Split('.');
                        if (arrFilename.Length > 1)
                        {
                            if (DamerauLevenshteinDistance(arrFilename[arrFilename.Length - 1], "pdf") < 3)
                                rtnFilePathName = filePathName.Remove(filePathName.IndexOf(arrFilename[arrFilename.Length - 1], StringComparison.Ordinal) - 1) + "." + extensionName.ToLower();     // pdf
                        }
                    }
                }
                else if (idxDot == 0) // first is . e.g. .pdf .abc.
                {
                    rtnFilePathName = filePathName.Remove(0, 1) + "." + extensionName.ToLower();
                }
                else // not .
                {
                    var idxPdf = filePathName.ToLower().LastIndexOf(extensionName.ToLower(), StringComparison.Ordinal);  //  pdf
                    if (idxPdf > 0)
                        rtnFilePathName = filePathName.Remove(idxPdf) + "." + extensionName.ToLower();
                    else if (idxPdf == 0)
                        rtnFilePathName = filePathName + "." + extensionName.ToLower();
                    else
                    {
                        rtnFilePathName = filePathName + "." + extensionName.ToLower();
                    }
                }

            }
            catch (Exception)
            {
                rtnFilePathName = "";
            }

            return rtnFilePathName;
        }



        ///// <summary>
        ///// https://www.dotnetperls.com/levenshtein
        ///// Compute the distance between two strings.
        ///// </summary>
        //public static int LevenshteinDistanceCompute(string s, string t)
        //{
        //    int n = s.Length;
        //    int m = t.Length;
        //    int[,] d = new int[n + 1, m + 1];

        //    // Step 1
        //    if (n == 0)
        //    {
        //        return m;
        //    }

        //    if (m == 0)
        //    {
        //        return n;
        //    }

        //    // Step 2
        //    for (int i = 0; i <= n; d[i, 0] = i++)
        //    {
        //    }

        //    for (int j = 0; j <= m; d[0, j] = j++)
        //    {
        //    }

        //    // Step 3
        //    for (int i = 1; i <= n; i++)
        //    {
        //        //Step 4
        //        for (int j = 1; j <= m; j++)
        //        {
        //            // Step 5
        //            int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

        //            // Step 6
        //            d[i, j] = Math.Min(
        //                Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
        //                d[i - 1, j - 1] + cost);
        //        }
        //    }
        //    // Step 7
        //    return d[n, m];
        //}

        ////  http://mihkeltt.blogspot.ca/2009/04/dameraulevenshtein-distance.html
        //public static int DamerauLevenshteinDistance(string string1, string string2)
        //{
        //    if (String.IsNullOrEmpty(string1))
        //    {
        //        if (!String.IsNullOrEmpty(string2))
        //            return string2.Length;

        //        return 0;
        //    }

        //    if (String.IsNullOrEmpty(string2))
        //    {
        //        if (!String.IsNullOrEmpty(string1))
        //            return string1.Length;

        //        return 0;
        //    }

        //    int length1 = string1.Length;
        //    int length2 = string2.Length;

        //    int[,] d = new int[length1 + 1, length2 + 1];

        //    int cost, del, ins, sub;

        //    for (int i = 0; i <= d.GetUpperBound(0); i++)
        //        d[i, 0] = i;

        //    for (int i = 0; i <= d.GetUpperBound(1); i++)
        //        d[0, i] = i;

        //    for (int i = 1; i <= d.GetUpperBound(0); i++)
        //    {
        //        for (int j = 1; j <= d.GetUpperBound(1); j++)
        //        {
        //            if (string1[i - 1] == string2[j - 1])
        //                cost = 0;
        //            else
        //                cost = 1;

        //            del = d[i - 1, j] + 1;
        //            ins = d[i, j - 1] + 1;
        //            sub = d[i - 1, j - 1] + cost;

        //            d[i, j] = Math.Min(del, Math.Min(ins, sub));

        //            if (i > 1 && j > 1 && string1[i - 1] == string2[j - 2] && string1[i - 2] == string2[j - 1])
        //                d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost);
        //        }
        //    }

        //    return d[d.GetUpperBound(0), d.GetUpperBound(1)];
        //}


        //public static string FormatFileExtension(string filePathName, string extensionName)
        //{
        //    string rtnFilePathName = "";
        //    try
        //    {
        //        int idxDot = filePathName.ToLower().LastIndexOf(".", StringComparison.Ordinal);
        //        if (idxDot > 0)    // has .
        //        {
        //            int idxExt = filePathName.ToLower().LastIndexOf("." + extensionName.ToLower(), StringComparison.Ordinal);
        //            if (idxExt > 0)
        //                rtnFilePathName = filePathName.Remove(idxDot) + "." + extensionName.ToLower();    // *.pdf
        //            else
        //            {
        //                string[] arrFilename = filePathName.Split('.');
        //                if (arrFilename.Length > 1)
        //                {
        //                    if (DamerauLevenshteinDistance(arrFilename[arrFilename.Length - 1], "pdf") < 3)
        //                        rtnFilePathName = filePathName.Remove(filePathName.IndexOf(arrFilename[arrFilename.Length - 1], StringComparison.Ordinal) - 1) + "." + extensionName.ToLower();     // pdf
        //                }
        //            }
        //        }
        //        else if (idxDot == 0) // first is . e.g. .pdf .abc.
        //        {
        //            rtnFilePathName = filePathName.Remove(0, 1) + "." + extensionName.ToLower();
        //        }
        //        else // not .
        //        {
        //            int idxPdf = filePathName.ToLower().LastIndexOf(extensionName.ToLower(), StringComparison.Ordinal);  //  pdf
        //            if (idxPdf > 0)
        //                rtnFilePathName = filePathName.Remove(idxPdf) + "." + extensionName.ToLower();
        //            else if (idxPdf == 0)
        //                rtnFilePathName = filePathName + "." + extensionName.ToLower();
        //            else
        //            {
        //                rtnFilePathName = filePathName + "." + extensionName.ToLower();
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        rtnFilePathName = "";
        //    }

        //    return rtnFilePathName;
        //}
    }
}
