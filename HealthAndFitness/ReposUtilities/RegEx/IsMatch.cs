using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ReposUtilities.RegEx
{
    static class IsMatch
    {
        //// https://www.icewarp.com/support/online_help/203030104.htm 

        public static bool GetPdfTagsByType(string input, string regexExpression)
        {
            var result = false;
            // This is fixed string expression.
            // const string tagsRegexExpression = @"(w|o|x|r|c|m|v|i|d)\d{1,2}['s','i']$";     // format : v99s, c99i, d99s
            // declare an array.
            var yongleSignerPrefix = new[] { "w", "o", "x", "r", "c", "m", "v", "i", "d" };

            var regJoin = string.Join("|", yongleSignerPrefix);    // string.Join vs string.Split
            // Replace single curly braces with double curly braces:
            var tagsRegexExpression = $@"({regJoin})\d{{1,2}}['s','i']$";
            
            // If that makes your eyes hurt you could just use ordinary string concatenation instead:
            // tagsRegexExpression = "(" + regJoin + @")\d{1,2}['s','i']$";     // format : v99s

            if (Regex.IsMatch(input, tagsRegexExpression, RegexOptions.IgnoreCase))
                return true;

            return result;
        }

    }
}
