#pragma checksum "D:\DoAnTN\Dotnet\Admin\Admin\Properties\CTKetQuas\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5312b1823dbcc0ba5dd7f923e3bb3d10a919b2ab"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Properties_CTKetQuas_Edit), @"mvc.1.0.view", @"/Properties/CTKetQuas/Edit.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5312b1823dbcc0ba5dd7f923e3bb3d10a919b2ab", @"/Properties/CTKetQuas/Edit.cshtml")]
    public class Properties_CTKetQuas_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Admin.Models.CTKetQua>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\DoAnTN\Dotnet\Admin\Admin\Properties\CTKetQuas\Edit.cshtml"
  
    ViewData["Title"] = "Edit";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Edit</h1>

<h4>CTKetQua</h4>
<hr />
<div class=""row"">
    <div class=""col-md-4"">
        <form asp-action=""Edit"">
            <div asp-validation-summary=""ModelOnly"" class=""text-danger""></div>
            <input type=""hidden"" asp-for=""MaCTKetQua"" />
            <div class=""form-group"">
                <label asp-for=""MaKetQua"" class=""control-label""></label>
                <input asp-for=""MaKetQua"" class=""form-control"" />
                <span asp-validation-for=""MaKetQua"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""SinhVien"" class=""control-label""></label>
                <select asp-for=""SinhVien"" class=""form-control"" asp-items=""ViewBag.SinhVienId"">
                </select>  
                <span asp-validation-for=""SinhVien"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""BaiKiemTra"" class=""control-label""></label>
                <select as");
            WriteLiteral(@"p-for=""BaiKiemTra"" class=""form-control"" asp-items=""ViewBag.BaiKiemTraId"">
                </select>
                <span asp-validation-for=""BaiKiemTra"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""CauHoi"" class=""control-label""></label>
                <select asp-for=""CauHoi"" class=""form-control"" asp-items=""ViewBag.CauHoiId"">
                </select>
                <span asp-validation-for=""CauHoi"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""DapAnSVChon"" class=""control-label""></label>
                <input asp-for=""DapAnSVChon"" class=""form-control"" />
                <span asp-validation-for=""DapAnSVChon"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <input type=""submit"" value=""Save"" class=""btn btn-primary"" />
            </div>
        </form>
    </div>
</div>

<div>
    <a asp-action=""");
            WriteLiteral("Index\">Back to List</a>\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 56 "D:\DoAnTN\Dotnet\Admin\Admin\Properties\CTKetQuas\Edit.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Admin.Models.CTKetQua> Html { get; private set; }
    }
}
#pragma warning restore 1591