<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Frontend.Pages.webforms.Inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/filtradovuelos/barra.css" rel="stylesheet" />
    <link href="../css/inicio/inicial.css" rel="stylesheet" />
    <link href="../css/inicio/slider3.css" rel="stylesheet" />
    <link href="../css/filtradovuelos/Car2.css" rel="stylesheet" />
    <link href="../css/inicio/botonanimado.css" rel="stylesheet" />
    <link href="../css/inicio/inicio2.css" rel="stylesheet" />
    <link href="../css/inicio/Logo.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <title></title>
    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: 'Roboto', sans-serif !important; 
        }
    </style>
    <style>
        .texto1 h1, .texto1 p {
            font-family: 'Roboto', sans-serif !important;
            font-weight: 300; 
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar">
            <div class="navbar-links">
                <a href="Inicio.aspx">Inicio</a>
                <a href="Login.aspx">Iniciar sesión</a>
                <a href="Login.aspx">Registrarse</a>
            </div>
        </div>
        <asp:Image ID="logo" runat="server" ImageUrl="~/Pages/assets/inicio/logo.svg" CssClass="logoinicial"/>
        <div class="slider-frame">
            <ul>
                <li>
                    <img src="../assets/inicio/1.png" alt="" /></li>
                <li>
                    <img src="../assets/inicio/2.png" /></li>
                <li>
                    <img src="../assets/inicio/5.png" /></li>
                <li>
                    <img src="../assets/inicio/1.png" alt="" /></li>
            </ul>
        </div>
        <div class="texto1">
            <h1 class="letras1" style="font-family: 'Roboto', sans-serif;">Embarcamiento, Despegue, Desembarque<br />
            </h1>
            <p class="letras1" style="font-family: 'Roboto', sans-serif;">
                Nuestro equipo fue encargado de los temas de lo que serian<br />
                Embarcamiento , Despuegue y desembarque que son muy
                <br />
                importantes para lo que seria el control aereo.
                <br />
                si bien todas las etapas son importante siempre los temas
                <br />
                mencionados anteriormente son de suma importancia para poder
                <br />
                continuar con los demas procesos.
            </p>
        </div>
        <div class="elemento2">
            <h1 class="" style="font-family: 'Roboto', sans-serif;">Asignación de apartados</h1>
            <img src="../assets/inicio/gif1.gif" />
        </div>
        <div class="tarjetas">
            <div class="card">
                <div class="content">
                    <p class="heading" style="font-family: 'Roboto', sans-serif;">Embarcamiento</p>
                    <p class="para" style="font-family: 'Roboto', sans-serif;">
                        Este apartado trata de como se puede embarcar todo el equipaje a lo
                        que seria un respectivo avion de un manera simple , complaciente y eficaz.
                    </p>
                </div>
            </div>
            <div class="card">
                <div class="content">
                    <p class="heading" style="font-family: 'Roboto', sans-serif;">Despegue</p>
                    <p class="para" style="font-family: 'Roboto', sans-serif;">
                        En este otro apartado se basicamente se elige el vuelo a despegar y verificar si
                       cumple con todas las validaciones para que este pueda despegar.
                    </p>
                </div>
            </div>
            <div class="card">
                <div class="content">
                    <p class="heading" style="font-family: 'Roboto', sans-serif;">Desembarque</p>
                    <p class="para" style="font-family: 'Roboto', sans-serif;">
                        Este trata de como el avion es desembarcado y llevado hacia al hangar en el camino
                        desembarcando con pasajeros y sus respectivos equipajes.
                    </p>
                </div>
            </div>
            <div class="contenedorelementos">
                <div class="elemento3">
                    <div class="c2">
                        <h1 class="" style="font-family: 'Roboto', sans-serif;">Embarcamiento</h1>
                        <p class="" style="font-family: 'Roboto', sans-serif;">
                            Este apartado trata de como se puede embarcar todo el equipaje a lo<br />
                            que seria un respectivo avion de un manera simple , complaciente y eficaz.
                        </p>
                        <asp:Button ID="btmEmbarcamiento" runat="server" Text="Seleccionar" CssClass="animated-button" OnClick="btmEmbarcamiento_Click"/>
                    </div>
                </div>
                <div class="elemento4">
                    <div class="c2">
                        <h1 class="" style="font-family: 'Roboto', sans-serif;">Despegue</h1>
                        <p class="" style="font-family: 'Roboto', sans-serif;">
                            En este otro apartado se basicamente se elige el vuelo a despegar y verificar si<br />
                            cumple con todas las validaciones para que este pueda despegar.
                        </p>
                        <asp:Button ID="btmDespegue" runat="server" Text="Seleccionar" CssClass="animated-button" OnClick="btmDespegue_Click"/>
                    </div>
                </div>
                <div class="elemento5">
                    <div class="c2">
                        <h1 class="" style="font-family: 'Roboto', sans-serif;">Desembarque</h1>
                        <p class="" style="font-family: 'Roboto', sans-serif;">
                            Este trata de como el avion es desembarcado y llevado hacia al hangar en el camino<br />
                            desembarcando con pasajeros y sus respectivos equipajes.
                        </p>
                        <asp:Button ID="btmDesembarque" runat="server" Text="Seleccionar" CssClass="animated-button" OnClick="btmDesembarque_Click"/>
                    </div>
                </div>
            </div>
            <div class="elemento6">
            </div>
    </form>
    <script>
        const elementos = document.querySelectorAll('.elemento3, .elemento4, .elemento5');
        function mostrarOverlay() {
            document.querySelector('.overlay').style.display = 'block';
        }
        function ocultarOverlay() {
            document.querySelector('.overlay').style.display = 'none';
        }
        elementos.forEach(elemento => {
            elemento.addEventListener('mouseenter', mostrarOverlay);
            elemento.addEventListener('mouseleave', ocultarOverlay);
        });
    </script>
</body>
</html>
