using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HeavenProperty.Models
{
    [HtmlTargetElement(Attributes = "condition")]
    [HtmlTargetElement("visible")]
    public class VisibleTagHelper : TagHelper
    {
        [HtmlAttributeName("condition")]
        public bool Condition { get; set; }

        public VisibleTagHelper()
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (output.TagName == "visible")
            {
                output.TagName = "";
            }

            if (!this.Condition)
            {
                output.TagName = "";
                output.Content.SetHtmlContent("");
            }
            else
            {
                base.Process(context, output);
            }
        }
    }
}
