using Core.Primitive;

namespace Core.Template
{

    public interface ITemplateEvaluator
    {
        string Eval(ITemplateSegment segment, ITemplateContext context);
    }

    public class SimpleEvaluator : ITemplateEvaluator
    {

        public string Eval(ITemplateSegment segment, ITemplateContext context)
        {
            return segment.IsText 
                 ? segment.Text 
                 : StringUtil.Of(context.GetObjectIfNotExist(segment.Expression));
        }

    }

}