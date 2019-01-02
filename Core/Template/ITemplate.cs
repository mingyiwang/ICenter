using System;

namespace Core.Template
{

    public interface ITemplate : IDisposable
    {
        string Generate(string input);
        string Generate(string input, ITemplateContext context);
    }

}
