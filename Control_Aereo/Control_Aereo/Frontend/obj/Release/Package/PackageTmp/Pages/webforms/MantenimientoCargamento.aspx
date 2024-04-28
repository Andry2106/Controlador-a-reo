<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Sitio.Master" AutoEventWireup="true" CodeBehind="MantenimientoCargamento.aspx.cs" Inherits="Frontend.Pages.webforms.MantenimientoCargamento" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../css/MantenimientoCargamento.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="card">
            <div class="card-header">
                <img src="../assets/logo.png" style="width:100px; height:100px; position:absolute; left:225px;"/>
                <h2>Mantenimiento cargamento</h2>
                <asp:LinkButton ID="btmAgregar" runat="server"  CssClass="styled-linkbutton">Agregar</asp:LinkButton>
                <asp:LinkButton ID="btmModificar" runat="server"  CssClass="styled-linkbutton">Modificar</asp:LinkButton>
                <asp:LinkButton ID="btmEliminar" runat="server"  CssClass="styled-linkbutton">Eliminar</asp:LinkButton>
            </div>
            <div class="card-body gridview-container">
                <asp:MultiView ID="mvVuelos" runat="server">
                    <asp:View ID="View1" runat="server">
                        <asp:GridView ID="GvCargamento" runat="server" CssClass="styled-gridview"></asp:GridView>
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
    </div>
</asp:Content>
