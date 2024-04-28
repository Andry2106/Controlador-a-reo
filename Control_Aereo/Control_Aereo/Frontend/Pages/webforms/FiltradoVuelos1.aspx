<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FiltradoVuelos1.aspx.cs" Inherits="Frontend.Pages.webforms.FiltradoVuelos1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/filtradovuelos/fondo.css" rel="stylesheet" />
    <link href="../css/filtradovuelos/barra.css" rel="stylesheet" />
    <link href="../css/filtradovuelos/primero.css" rel="stylesheet" />
    <link href="../css/filtradovuelos/Car2.css" rel="stylesheet" />
    <link href="../css/filtradovuelos/filtrado2.css" rel="stylesheet" />
    <link href="../css/filtradovuelos/segundo2.css" rel="stylesheet" />
    <link href="../css/ModalHangares.css" rel="stylesheet" />
    <link href="../css/filtradovuelos/Calendario.css" rel="stylesheet" />
    <link href="../css/filtradovuelos/botonCard.css" rel="stylesheet" />
    <link href="../css/inicio/Logo.css" rel="stylesheet" />
    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: 'Roboto', sans-serif;
            background-image:url('../assets/filtro_avion/fondo_filtro.png');
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
    <form id="form1" runat="server" autocomplete="off">
        <div class="navbar">
            <div class="navbar-links">
                <a href="Inicio.aspx">Inicio</a>
            </div>
        </div>
        <asp:Image ID="logo" runat="server" ImageUrl="~/Pages/assets/inicio/logo.svg" CssClass="logoinicial"/>
        <div class="mi-div-con-imagen">
        </div>
        <h1 id="filtrado1">Filtrado de busqueda</h1>
        <p id="filtrado2">
            Este apartado se encarga de filtrar la lista de vuelos para que sea mas facil<br/> 
            encontrar o ubicar los vuelos para su respectiva operacion ya sea despegar o<br/> 
            embarcar , va a facilitar mucho el funcionamiento del sistema y dar mas<br/>  
            velocidad al encargado del control de despegue y embarque.
        </p>
        <div class="card">
          <div class="content">
            <p class="heading">Aereopuerto</p>
            <p class="para">
              El filtro de busqueda por aereopuerto basicamente va a mostrar solo los
              vuelos que van a despegar de ese aereopuerto seleccionado.
            </p>
          </div>
        </div>

        <div class="card">
          <div class="content">
            <p class="heading">Destino</p>
            <p class="para">
              El filtro de busqueda por destino basicamente va a mostrar solo los
              vuelos que van hacia el lugar de destino establecido.
            </p>
          </div>
        </div>

        <div class="card">
          <div class="content">
            <p class="heading">Horario</p>
            <p class="para">
              El filtro de busqueda por horario basicamente va a mostrar solo los
              vuelos que van a despegar en el horario seleccionado.
            </p>
          </div>
        </div>
        <div class="segundo">
            <h1>Seleccionar</h1>
            <p class="para">
                Ahora conociendo las opciones anteriores seleccione una de las opciones que se muestran <br/> 
                a continuacion para proceder con alguna de las opereaciones a realizar ya sea despegar<br/>
                o embarcar para seguir con los procedimientos de la aereolinea.
            </p>
            <h3 class="elementos">Aereopuerto</h3>
            <h3 class="elementos">Destino</h3>
            <h3 class="elementos">Horario</h3>
            <div class="elementos2">
                <asp:ImageButton ID="btmAereopuerto" runat="server" ImageUrl="../assets/icono_aereopuerto.gif" CssClass="imagenselect" OnClick="btmAereopuerto_Click"/>
                <asp:ImageButton ID="btmDestino" runat="server" ImageUrl="../assets/icono_destino.gif" CssClass="imagenselect2" OnClick="btmDestino_Click"/>
                <asp:ImageButton ID="btmHorario" runat="server" ImageUrl="../assets/icono_horario.gif" CssClass="imagenselect3" OnClick="btmHorario_Click"/>
            </div>
            <div>
                <asp:Button ID="btmPersonalizado" runat="server" Text="Filtro personalizado" OnClick="btmPersonalizado_Click" CssClass="botoncardp"/>
            </div>
        </div>
        <div id="modalAereopuerto" class="modal">
            <div class="modal-content">
                <span class="close" onclick="cerrarModal('modalAereopuerto')">&times;</span>
                <h2>Filtrado por aereopuerto</h2>
                <h3>Aereopuerto</h3>
                <asp:DropDownList ID="ddlAereopuerto" runat="server"></asp:DropDownList>
                <asp:Button ID="btmFiltrarAereopuerto" runat="server" Text="Filtrar" CssClass="boton-modal" OnClick="btmFiltrarAereopuerto_Click" />
            </div>
        </div>
        <div id="modalDestino" class="modal">
            <div class="modal-content">
                <span class="close" onclick="cerrarModal('modalDestino')">&times;</span>
                <h2>Filtrado por destino</h2>
                <h3>Ingrese el destino</h3>
                <asp:TextBox ID="txtDestino" runat="server"></asp:TextBox>
                <asp:Button ID="btmFiltrarDestino" runat="server" Text="Filtrar" CssClass="boton-modal" OnClick="btmFiltrarDestino_Click" />
            </div>
        </div>
        <div id="modalHorario" class="modal">
            <div class="modal-content">
                <span class="close" onclick="cerrarModal('modalHorario')">&times;</span>
                <h2>Filtrado por horario</h2>
                <h3>Ingrese el horario</h3>
                <asp:Calendar ID="cldHorario" runat="server" CssClass="custom-calendar" OnSelectionChanged="cldHorario_SelectionChanged" OnVisibleMonthChanged="cldHorario_VisibleMonthChanged"></asp:Calendar>
                <asp:Button ID="btmFiltrarHorario" runat="server" Text="Filtrar" CssClass="boton-modal" OnClick="btmFiltrarHorario_Click" />
            </div>
        </div>
    </form>
</body>
</html>
