#pragma checksum "D:\DoAnTN\Dotnet\Admin\Admin\Views\Login\UserRegister.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a19fd96c3638cd00ba3a5c04164e490e50e11d66"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Login_UserRegister), @"mvc.1.0.view", @"/Views/Login/UserRegister.cshtml")]
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
#line 1 "D:\DoAnTN\Dotnet\Admin\Admin\Views\_ViewImports.cshtml"
using Admin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\DoAnTN\Dotnet\Admin\Admin\Views\_ViewImports.cshtml"
using Admin.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a19fd96c3638cd00ba3a5c04164e490e50e11d66", @"/Views/Login/UserRegister.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"425c13ece3d0cfbb1fc0f84962af70815be6a0e8", @"/Views/_ViewImports.cshtml")]
    public class Views_Login_UserRegister : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("login100-form validate-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Login", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "UserRegister", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\DoAnTN\Dotnet\Admin\Admin\Views\Login\UserRegister.cshtml"
  
    ViewData["Title"] = "UserRegister";
    Layout = "~/Views/Shared/_Login.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"limiter\">\r\n    <div class=\"container-login100\" style=\"background-image: url(\'~/images/bg-01.jpg\');\">\r\n        <div class=\"wrap-login100 p-l-55 p-r-55 p-t-65 p-b-54\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a19fd96c3638cd00ba3a5c04164e490e50e11d665627", async() => {
                WriteLiteral(@"
                <span class=""login100-form-title p-b-49"">
                    Login
                </span>

                <div class=""wrap-input100 validate-input m-b-23"" data-validate=""Username is reauired"">
                    <span class=""label-input100"">Username</span>
                    <input class=""input100"" type=""text"" name=""username"" placeholder=""Username"">
                    <span class=""focus-input100"" data-symbol=""&#xf206;""></span>
                </div>

                <div class=""wrap-input100 validate-input"" data-validate=""Password is required"">
                    <span class=""label-input100"">Password</span>
                    <input class=""input100"" type=""password"" name=""pass"" placeholder=""Password"">
                    <span class=""focus-input100"" data-symbol=""&#xf190;""></span>
                </div>
                <div class=""wrap-input100 validate-input"" data-validate=""Password is required"">
                    <span class=""label-input100"">Confirm Password</span>");
                WriteLiteral(@"
                    <input class=""input100"" type=""password"" name=""confirm_pass"" placeholder=""Confirm your password"">
                    <span class=""focus-input100"" data-symbol=""&#xf190;""></span>
                </div>

                <div class=""wrap-input100 validate-input"" data-validate=""Password is required"">
                    <span class=""label-input100"">Loại tài khoản</span>
                    <select name=""type_taikhoan"">
                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a19fd96c3638cd00ba3a5c04164e490e50e11d667476", async() => {
                    WriteLiteral("Sinh viên");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a19fd96c3638cd00ba3a5c04164e490e50e11d668726", async() => {
                    WriteLiteral("Giao vien");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                    </select>

                </div>

                <div class=""wrap-input100 validate-input m-b-23"" data-validate=""Username is reauired"">
                    <span class=""label-input100"">Tên</span>
                    <input class=""input100"" type=""text"" name=""name"" placeholder=""Username"">
                    <span class=""focus-input100"" data-symbol=""&#xf206;""></span>
                </div>
                <div class=""wrap-input100 validate-input m-b-23"" data-validate=""Username is reauired"">
                    <span class=""label-input100"">Địa chỉ</span>
                    <input class=""input100"" type=""text"" name=""diachi"" placeholder=""Địa Chỉ"">
                    <span class=""focus-input100"" data-symbol=""&#xf206;""></span>
                </div>
                <div class=""wrap-input100 validate-input m-b-23"" data-validate=""Username is reauired"">
                    <span class=""label-input100"">Số điện thoại</span>
                    <input class=""input100"" type=""text"" n");
                WriteLiteral(@"ame=""sdt"" placeholder=""Số điện thoại"">
                    <span class=""focus-input100"" data-symbol=""&#xf206;""></span>
                </div>
                <div class=""wrap-input100 validate-input m-b-23"">
                    <span class=""label-input100"">email</span>
                    <input class=""input100"" type=""email"" name=""email"" placeholder=""Email"">
                    <span class=""focus-input100"" data-symbol=""&#xf206;""></span>
                </div>
   
                <div class=""container-login100-form-btn"">
                    <div class=""wrap-login100-form-btn"">
                        <div class=""login100-form-bgbtn""></div>
                        <button class=""login100-form-btn"">
                            Register
                        </button>
                    </div>
                </div>

                <div class=""txt1 text-center p-t-54 p-b-20"">
                    <span>
                        Or Sign Up Using
                    </span>
                <");
                WriteLiteral(@"/div>

                <div class=""flex-c-m"">
                    <a href=""#"" class=""login100-social-item bg1"">
                        <i class=""fa fa-facebook""></i>
                    </a>

                    <a href=""#"" class=""login100-social-item bg2"">
                        <i class=""fa fa-twitter""></i>
                    </a>

                    <a href=""#"" class=""login100-social-item bg3"">
                        <i class=""fa fa-google""></i>
                    </a>
                </div>

                <div class=""flex-col-c p-t-155"">
                    <span class=""txt1 p-b-17"">
                        Or Sign Up Using
                    </span>

                    <a href=""#"" class=""txt2"">
                        Sign Up
                    </a>
                </div>
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n<div id=\"dropDownSelect1\"></div>\r\n");
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
