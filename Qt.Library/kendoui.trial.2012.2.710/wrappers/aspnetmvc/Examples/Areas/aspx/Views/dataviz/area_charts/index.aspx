﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/DataViz.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
<style>
    #chart {
        background: center no-repeat url('<%= Url.Content("~/Content/shared/world-map.png") %>')
    }
</style>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="chart-wrapper">
    <%= Html.Kendo().Chart()
        .Name("chart")
        .Title("Internet Users")
        .Legend(legend => legend
            .Position(ChartLegendPosition.Bottom)
        )
        .ChartArea(chartArea => chartArea
            .Background("transparent")
        )
        .Series(series => {
            series.Area(new double[] { 15.7, 16.7, 20, 23.5, 26.6 }).Name("World");
            series.Area(new double[] { 67.96, 68.93, 75, 74, 78 }).Name("United States");
        })
        .CategoryAxis(axis => axis
            .Categories("2005", "2006", "2007", "2008", "2009")
        )
        .ValueAxis(axis => axis
            .Numeric().Labels(labels => labels.Format("{0}%"))
        )
        .Tooltip(tooltip => tooltip
            .Visible(true)
            .Format("{0}%")
        )
    %>
</div>
</asp:Content>
