namespace Core.Template
{
    public interface ITemplateSegment
    {

        bool IsExpression { get; set; }
        string Expression { get; set; }

        bool IsText { get; set; }
        string Text { get; set; }

    }

    public class SimpleSegment : ITemplateSegment
    {
        public bool IsExpression { get; set; }
        public string Expression { get; set; }
        public bool IsText { get; set; }
        public string Text { get; set; }
    }

}