<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Sitio.Master" AutoEventWireup="true" CodeBehind="ListaVuelos.aspx.cs" Inherits="Frontend.Pages.Formulario_web1" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../css/ListadoVuelos.css" rel="stylesheet" />
    <link href="../css/Hangares.css" rel="stylesheet" />
    <link href="../css/TablaHangares.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="card">
            <div class="card-header">
                <img src="../assets/logo.png" style="width:100px; height:100px; position:absolute; left:225px;"/>
                <h2 class="letrash">Lista de vuelos</h2>
                <asp:LinkButton ID="lnkVuelosProgramados" runat="server" OnClick="lnkVuelosProgramados_Click" CssClass="styled-linkbutton">Vuelos programados</asp:LinkButton>
                <asp:LinkButton ID="lnkVuelosCancelados" runat="server" OnClick="lnkVuelosCancelados_Click" CssClass="styled-linkbutton">Vuelos cancelados</asp:LinkButton>
            </div>
            <div class="card-body gridview-container">
                <asp:GridView ID="GvListaVuelos" runat="server" CssClass="my-custom-gridview" AutoGenerateColumns="true" OnRowDataBound="GvListaVuelos_RowDataBound"></asp:GridView>
                <asp:GridView ID="GvVuelosCancelados" runat="server" CssClass="my-custom-gridview" AutoGenerateColumns="true" OnRowDataBound="GvVuelosCancelados_RowDataBound"></asp:GridView>
            </div>
            <asp:Button ID="btmSeleccionar" runat="server" Text="Despegar" CssClass="select-button" OnClick="btmSeleccionar_Click" />
            <asp:Button ID="btmEmbarcar" runat="server" Text="Embarcar" CssClass="select-button" OnClick="btmEmbarcar_Click" />
        </div>
    </div>
    <asp:HiddenField ID="HiddenFieldVuelo" runat="server" />
    <asp:HiddenField ID="HiddenFieldVueloCancelado" runat="server" />
    <asp:HiddenField ID="HiddenFieldIdAvion" runat="server" />
    <asp:HiddenField ID="HiddenFieldOrigen" runat="server" />
    <asp:HiddenField ID="HiddenFieldDestino" runat="server" />
    <script type="text/javascript">
        function handleRowClickListado(rowIndex) {
            var grid = document.getElementById('<%= GvListaVuelos.ClientID %>');
            var rows = grid.getElementsByTagName("tr");
            for (var i = 0; i < rows.length; i++) {
                rows[i].style.backgroundColor = "";
            }
            var row = rows[rowIndex + 1];
            row.style.backgroundColor = "lightblue";
            var rowIndexDisplay = rowIndex + 1;
            var vuelo = row.cells[0].innerText;
            var avionId = row.cells[4].innerText;
            var origen = row.cells[1].innerText;
            var destino = row.cells[2].innerText;
            document.getElementById('<%= HiddenFieldVuelo.ClientID %>').value = vuelo;
            document.getElementById('<%= HiddenFieldIdAvion.ClientID %>').value = avionId
            document.getElementById('<%= HiddenFieldOrigen.ClientID %>').value = origen; 
            document.getElementById('<%= HiddenFieldDestino.ClientID %>').value = destino; 
        }
    </script>
    <script type="text/javascript">
        function handleRowClickCancelado(rowIndex) {
            var grid = document.getElementById('<%= GvVuelosCancelados.ClientID %>');
            var rows = grid.getElementsByTagName("tr");
            for (var i = 0; i < rows.length; i++) {
                rows[i].style.backgroundColor = "";
            }
            var row = rows[rowIndex + 1];
            row.style.backgroundColor = "lightblue";
            var rowIndexDisplay = rowIndex + 1;
            var vuelo = row.cells[0].innerText;
            var avionId = row.cells[4].innerText;
            var origen = row.cells[1].innerText;
            var destino = row.cells[2].innerText;
            document.getElementById('<%= HiddenFieldVuelo.ClientID %>').value = vuelo;
            document.getElementById('<%= HiddenFieldIdAvion.ClientID %>').value = avionId
            document.getElementById('<%= HiddenFieldOrigen.ClientID %>').value = origen; 
            document.getElementById('<%= HiddenFieldDestino.ClientID %>').value = destino;  
        }
    </script>
</asp:Content>
