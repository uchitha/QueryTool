<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/DataViz.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        #gauge-container {
            background: transparent url(<%= Url.Content("~/Content/dataviz/gauge/gauge-container-partial.png") %>) no-repeat 50% 50%;
            width: 386px;
            height: 386px;
            text-align: center;
            margin: 0 auto 30px auto;
        }

        #gauge {
            width: 350px;
            height: 300px;
            margin: 0 auto;
        }

        #gauge-container input {
            margin-top: -11px;
            width: 140px;
        }
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="gauge-container">
    <%= Html.Kendo().RadialGauge()
        .Name("gauge")
        .Pointer(pointer => pointer.Value(65))
        .Scale(scale => scale
            .MinorUnit(5)
            .StartAngle(-30)
            .EndAngle(210)
            .Max(180)
        )
    %>

    <input id="gauge-value" value="65" onchange="javascript:change();">
</div>
<script>
    function change(e) {
        $("#gauge").data("kendoRadialGauge").value($("#gauge-value").val());
    }
</script>
</asp:Content>