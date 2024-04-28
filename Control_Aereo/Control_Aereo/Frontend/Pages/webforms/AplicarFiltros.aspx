<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AplicarFiltros.aspx.cs" Inherits="Frontend.Pages.webforms.AplicarFiltros" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../css/filtradovuelos/barra.css" rel="stylesheet" />
    <link href="../css/aplicarfiltro/botonesaplicar.css" rel="stylesheet" />
    <link href="../css/aplicarfiltro/aplicarfiltro2.css" rel="stylesheet" />
    <link href="../css/aplicarfiltro/txtb.css" rel="stylesheet" />
    <link href="../css/ModalHangares.css" rel="stylesheet" />
    <link href="../css/inicio/Logo.css" rel="stylesheet" />
    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: 'Roboto', sans-serif;
            background-image:url('../assets/aplicar_filtro/fondo_aplicar_filtro.jpg');
            background-size:cover;
        }
    </style>
    <script>
        function mostrarModal(idModal) {
            document.getElementById(idModal).style.display = "block";
        }

        function cerrarModal(idModal) {
            document.getElementById(idModal).style.display = "none";
        }
    </script>
</head>
<body>
    <form id="formAereopuerto" runat="server" autocomplete="off">
        <div class="navbar">
            <div class="navbar-links">
                <a href="Inicio.aspx">Inicio</a>
                <a href="FiltradoVuelos1.aspx">Filtros</a>
            </div>
        </div>
        <asp:Image ID="logo" runat="server" ImageUrl="~/Pages/assets/inicio/logo.svg" CssClass="logoinicial"/>
        <div class="container">
            <div class="container-card2">
                <h1>Filtrado del listado de vuelos</h1>
                <asp:Image ID="gif1" runat="server" CssClass="gif1" ImageUrl="https://i.pinimg.com/originals/cc/a5/02/cca5022c86f67861746d7cf2eb486de8.gif"/>
                <p class="textosap">
                    En este apartado se podra filtrar hacia</br>
                    la lista de vuelos filtrando siempre lo </br>
                    que quiera el usuario haciendo mas</br>
                    comoda la busqueda del vuelo para el usuario.
                </p>
            </div>
            <div class="container-card">
                <h2>Filtros personalizados</h2>
                <h2>Seleccione el aeropuerto:</h2>
                <asp:DropDownList ID="ddlAereopuertos" runat="server" CssClass="custom-dropdown"></asp:DropDownList>
                <asp:Button ID="btmVerDestinos" runat="server" Text="Destinos" CssClass="botonescuadro" OnClick="btmVerDestinos_Click"/>
                <h2>Seleccione el horario</h2>
                <asp:Button ID="btmAbrirCalendario" CssClass="btnAbrirCalendario" runat="server" Text="Abrir Calendario" OnClick="btmAbrirCalendario_Click"/>
                <div id="modalCalendario" class="modal">
                  <div class="modal-content">
                    <span class="close" onclick="cerrarModal('modalCalendario')">&times;</span>
                    <asp:Calendar ID="cldHorario" runat="server" CssClass="custom-calendar" OnSelectionChanged="Calendar1_SelectionChanged" OnVisibleMonthChanged="Calendar1_VisibleMonthChanged"></asp:Calendar>
                      <asp:Button ID="btmLimpiar" runat="server" Text="Limpiar" OnClick="btmLimpiar_Click"/>
                  </div>
                </div>
                <div class="espaciado">
                    <asp:Button ID="btmAplicar" runat="server" Text="Aplicar" CssClass="botonescuadro" OnClick="btmAplicar_Click"/>
                    <asp:Button ID="btmContinuar" runat="server" Text="Continuar sin filtros" CssClass="botonescuadro" OnClick="btmContinuar_Click"/>
                </div>
            </div>
        </div>
        <div class="container-card3">
            <h1 class="titulo">Metodos de filtrado</h1>
            <asp:Image ID="gif2" runat="server" CssClass="gif2" ImageUrl="https://i.pinimg.com/originals/38/27/ab/3827ab69c03f02d43dcd7b4c659464a7.gif"/>
            <div class="texto2sap">
                <h2>1-Se puede filtrar por el aereopuerto que guste el usuario</h2>
                <h2>2-Tambien se puede filtrar por el destino que se requiera</h2>
                <h2>3-Si se gusta se puede filtrar por fecha</h2>
                <h2>4-Se puede filtrar combinando cualquiera de los puntos</h2>
            </div>
        </div>
        <div id="modalDestinos" class="modal">
            <div class="modal-content">
                <span class="close" onclick="cerrarModal('modalDestinos')">&times;</span>
                <h2>Seleccione el destino:</h2>
                <asp:DropDownList ID="ddlDestinos" runat="server" OnSelectedIndexChanged="ddlDestinos_SelectedIndexChanged"></asp:DropDownList>
                <h2>Destino personalizado:</h2>
                <asp:TextBox ID="txtDestino" runat="server"></asp:TextBox>
                <asp:Button ID="txtConfirmarDestino" runat="server" Text="Continuar" CssClass="boton-modal" OnClick="txtConfirmarDestino_Click"/>
            </div>
        </div>
    </form>
</body>
</html>
