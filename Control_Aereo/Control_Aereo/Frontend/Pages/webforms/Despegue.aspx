<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Sitio.Master" AutoEventWireup="true" CodeBehind="Despegue.aspx.cs" Inherits="Frontend.Pages.webforms.Despegue" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../css/prueba.css" rel="stylesheet" />
    <link href="../css/ModalHangares.css" rel="stylesheet" />
    <link href="../css/gifs_despegue/gifsdespegue.css" rel="stylesheet" />
    <link href="../css/Validaciones.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script>
        function mostrarModal(idModal) {
            document.getElementById(idModal).style.display = "block";
        }

        function cerrarModal(idModal) {
            document.getElementById(idModal).style.display = "none";
        }
    </script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="card">
            <div class="card-header">
                <h3>Controlador de indicadores</h3>
            </div>
            <div class="card-body">
                <h4>Recopilacion de datos</h4>
                <asp:Label ID="Label1" runat="server" Text="✔Informacion meteorologica" CssClass="textos"></asp:Label>
                <asp:Label ID="Label2" runat="server" Text="✔Coordinacion" CssClass="textos"></asp:Label>
                <h4>Separacion inicial</h4>
                <asp:Label ID="Label8" runat="server" Text="✔Se garantizo el espacio correcto entre aereonaves" CssClass="textos"></asp:Label>
                <h4>Embarcamiento</h4>
                <asp:Label ID="Label9" runat="server" Text="✔Equipaje y pasajeros segun los parametros del avion" CssClass="textos"></asp:Label>
                <asp:Button ID="btmValidar" runat="server" Text="Validar" CssClass="botonvalidar" OnClick="btmValidar_Click"/>
            </div>
        </div>
    </div>
    <div class="centrocontent">
        <div class="cardcentro">
            <div class="cardcentro-header">
                <img src="../assets/logo.png" style="width:150px; height:125px; position:absolute; left:415px;"/>
                <h1>Panel de control de despegue</h1>
            </div>
            <div class="cardcentro-body">
                <div class="cardadentro">
                    <h4>Avion</h4>
                    <asp:Label ID="lblIdentificacionAvion" runat="server" Text="N.Vuelo: " CssClass="textos"></asp:Label>
                    <asp:Label ID="lblModelo" runat="server" Text="Identificacion del avion: " CssClass="textos"></asp:Label>
                    <asp:Label ID="lblPesoMaximo" runat="server" Text="Peso maximo: " CssClass="textos"></asp:Label>
                    <asp:Label ID="lblCapacidadPasajeros" runat="server" Text="Alcance: " CssClass="textos"></asp:Label>
                    <asp:Label ID="lblCapacidadCombustible" runat="server" Text="Velocidad: " CssClass="textos"></asp:Label>
                    <asp:Label ID="lblUltimaRevision" runat="server" Text="Capacidad: " CssClass="textos"></asp:Label>
                    <asp:Label ID="lblProximaRevision" runat="server" Text="Combustible: " CssClass="textos"></asp:Label>
                    <asp:Label ID="lblCompaniaAerea" runat="server" Text="Combustible: " CssClass="textos"></asp:Label>
                    <asp:Label ID="lblObservaciones" runat="server" Text="Combustible: " CssClass="textos"></asp:Label>
                    <asp:Label ID="lblEstado" runat="server" Text="Combustible: " CssClass="textos"></asp:Label>

                </div>
                <div class="carderecha">
                    <h1>Informacion del vuelo</h1>
                    <asp:Label ID="lblVuelo" runat="server" Text="N.Vuelo: " CssClass="textos"></asp:Label>
                    <asp:Label ID="lblOrigen" runat="server" Text="Hora de salida: " CssClass="textos"></asp:Label>
                    <asp:Label ID="lblDestino" runat="server" Text="Hora de llegada: " CssClass="textos"></asp:Label>
                    <asp:Label ID="lblHoraSalida" runat="server" Text="Cantidad de pasajeros: " CssClass="textos"></asp:Label>
                    <asp:Label ID="lblHoraLlegada" runat="server" Text="Duracion del vuelo: " CssClass="textos"></asp:Label>

                </div>
                <asp:Image ID="avi1" runat="server" ImageUrl="~/Pages/assets/despegue/avion_despegue.gif" CssClass="gf1"/>
                <div class="botones">
                    <asp:Button ID="btmAutorizar" runat="server" Text="Autorizar Despegue" CssClass="botonautorizar" OnClick="btmAutorizar_Click"/>
                    <asp:Button ID="btmDenegar" runat="server" Text="Denegar Despegue" CssClass="botondenegar" OnClick="btmDenegar_Click"/>
                </div>
            </div>
        <div id="modalDespegue" class="modal">
            <div class="modal-content">
                <span class="close" onclick="cerrarModal('modalDespegue')">&times;</span>
                <h2>Listado de validaciones</h2>
                <div class="tarjeta">
                        <asp:Button ID="btmInfoMeteorologica" runat="server" Text="Condicion Meteorologica" CssClass="textos2" OnClick="btmInfoMeteorologica_Click"/>
                        <asp:Button ID="btmRutaEstablecida" runat="server" Text="Ruta establecida" CssClass="textos2" OnClick="btmRutaEstablecida_Click"/>
                        <asp:Button ID="btmCoordinacion" runat="server" Text="Coordinacion" CssClass="textos2" OnClick="btmCoordinacion_Click"/>
                        <asp:Button ID="btmSeparacionInicial" runat="server" Text="Separacion Inicial" CssClass="textos2" OnClick="btmSeparacionInicial_Click"/>
                        <asp:Button ID="btmEspacio" runat="server" Text="Espacio correcto entre aereonaves" CssClass="textos2" OnClick="btmEspacio_Click"/>
                        <asp:Button ID="btmEmbarcamiento" runat="server" Text="Embarcamiento" CssClass="textos2" OnClick="btmEmbarcamiento_Click"/>
                        <asp:Button ID="btmEquipajePasajeros" runat="server" Text="Equipaje y pasajeros" CssClass="textos2" OnClick="btmEquipajePasajeros_Click"/>
                </div>
                <asp:Button ID="btmValidarModal" runat="server" Text="Validar" CssClass="boton-modal" OnClick="btmValidarModal_Click"/>
            </div>
        </div>
        <div id="modalTextoValidacion" class="modal">
            <div class="modal-content">
                <span class="close" onclick="cerrarModal('modalTextoValidacion')">&times;</span>
                <h2>Estado de validacion</h2>
                <asp:Label ID="lblEstadoValidacion" runat="server" Text=""></asp:Label>
            </div>
        </div>
        </div>
    </div>
</asp:Content>
