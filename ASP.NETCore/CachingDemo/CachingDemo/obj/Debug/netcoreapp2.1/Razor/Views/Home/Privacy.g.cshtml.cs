#pragma checksum "D:\07082019\ASP.NETCore\CachingDemo\CachingDemo\Views\Home\Privacy.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6456385c01613173087a9d3e489f0b9fcdf7834f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Privacy), @"mvc.1.0.view", @"/Views/Home/Privacy.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Privacy.cshtml", typeof(AspNetCore.Views_Home_Privacy))]
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
#line 1 "D:\07082019\ASP.NETCore\CachingDemo\CachingDemo\Views\_ViewImports.cshtml"
using CachingDemo;

#line default
#line hidden
#line 2 "D:\07082019\ASP.NETCore\CachingDemo\CachingDemo\Views\_ViewImports.cshtml"
using CachingDemo.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6456385c01613173087a9d3e489f0b9fcdf7834f", @"/Views/Home/Privacy.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d5be6915073caf47543a64655f82c55236f3f6fb", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Privacy : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\07082019\ASP.NETCore\CachingDemo\CachingDemo\Views\Home\Privacy.cshtml"
  
    ViewData["Title"] = "Privacy Policy";

#line default
#line hidden
            BeginContext(50, 4, true);
            WriteLiteral("<h2>");
            EndContext();
            BeginContext(55, 17, false);
#line 4 "D:\07082019\ASP.NETCore\CachingDemo\CachingDemo\Views\Home\Privacy.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(72, 13, true);
            WriteLiteral("</h2>\r\n\r\n<h3>");
            EndContext();
            BeginContext(86, 15, false);
#line 6 "D:\07082019\ASP.NETCore\CachingDemo\CachingDemo\Views\Home\Privacy.cshtml"
Write(ViewBag.Message);

#line default
#line hidden
            EndContext();
            BeginContext(101, 13, true);
            WriteLiteral("</h3>\r\n\r\n<h4>");
            EndContext();
            BeginContext(115, 15, false);
#line 8 "D:\07082019\ASP.NETCore\CachingDemo\CachingDemo\Views\Home\Privacy.cshtml"
Write(ViewBag.Service);

#line default
#line hidden
            EndContext();
            BeginContext(130, 69, true);
            WriteLiteral("</h4>\r\n\r\n<p>Use this page to detail your site\'s privacy policy.</p>\r\n");
            EndContext();
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
