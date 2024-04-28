<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Sitio.Master" AutoEventWireup="true" CodeBehind="Hangares.aspx.cs" Inherits="Frontend.Pages.webforms.Hangares" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../css/ListadoVuelos.css" rel="stylesheet" />
    <link href="../css/Hangares.css" rel="stylesheet" />
    <link href="../css/ModalHangares.css" rel="stylesheet" />
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap" rel="stylesheet">
    <link href="../css/TablaHangares.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="card">
            <div class="card-header">
                <img src="../assets/logo.png" style="width: 100px; height: 100px; position: absolute; left: 225px;" />

                <h2 class="letrash">Mantenimiento hangares</h2>
                <asp:Button ID="BtmAgregar" runat="server" Text="Agregar" CssClass="botoneshangares" OnClick="BtmAgregar_Click" />
                <asp:Button ID="BtmModificar" runat="server" Text="Modificar" CssClass="botoneshangares" OnClick="BtmModificar_Click" />
                <asp:Button ID="BtmEliminar" runat="server" Text="Eliminar" CssClass="botoneshangares" OnClick="BtmEliminar_Click" />
            </div>
            <div class="card-body gridview-container">
                <asp:GridView ID="GvHangares" runat="server" CssClass="my-custom-gridview" AutoGenerateColumns="true" OnRowDataBound="GvHangares_RowDataBound">
                </asp:GridView>
            </div>
        </div>
    </div>
    <div id="modalAgregar" class="modal">
        <div class="modal-content">
            <span class="close" onclick="cerrarModal('modalAgregar')">&times;</span>
            <h2>Agregar hangar</h2>
            <h3>Tipo de hangar</h3>
            <asp:DropDownList ID="ddlTiposHangaresAgregar" runat="server"></asp:DropDownList>
            <h3>Nombre del hangar</h3>
            <asp:TextBox ID="txtNombreAgregar" runat="server" autocomplete="off"></asp:TextBox>
            <h3>Ubicacion</h3>
            <asp:TextBox ID="txtUbicacionAgregar" runat="server" autocomplete="off"></asp:TextBox>
            <h3>Capacidad</h3>
            <asp:TextBox ID="txtCapacidadAgregar" runat="server" autocomplete="off"></asp:TextBox>
            <h3>Estado</h3>
            <asp:TextBox ID="txtEstadoAgregar" runat="server" autocomplete="off"></asp:TextBox>
            <h3>Tamaño</h3>
            <asp:TextBox ID="txtTamañoAgregar" runat="server" autocomplete="off"></asp:TextBox>
            <h3>Altura</h3>
            <asp:TextBox ID="txtAlturaAgregar" runat="server" autocomplete="off"></asp:TextBox>
            <h3>Aereopuerto</h3>
            <asp:DropDownList ID="ddlAereopuertoAgregar" runat="server"></asp:DropDownList>
            <asp:Button ID="btmAgregarModal" runat="server" Text="Agregar" CssClass="boton-modal" OnClick="btmAgregarModal_Click" OnClientClick="return validarFormularioAgregar();" />
        </div>
    </div>
    <div id="modalModificar" class="modal">
        <div class="modal-content">
            <span class="close" onclick="cerrarModal('modalModificar')">&times;</span>
            <h2>Modificar hangar</h2>
            <h3>Tipo de hangar</h3>
            <asp:DropDownList ID="ddlTiposHangaresModificar" runat="server"></asp:DropDownList>
            <h3>Nombre del hangar</h3>
            <asp:TextBox ID="txtNombreModificar" runat="server" autocomplete="off"></asp:TextBox>
            <h3>Ubicacion</h3>
            <asp:TextBox ID="txtUbicacionModificar" runat="server" autocomplete="off"></asp:TextBox>
            <h3>Capacidad</h3>
            <asp:TextBox ID="txtCapacidadModificar" runat="server" autocomplete="off"></asp:TextBox>
            <h3>Estado</h3>
            <asp:TextBox ID="txtEstadoModificar" runat="server" autocomplete="off"></asp:TextBox>
            <h3>Tamaño</h3>
            <asp:TextBox ID="txtTamañoModificar" runat="server" autocomplete="off"></asp:TextBox>
            <h3>Altura</h3>
            <asp:TextBox ID="txtAlturaModificar" runat="server" autocomplete="off"></asp:TextBox>
            <h3>Aereopuerto</h3>
            <asp:DropDownList ID="ddlAereopuertoModificar" runat="server"></asp:DropDownList>
            <asp:Button ID="btmModificarModal" runat="server" Text="Modificar" CssClass="boton-modal" OnClick="BtmModificarModal_Click" OnClientClick="return validarFormularioModificar();" />
        </div>
    </div>
    <asp:HiddenField ID="HiddenFieldHangarName" runat="server" />
    <script>
        function mostrarModal(idModal) {
            document.getElementById(idModal).style.display = "block";
        }

        function cerrarModal(idModal) {
            document.getElementById(idModal).style.display = "none";
        }
    </script>
    <script type="text/javascript">
        function handleRowClick(rowIndex) {
            var grid = document.getElementById('<%= GvHangares.ClientID %>');
            var rows = grid.getElementsByTagName("tr");
            for (var i = 0; i < rows.length; i++) {
                rows[i].style.backgroundColor = "";
            }
            var row = rows[rowIndex + 1];
            row.style.backgroundColor = "lightblue";
            var rowIndexDisplay = rowIndex + 1;
            var hangarName = row.cells[1].innerText;
            document.getElementById('<%= HiddenFieldHangarName.ClientID %>').value = hangarName;
        }
    </script>
    <script type="text/javascript">
        function mostrarModalYSetearNombre(modalId, hangarName) {
            mostrarModal(modalId);
            document.getElementById('<%= txtNombreAgregar.ClientID %>').value = hangarName;
        }
    </script>
    <script>
        function validarFormularioAgregar() {
            var nombre = document.getElementById("txtNombreAgregar").value;
            var ubicacion = document.getElementById("txtUbicacionAgregar").value;
            var capacidad = document.getElementById("txtCapacidadAgregar").value;
            var estado = document.getElementById("txtEstadoAgregar").value;

            if (nombre.trim() === "" || ubicacion.trim() === "" || capacidad.trim() === "" || estado.trim() === "") {
                alert("Por favor complete todos los campos.");
                return false;
            }

            if (!esNumero(capacidad)) {
                alert("Por favor ingrese un número válido en el campo de capacidad.");
                return false;
            }

            return true;
        }

        function esNumero(valor) {
            return !isNaN(parseFloat(valor)) && isFinite(valor);
        }

        function validarFormularioModificar() {
            var nombre = document.getElementById("txtNombreModificar").value;
            var ubicacion = document.getElementById("txtUbicacionModificar").value;
            var capacidad = document.getElementById("txtCapacidadModificar").value;
            var estado = document.getElementById("txtEstadoModificar").value;

            if (nombre.trim() === "" || ubicacion.trim() === "" || capacidad.trim() === "" || estado.trim() === "") {
                alert("Por favor complete todos los campos.");
                return false;
            }
            if (!esNumero(capacidad)) {
                alert("Por favor ingrese un número válido en el campo de capacidad.");
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
