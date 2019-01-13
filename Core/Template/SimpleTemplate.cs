using System.Collections.Generic;
using Common.Collection;

namespace Core.Template
{

    public sealed class SimpleTemplate
    {

        private ITemplateEvaluator _evaluator;
        private readonly ITemplateContext _context;

        private SimpleTemplate(ITemplateContext context)
        {
            this._context = context;
            this._evaluator = new SimpleEvaluator();
        }

        public static SimpleTemplate Of(ITemplateContext context)
        {
            return new SimpleTemplate(context);
        }

        public SimpleTemplate WithEvaluator(ITemplateEvaluator evaluator)
        {
            this._evaluator = evaluator;
            return this;
        }

        public string Generate(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            // Parse
            var segments = _context.GetParser().Parse(input);

            // Eval
            if (_evaluator == null)
            {
                _evaluator = new SimpleEvaluator();
            }

            var results = new List<string>();
            segments.ForEach(s => {
//                yield _evaluator.Eval(s, _context);
            });

            return "";
            //return Collections.JoinAsString(results);
        }



    }



}
