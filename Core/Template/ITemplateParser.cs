using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Core.Template
{

    public interface ITemplateParser
    {
        List<ITemplateSegment> Parse(string input);
    }

    public class SimpleParser : ITemplateParser
    {

        private static readonly Regex Pattern = new Regex(
            @"(\$\{{([a-zA-Z0-9_]*)\}})", 
            RegexOptions.CultureInvariant
        );

        public List<ITemplateSegment> Parse(string input)
        {
            var segments = new List<ITemplateSegment>();

            var match = Pattern.Match(input);
            if (!match.Success)
            {

            }

            var start = 0;
            while (match.Success)
            {
                var outer = match.Groups[1];
                var inner = match.Groups[2];

                if (outer.Index > start)
                {
                    input.Substring(start, outer.Index - start);
                }

                input.Substring(inner.Index, inner.Length);
                start = outer.Index + outer.Length;
                match = match.NextMatch();
            }

            return segments;
        }

    }

}