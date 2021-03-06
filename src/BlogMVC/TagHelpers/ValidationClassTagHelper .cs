﻿using System.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace BlogMVC.TagHelpers
{
    [HtmlTargetElement("div", Attributes = ValidationForAttributeName + "," + ValidationErrorClassName)]
    public class ValidationClassTagHelper : TagHelper
    {
        private const string ValidationForAttributeName = "form-validation-for";
        private const string ValidationErrorClassName = "form-validationerror-class";

        [HtmlAttributeName(ValidationForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName(ValidationErrorClassName)]
        public string ValidationErrorClass { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            ModelStateEntry entry;
            ViewContext.ViewData.ModelState.TryGetValue(For.Name, out entry);
            if (entry == null || !entry.Errors.Any()) return;
            var tagBuilder = new TagBuilder("div");
            tagBuilder.AddCssClass(ValidationErrorClass);
            output.MergeAttributes(tagBuilder);
        }
    }
}
