﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/DataViz.Master"
         Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="chart-wrapper">
    <%= Html.Kendo().Chart<Kendo.Mvc.Examples.Models.ElectricityProduction>()
        .Name("chart")
        .Title("Spain electricity production (GWh)")
        .Legend(legend => legend
            .Position(ChartLegendPosition.Top)
        )
        .DataSource(ds => ds.Read(read => read.Action("_SpainElectricityProduction", "Api")))
        .Series(series =>
        {
            series.Column(model => model.Nuclear).Name("Nuclear");
            series.Column(model => model.Hydro).Name("Hydro");
            series.Column(model => model.Wind).Name("Wind");
        })
        .CategoryAxis(axis => axis
            .Categories(model => model.Year)
            .Labels(labels => labels.Rotation(-90))
        )
        .ValueAxis(axis => axis.Numeric()
            .Labels(labels => labels.Format("{0:N0}"))
            .MajorUnit(10000)
        )
        .Tooltip(tooltip => tooltip
            .Visible(true)
            .Format("{0:N0}")
        )
        .Events(events => events
            .SeriesClick("onSeriesClick")
            .SeriesHover("onSeriesHover")
            .DataBound("onDataBound")
            .AxisLabelClick("onAxisLabelClick")
            .PlotAreaClick("onPlotAreaClick")
        )
    %>
</div>

<script>
    function onSeriesClick(e) {
        kendoConsole.log(kendo.format("Series click :: {0} ({1}): {2}",
            e.series.name, e.category, e.value));
    }

    function onSeriesHover(e) {
        kendoConsole.log(kendo.format("Series hover :: {0} ({1}): {2}",
            e.series.name, e.category, e.value));
    }

    function onDataBound(e) {
        kendoConsole.log("Data bound");
    }

    function onAxisLabelClick(e) {
        kendoConsole.log(kendo.format("Axis label click :: {0} axis : {1}",
            e.axis.type, e.text));
    }

    function onPlotAreaClick(e) {
        kendoConsole.log(kendo.format("Plot area click :: {0} : {1:N0}",
            e.category, e.value));
    }
</script>

<div class="console"></div>

</asp:Content>
