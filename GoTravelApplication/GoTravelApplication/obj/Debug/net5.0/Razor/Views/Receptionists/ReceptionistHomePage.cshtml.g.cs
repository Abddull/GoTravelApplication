#pragma checksum "C:\GoTravel\repo testing\GoTravelApplication\GoTravelApplication\GoTravelApplication\Views\Receptionists\ReceptionistHomePage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "835e25a0a3705fc860f6050158d39caedd8ceff7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Receptionists_ReceptionistHomePage), @"mvc.1.0.view", @"/Views/Receptionists/ReceptionistHomePage.cshtml")]
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
#line 1 "C:\GoTravel\repo testing\GoTravelApplication\GoTravelApplication\GoTravelApplication\Views\_ViewImports.cshtml"
using GoTravelApplication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\GoTravel\repo testing\GoTravelApplication\GoTravelApplication\GoTravelApplication\Views\_ViewImports.cshtml"
using GoTravelApplication.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"835e25a0a3705fc860f6050158d39caedd8ceff7", @"/Views/Receptionists/ReceptionistHomePage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5e273dc96806dda8e1a43cbeb56809e2fddeaa70", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Receptionists_ReceptionistHomePage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<GoTravelApplication.Model.Receptionist>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "BookingDetails", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<h1>Receptionist Home Page</h1>\r\n<h4>\r\n    Welcome ");
#nullable restore
#line 5 "C:\GoTravel\repo testing\GoTravelApplication\GoTravelApplication\GoTravelApplication\Views\Receptionists\ReceptionistHomePage.cshtml"
       Write(Model.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</h4>\r\n\r\n");
#nullable restore
#line 8 "C:\GoTravel\repo testing\GoTravelApplication\GoTravelApplication\GoTravelApplication\Views\Receptionists\ReceptionistHomePage.cshtml"
  
    var Msg = "";
    if (ViewData["msg"] != null)
        if (ViewData["msg"].ToString() != "Fine")
            Msg = ViewData["msg"].ToString();
    if (Msg != "")
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <h5 style=\"color:red;\">");
#nullable restore
#line 15 "C:\GoTravel\repo testing\GoTravelApplication\GoTravelApplication\GoTravelApplication\Views\Receptionists\ReceptionistHomePage.cshtml"
                          Write(Msg);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5><br>\r\n");
#nullable restore
#line 16 "C:\GoTravel\repo testing\GoTravelApplication\GoTravelApplication\GoTravelApplication\Views\Receptionists\ReceptionistHomePage.cshtml"
    }

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "835e25a0a3705fc860f6050158d39caedd8ceff75503", async() => {
                WriteLiteral("\r\n    <input type=\"hidden\"");
                BeginWriteAttribute("value", " value=\"", 444, "\"", 473, 1);
#nullable restore
#line 19 "C:\GoTravel\repo testing\GoTravelApplication\GoTravelApplication\GoTravelApplication\Views\Receptionists\ReceptionistHomePage.cshtml"
WriteAttributeValue("", 452, Model.ReceptionistId, 452, 21, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" name=\"id\" />\r\n    <input type=\"number\" min=\"0\" name=\"bookId\" required=\"required\"/>\r\n\r\n    <div class=\"form-group\">\r\n        <input type=\"submit\" value=\"Search Booking Id\" />\r\n    </div>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<GoTravelApplication.Model.Receptionist> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591