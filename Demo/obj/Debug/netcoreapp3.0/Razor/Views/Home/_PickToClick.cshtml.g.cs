#pragma checksum "C:\WhiteSoxExclusive\Demo\Demo\Views\Home\_PickToClick.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b66a536909e5ca9633ba3e66234374ac78f948fd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home__PickToClick), @"mvc.1.0.view", @"/Views/Home/_PickToClick.cshtml")]
namespace AspNetCore
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
#line 1 "C:\WhiteSoxExclusive\Demo\Demo\Views\_ViewImports.cshtml"
using Demo;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\WhiteSoxExclusive\Demo\Demo\Views\_ViewImports.cshtml"
using Demo.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b66a536909e5ca9633ba3e66234374ac78f948fd", @"/Views/Home/_PickToClick.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e18407c5b9dabc62761fc6cdd8f67817f22bc556", @"/Views/_ViewImports.cshtml")]
    public class Views_Home__PickToClick : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n        <div class=\"row\">\r\n            <div class=\"col-5\">Last 10 picks </div><div class=\"col-7 text-right\"></div>\r\n            <div class=\"col-12\"><hr /></div>\r\n            \r\n        </div>\r\n\r\n");
#nullable restore
#line 9 "C:\WhiteSoxExclusive\Demo\Demo\Views\Home\_PickToClick.cshtml"
         foreach (var p in Model.LastTenPicks)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"row\">\r\n                <div class=\"col-4\">");
#nullable restore
#line 12 "C:\WhiteSoxExclusive\Demo\Demo\Views\Home\_PickToClick.cshtml"
                              Write(p.DateOfGame);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div><div class=\"col-8\">");
#nullable restore
#line 12 "C:\WhiteSoxExclusive\Demo\Demo\Views\Home\_PickToClick.cshtml"
                                                                    Write(p.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 12 "C:\WhiteSoxExclusive\Demo\Demo\Views\Home\_PickToClick.cshtml"
                                                                                 Write(p.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                </div>\r\n");
#nullable restore
#line 14 "C:\WhiteSoxExclusive\Demo\Demo\Views\Home\_PickToClick.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
