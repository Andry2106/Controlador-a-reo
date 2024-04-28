<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Sitio.Master" AutoEventWireup="true" CodeBehind="EmbarcamientoAviones.aspx.cs" Inherits="Frontend.Pages.webforms.EmbarcamientoAviones" EnableEventValidation="false" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../css/EmbarcamientoAviones.css" rel="stylesheet" />
    <link href="../css/ModalHangares.css" rel="stylesheet" />
    <link href="../css/TablaEmbarcamiento.css" rel="stylesheet" />



</asp:Content>


<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="card">
            <div class="card-header">
                <h3>Seleccionar Avión</h3>
            </div>
            <div class="card-body">
                <asp:Image ID="AvionImagen" runat="server" src="https://i.gifer.com/IQMm.gif" CssClass="AvionImagen" style="height:150px; width:250px;"/>
                <div class="info-avion">
                    <asp:Label ID="LabelIDAvion" runat="server" Text="Identificacion del avion: "></asp:Label>
                    <asp:Label ID="LabelModelo" runat="server" Text="Modelo: "></asp:Label>
                    <asp:Label ID="LabelCompaniaAeria" runat="server" Text="Compania Aerea: "></asp:Label>
                    <asp:Label ID="LabelCapacidadCarga" runat="server" Text="Capacidad Carga: "></asp:Label>
                    <asp:Label ID="LabelCapacidadPasajeros" runat="server" Text="Capacidad Pasajeros: "></asp:Label>
                    <asp:Label ID="LabelCapacidadCombustible" runat="server" Text="Capacidad Combustible: "></asp:Label>
                    <asp:Label ID="LabelEstado" runat="server" Text="Estado: "></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <div class="centrocontent">
        <div class="cardcentro">
            <div class="cardcentro-header">
                <img src="../assets/logo.png" style="width: 150px; height: 100px; position: absolute; left: 460px; top: 70px" />
                <h3>Embarcamiento</h3>
            </div>

            <div class="cardcentro-body">
                <div class="card-header">
                    <asp:LinkButton ID="lnkEmbarcamiento" runat="server" OnClick="lnkEmbarcamiento_Click" CssClass="styled-linkbutton">Embarques</asp:LinkButton>
                    <asp:LinkButton ID="LinkEquipaje" runat="server" OnClick="LinkEquipaje_Click" CssClass="styled-linkbutton">Equipaje</asp:LinkButton>
                    <asp:LinkButton ID="lnkEmbarcamiento_Avion" runat="server" OnClick="lnkEmbarcamiento_Avion_Click" CssClass="styled-linkbutton">Embarcamientos</asp:LinkButton>

                </div>

                <div class="card-body gridview-container">

                    <asp:GridView ID="GvEquipamentos" runat="server" CssClass="my-custom-gridview" AutoGenerateColumns="True"
                        OnRowDataBound="GvEquipamentos_RowDataBound" DataKeyNames="IDEquipamiento">
                    </asp:GridView>

                    <asp:GridView ID="GvDatosEmbarque" runat="server" CssClass="my-custom-gridview" AutoGenerateColumns="True" OnRowDataBound="GvDatosEmbarque_RowDataBound" DataKeyNames="IDEmbarque"></asp:GridView>

                    <asp:DropDownList ID="DDLPuertas" runat="server" CssClass="custom-dropdownlist" Style="width: auto; height: auto; border: solid 1px; font-size: 1em;"></asp:DropDownList>
                    <br />

                    <%-- Button selecion de equipaje para embarque individual --%>
                    <asp:Button ID="btmSeleccionar" runat="server" Text="Seleccionar" OnClick="btmSelecionar_Click" CssClass="select-button" Enabled="false" Style="margin-top: 20px; display: inline-block;" />


                    <%-- Button Embarque Individual select equipaje --%>
                    <asp:Button ID="btmSelecionarEmbarcamiento" runat="server" Text="Selecionar" OnClik=" btmSelecionarEmbarcamiento_Click" CssClass="select-button" Enabled="true" Style="margin-top: 20px;" />

                    <%-- Button Embarque masivo --%>
                    <asp:Button ID="btmEmbarcarMasivo" runat="server" Text="Embarque Automatico" CssClass="select-button" OnClick="btmEmbarcarMasivo_Click" Style="display: inline-block;" />


                    <%-- Button Embarcamientos--%>

                    <asp:Button ID="btmInsertarEmbarcamiento" runat="server" Text="Embarcamiento" OnClick="btmInsertarEmbarcamiento_Click" CssClass="select-button" Enabled="True" Style="margin-top: 20px; display: inline-block;" />

                    <asp:Button ID="btmEmbarcamientoMasivo" runat="server" Text="Embarcamiento Automatico" CssClass="select-button" OnClick="btmEmbarcamientoMasivo_Click" Style="display: inline-block;" />

                    <asp:Button ID="btmEliminar_Embarque_Individual" runat="server" Text="Eliminar Embarque" CssClass="select-button" OnClick="btmEliminar_Embarque_Individual_Click" Style="display: inline-block;" />

                    <asp:Button ID="btmElimiar_Embarque_Masivo" runat="server" Text="Eliminar Embarques" CssClass="select-button" OnClick="btmElimiar_Embarque_Masivo_Click" Style="display: inline-block;" />


                    <%-- Buttons Embarcamientos Avion  --%>

                    <asp:Button ID="btmEmbarcamientoAvion_Eliminar_Individual" runat="server" Text="Eliminar Embarcamiento" CssClass="select-button" OnClick="btmEmbarcamientoAvion_Eliminar_Individual_Click" Style="display: inline-block;" />

                    <asp:Button ID="btmEmbarcamientoAvion_Eliminar_Automatico" runat="server" Text="Eliminar Embarcamientos" CssClass="select-button" OnClick="btmEmbarcamientoAvion_Eliminar_Automatico_Click" Style="display: inline-block;" />
                    
                    <%--                    <asp:Button ID="ccc" runat="server" Text="Insertar" OnClik="btmInsertarEmbarcamiento"  CssClass="select-button" Enabled="false" Style="margin-top:20px;"/>--%>
                </div>

                <div id="modalPasajeros" class="modal">

                    <div class="modal-content" style="overflow-y: auto; max-height: 400px;">

                        <span class="close" onclick="cerrarModal('modalPasajeros')">&times;</span>
                        <h2>Pasajeros</h2>
                        <asp:GridView ID="GvPasajeros" runat="server" CssClass="my-custom-gridview" AutoGenerateColumns="True"
                            OnRowDataBound="GvPasajeros_RowDataBound" DataKeyNames="IDPasajero">
                        </asp:GridView>

                        <asp:Button ID="btmAgregar" runat="server" Text="Embarcar" CssClass="select-button" Visible="false" OnClick="btmAgregar_Click" Style="margin-top: 10px" />

                    </div>

                </div>

            </div>
        </div>
    </div>

    <asp:HiddenField ID="HiddenFieldIDEquipamiento" runat="server" />
    <asp:HiddenField ID="HiddenFieldIDPasajero" runat="server" />
    <asp:HiddenField ID="HiddenFieldIDEmbarque" runat="server" />

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <%-- Script que muestra modal --%>
    <script>
        function mostrarModal(idModal) {
            document.getElementById(idModal).style.display = "block";
        }

        function cerrarModal(idModal) {
            document.getElementById(idModal).style.display = "none";
        }
    </script>

    <script type="text/javascript">
        function handleRowClickEquipamiento(rowIndex) {
            var grid = document.getElementById('<%= GvEquipamentos.ClientID %>');
            var idEquipamiento = grid.rows[rowIndex + 1].getAttribute('data-idequipamiento');
            document.getElementById('<%= HiddenFieldIDEquipamiento.ClientID %>').value = idEquipamiento;
            document.getElementById('<%= btmSeleccionar.ClientID %>').disabled = false;
        }
    </script>


    <%-- Script que remarca de azul la fila selecionada de DataGriew Equipamentos --%>
    <script type="text/javascript">
        function handleRowClickEquipamiento(rowIndex) {
            var grid = document.getElementById('<%= GvEquipamentos.ClientID %>');
            var rows = grid.getElementsByTagName("tr");

            for (var i = 1; i < rows.length; i++) {
                rows[i].classList.remove("row-selected");
            }

            var selectedRow = rows[rowIndex + 1];
            selectedRow.classList.add("row-selected");

            var idEquipamiento = selectedRow.getAttribute('data-idequipamiento');
            document.getElementById('<%= HiddenFieldIDEquipamiento.ClientID %>').value = idEquipamiento;
            document.getElementById('<%= btmSeleccionar.ClientID %>').disabled = false;

            console.log("IDEquipamiento seleccionado: ", idEquipamiento); // Test funcionalidad
        }
    </script>

    <%-- Script que remarca de azul la fila selecionada de DataGriew Pasajeros --%>
    <script type="text/javascript">
        function handleRowClickPasajero(rowIndex) {
            var grid = document.getElementById('<%= GvPasajeros.ClientID %>');
            var rows = grid.getElementsByTagName("tr");

            for (var i = 1; i < rows.length; i++) {
                rows[i].classList.remove("row-selected");
            }

            var selectedRow = rows[rowIndex + 1];
            selectedRow.classList.add("row-selected");

            var idPasajero = selectedRow.getAttribute('data-idpasajero');
            document.getElementById('<%= HiddenFieldIDPasajero.ClientID %>').value = idPasajero;
            document.getElementById('<%= btmAgregar.ClientID %>').disabled = false;

            console.log("IDPasajero seleccionado: ", idPasajero); // Test funcionalidad
        }
    </script>

    <%-- Script que remarca de azul la fila selecionada de DataGriew Pasajeros --%>
    <script type="text/javascript">
        function handleRowClickEmbarque(rowIndex) {
            var grid = document.getElementById('<%= GvDatosEmbarque.ClientID %>');
            var rows = grid.getElementsByTagName("tr");

            for (var i = 1; i < rows.length; i++) {
                rows[i].classList.remove("row-selected");
            }

            var selectedRow = rows[rowIndex + 1];
            selectedRow.classList.add("row-selected");

            var IDEmbarque = selectedRow.getAttribute('data-IDEmbarque');
            document.getElementById('<%= HiddenFieldIDEmbarque.ClientID %>').value = IDEmbarque;
            document.getElementById('<%= btmInsertarEmbarcamiento.ClientID %>').disabled = false;

            console.log("IDEmbarque seleccionado: ", IDEmbarque); // Test funcionalidad
        }
    </script>

</asp:Content>
