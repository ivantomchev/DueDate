namespace Project.Web.Infrastructure.Helpers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public static class HtmlExtentions
    {
        public static MvcHtmlString Submit(this HtmlHelper helper, object htmlAttributes = null)
        {
            var input = new TagBuilder("input");
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) as IDictionary<string, object>;
            input.MergeAttributes(attributes);
            input.Attributes.Add("type", "submit");

            return MvcHtmlString.Create(input.ToString());
        }

        public static MvcHtmlString Submit(this HtmlHelper helper, string name, object htmlAttributes = null)
        {
            var input = new TagBuilder("input");
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) as IDictionary<string, object>;
            input.MergeAttributes(attributes);
            input.Attributes.Add("type", "submit");
            input.Attributes.Add("value", name);

            return MvcHtmlString.Create(input.ToString());
        }
    }
}
