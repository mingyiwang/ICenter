using System;
using System.Collections.Generic;
using Core.Collection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebAPI
{

    public sealed class Middlewares
    {
        
        private readonly IApplicationBuilder _builder;
        private readonly List<Action<HttpContext>> _middlewares;

        private Middlewares(IApplicationBuilder builder)
        {
            this._builder = builder;
            this._middlewares = new List<Action<HttpContext>>();
        }

        public static Middlewares Of(IApplicationBuilder builder)
        {
            return new Middlewares(builder);
        }

        
        public Middlewares First(Action<HttpContext> middleware)
        {
            Collections.InsertFirst(_middlewares, middleware);
            return this;
        }

        public Middlewares Last(Action<HttpContext> middleware)
        {
            Collections.InsertLast(_middlewares, middleware);
            return this;
        }

        public Middlewares With(Action<HttpContext> middleware)
        {
            _middlewares.Add(middleware);
            _builder.Use((context, next) =>
            {
                middleware(context);
                return next();
            });
            return this;
        }

        public void Build()
        {
            _middlewares.ForEach(action =>
            {
                _builder.Use((context, next) =>
                {
                    action(context);
                    return next();
                });
            });
        }

       
    }
}