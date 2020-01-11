using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SmartInItProjekat.Helper
{
        public static class HtmlHelper
        {
            public static bool HasValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
            {
                var value = htmlHelper.ValidationMessageFor(expression).ToString();

                return value.Contains("field-validation-error");
            }
        }
}