#pragma checksum "C:\Users\HP O M E N\Documents\C#\Atriya\MVC_Final_Project\Code\LampShade\ServiceHost\Pages\Components\ProductCategoryViewComponents\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9d2f76aed031e69be64d9374b558a9be42d8a148"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(ServiceHost.Pages.Components.ProductCategoryViewComponents.Pages_Components_ProductCategoryViewComponents_Default), @"mvc.1.0.view", @"/Pages/Components/ProductCategoryViewComponents/Default.cshtml")]
namespace ServiceHost.Pages.Components.ProductCategoryViewComponents
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\HP O M E N\Documents\C#\Atriya\MVC_Final_Project\Code\LampShade\ServiceHost\Pages\_ViewImports.cshtml"
using ServiceHost;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9d2f76aed031e69be64d9374b558a9be42d8a148", @"/Pages/Components/ProductCategoryViewComponents/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d027006424b9e12b1709732f146fce9f1d78e6a1", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Components_ProductCategoryViewComponents_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<_01_lampshadeQuery.Contracts.ProductCategory.ProductCategoryQueryModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-fluid"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "/ProductCategory", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<!--====================  category area ====================-->
<div class=""category-area section-space"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-lg-12"">
                <!--=======  category wrapper  =======-->
                <div class=""category-wrapper"">
                    <div class=""row row-10 masonry-category-layout"">
");
#nullable restore
#line 11 "C:\Users\HP O M E N\Documents\C#\Atriya\MVC_Final_Project\Code\LampShade\ServiceHost\Pages\Components\ProductCategoryViewComponents\Default.cshtml"
                         foreach (var productCategory in Model)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            <div class=""col-lg-4 col-sm-6 grid-item"">
                                <!--=======  single category item  =======-->
                                <div class=""single-category-item"">
                                    <div class=""single-category-item__image"">
                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9d2f76aed031e69be64d9374b558a9be42d8a1485193", async() => {
                WriteLiteral("\r\n                                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "9d2f76aed031e69be64d9374b558a9be42d8a1485492", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                AddHtmlAttributeValue("", 1018, "~/ProductPictures/", 1018, 18, true);
#nullable restore
#line 18 "C:\Users\HP O M E N\Documents\C#\Atriya\MVC_Final_Project\Code\LampShade\ServiceHost\Pages\Components\ProductCategoryViewComponents\Default.cshtml"
AddHtmlAttributeValue("", 1036, productCategory.Picture, 1036, 24, false);

#line default
#line hidden
#nullable disable
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "alt", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 18 "C:\Users\HP O M E N\Documents\C#\Atriya\MVC_Final_Project\Code\LampShade\ServiceHost\Pages\Components\ProductCategoryViewComponents\Default.cshtml"
AddHtmlAttributeValue("", 1085, productCategory.PictureAlt, 1085, 27, false);

#line default
#line hidden
#nullable disable
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "title", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 18 "C:\Users\HP O M E N\Documents\C#\Atriya\MVC_Final_Project\Code\LampShade\ServiceHost\Pages\Components\ProductCategoryViewComponents\Default.cshtml"
AddHtmlAttributeValue("", 1121, productCategory.PictureTitle, 1121, 29, false);

#line default
#line hidden
#nullable disable
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 17 "C:\Users\HP O M E N\Documents\C#\Atriya\MVC_Final_Project\Code\LampShade\ServiceHost\Pages\Components\ProductCategoryViewComponents\Default.cshtml"
                                                                         WriteLiteral(productCategory.Slug);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                    </div>\r\n                                    <div class=\"single-category-item__content\">\r\n                                        <h3 class=\"title\">");
#nullable restore
#line 22 "C:\Users\HP O M E N\Documents\C#\Atriya\MVC_Final_Project\Code\LampShade\ServiceHost\Pages\Components\ProductCategoryViewComponents\Default.cshtml"
                                                     Write(productCategory.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9d2f76aed031e69be64d9374b558a9be42d8a14810941", async() => {
                WriteLiteral("\r\n                                            <i class=\"ion-android-arrow-dropleft-circle\">  ????????  </i>    \r\n                                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 23 "C:\Users\HP O M E N\Documents\C#\Atriya\MVC_Final_Project\Code\LampShade\ServiceHost\Pages\Components\ProductCategoryViewComponents\Default.cshtml"
                                                                          WriteLiteral(productCategory.Slug);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                    </div>\r\n                                </div>\r\n                                <!--=======  End of single category item  =======-->\r\n                            </div>\r\n");
#nullable restore
#line 30 "C:\Users\HP O M E N\Documents\C#\Atriya\MVC_Final_Project\Code\LampShade\ServiceHost\Pages\Components\ProductCategoryViewComponents\Default.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                    </div>\r\n                </div>\r\n                <!--=======  End of category wrapper  =======-->\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n<!--====================  End of category area  ====================-->");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<_01_lampshadeQuery.Contracts.ProductCategory.ProductCategoryQueryModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
