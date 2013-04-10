﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/DataViz.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
<style>
    .chart-wrapper {
        margin: auto;
        width: 466px;
        height: 434px;
        background: url("<%= Url.Content("~/Content/dataviz/shared/chart-wrapper-small.png") %>") transparent no-repeat 0 0;
    }
                
    .chart-wrapper .k-chart {
        height: 280px;
        padding: 37px;
        width: 390px;
    }
</style>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="configuration k-widget k-header" style="width:170px;">
    <span class="configHead">API Functions</span>
    <ul class="options">
        <li>
            <input id="typeColumn" name="seriesType"
                    type="radio" value="column" checked="checked" autocomplete="off" />
            <label for="typeColumn">Columns</label>
        </li>
        <li>
            <input id="typeBar" name="seriesType"
                    type="radio" value="bar" autocomplete="off" />
            <label for="typeBar">Bars</label>
        </li>
        <li>
            <input id="typeLine" name="seriesType"
                    type="radio" value="line" autocomplete="off" />
            <label for="typeLine">Lines</label>
        </li>
        <li>
            <input id="stack" type="checkbox" autocomplete="off" checked="checked" />
            <label for="stack">Stacked</label>
        </li>
    </ul>
    <p>
        <strong>refresh()</strong> will be called on each configuration change
    </p>
</div>

<div class="chart-wrapper">
    <%= Html.Kendo().Chart()
        .Name("chart")
        .Title("Internet Users")
        .Legend(legend => legend
            .Position(ChartLegendPosition.Bottom)
        )
        .SeriesDefaults(seriesDefaults => seriesDefaults
            .Column().Stack(true)
        )
        .Series(series =>
        {
            series.Column(new double[] { 67.96, 68.93, 75, 74, 78 }).Name("United States");
            series.Column(new double[] { 15.7, 16.7, 20, 23.5, 26.6 }).Name("World");
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

<script>
    $(document).ready(function() {
        $(".configuration").bind("change", refresh);
    });

    function refresh() {
        var chart = $("#chart").data("kendoChart"),
            series = chart.options.series,
            type = $("input[name=seriesType]:checked").val(),
            stack = $("#stack").prop("checked");

        for (var i = 0, length = series.length; i < length; i++) {
            series[i].stack = stack;
            series[i].type = type;
        };

        chart.refresh();
    }
</script>
</asp:Content>
