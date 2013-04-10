<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="configuration k-widget k-header" style="width: 210px">
    <span class="configHead">API Functions</span>
    <ul class="options">
        <li>
            <button id="enable" class="k-button">Enable</button> or <button id="disable" class="k-button">Disable</button>
        </li>
        <li>
            <button id="open" class="k-button">Open</button> or <button id="close" class="k-button">Close</button> the drop-down
        </li>
        <li>
            <button id="getValue" class="k-button">Get value</button> or <button id="getText" class="k-button">Get text</button>
        </li>
        <li>
            <label for="filter" style="display: block; ">Filter type:</label>
            <select id="filter" style="width: 120px; float: right; margin-bottom: 3px; margin-top: -3px;">
                <option value="none">None</option>
                <option value="startswith">Starts with</option>
                <option value="contains">Contains</option>
                <option value="eq">Equal</option>
            </select>
        </li>
        <li>
            <input id="word" value="The" class="k-textbox" /> <button id="find" class="k-button">Find item</button>
        </li>
        <li>
            <input id="index" value="0" class="k-textbox" /> <button id="select" class="k-button">Select by index</button>
        </li>
        <li>
            <input id="value" value="1" class="k-textbox" /> <button id="setValue" class="k-button">Select by value</button>
        </li>
    </ul>
</div>

<%= Html.Kendo().ComboBox()
    .Name("movies")
    .DataTextField("Text")
    .DataValueField("Value")
    .BindTo(new List<SelectListItem>()
    {
        new SelectListItem() {
        Text = "The Shawshank Redemption", Value ="1"
        },
        new SelectListItem() {
        Text = "The Godfather", Value ="2"
        },
        new SelectListItem() {
        Text = "The Godfather: Part II", Value ="3"
        },
        new SelectListItem() {
        Text = "Il buono, il brutto, il cattivo.", Value ="4"
        },
        new SelectListItem() {
        Text = "Pulp Fiction", Value ="5"
        },
        new SelectListItem() {
        Text = "12 Angry Men", Value ="6"
        },
        new SelectListItem() {
        Text = "Schindler's List", Value ="7"
        },
        new SelectListItem() {
        Text = "One Flew Over the Cuckoo's Nest", Value ="8"
        },
        new SelectListItem() {
        Text = "Inception", Value ="9"
        },
        new SelectListItem() {
        Text = "The Dark Knight", Value ="10"
        }
    })
%>

<script>
    $(document).ready(function() {
        $("#movies").closest(".k-widget")
                    .attr("id", "products_wrapper");

        $("#filter").kendoDropDownList({
            change: filterTypeOnChanged
        });

        var combobox = $("#movies").data("kendoComboBox"),
            setValue = function(e) {
                if (e.type != "keypress" || kendo.keys.ENTER == e.keyCode)
                    combobox.value($("#value").val());
            },
            setIndex = function(e) {
                if (e.type != "keypress" || kendo.keys.ENTER == e.keyCode) {
                    var index = parseInt($("#index").val());
                    combobox.select(index);
                }
            },
            setSearch = function (e) {
                if (e.type != "keypress" || kendo.keys.ENTER == e.keyCode)
                    combobox.search($("#word").val());
            };

        $("#enable").click(function() {
            combobox.enable();
        });

        $("#disable").click(function() {
            combobox.enable(false);
        });

        $("#open").click(function() {
            combobox.open();
        });

        $("#close").click(function() {
            combobox.close();
        });

        $("#getValue").click(function() {
            alert(combobox.value());
        });

        $("#getText").click(function() {
            alert(combobox.text());
        });

        $("#setValue").click(setValue);
        $("#value").keypress(setValue);

        $("#select").click(setIndex);
        $("#index").keypress(setIndex);

        $("#find").click(setSearch);
        $("#word").keypress(setSearch);

        function filterTypeOnChanged() {
            combobox.options.filter = $("#filter").val();
        }
    });
</script>
<style scoped>
    .configuration .k-textbox {
        width: 40px;
    }
</style>
</asp:Content>