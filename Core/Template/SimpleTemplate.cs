namespace Core.Template
{

    public class SimpleTemplate
    {
        private const string PatternExpression = @"(\$\{([a-zA-Z0-9_]*)\})";

        public string Generate(string input)
        {
            return Generate(input, new DefaultTemplateContext());
        }

        public string Generate(string input, ITemplateContext context)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

    }

}
