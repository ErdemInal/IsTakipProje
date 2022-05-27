using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.TagHelpers
{
    [HtmlTargetElement("erdem")]
    public class Paragraf : TagHelper
    {
        public string GelenData { get; set; } = "Gelen Data";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var sb = new StringBuilder();
            //sb.AppendFormat("<p> <b> {0} </b> </p>", "Erdem İnal");
            sb.AppendFormat("<p> <b> {0} </b> </p>", GelenData);

            output.Content.SetHtmlContent(sb.ToString());
            //base.Process(context, output);
        }
    }
}
