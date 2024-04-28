<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Sitio.Master" AutoEventWireup="true" CodeBehind="Desembarque.aspx.cs" Inherits="Frontend.Pages.webforms.Desembarque" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../css/Desembarque.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <div class="title-outside-container">
        <img src="../assets/logo.png" style="width: 100px; height: 100px; position: absolute; left: 225px;" />
        <span>Panel de Control de Desembarque</span>
    </div>
    <div class="container">
        <div class="card">
            <div class="title">Control de Desembarque</div>
            <img class="img-small" src="https://cdn.dribbble.com/users/627633/screenshots/2039122/media/d9076fd27b410e40890d75e832894d18.gif" alt="Imagen del avión" style="width: 100%; height: auto; border: 1px solid #ccc;">

            
            <asp:Button ID="btnDesembarcar" runat="server" CssClass="select-hangar-btn select-button" Text="Desembarcar" OnClick="Desembarcar_Click" OnClientClick="return openModalDesembarque();" />
        </div>

        <div class="card">
            <div class="title">Información del Hangar</div>
            <div id="divInformacionHangar" runat="server" ClientIDMode="Static">
                <asp:Button ID="myBtn" runat="server" CssClass="select-hangar-btn select-button" Text="Seleccionar" CommandArgument='<%# Eval("HangarNombre") %>' OnClick="SeleccionarHangar" OnClientClick="openModal(); return false;" />

<div class="card-mediana">
    <div class="details">
        <p><strong>Tipo Hangar:</strong> <span id="idTipoHangar"></span></p>
        <p><strong>Identificador:</strong> <span id="idHangar"></span></p>
        <p><strong>Nombre:</strong> <span id="NombreHangar"></span></p>
        <p><strong>Ubicación:</strong> <span id="ubicacionHangar"></span></p>
        <p><strong>Capacidad:</strong> <span id="capacidadHangar"></span></p>
        <p><strong>Estado:</strong> <span id="estadoHangar"></span></p>
        <p><strong>Tamaño:</strong> <span id="tamañoHangar"></span></p>
        <p><strong>Altura:</strong> <span id="alturaHangar"></span></p>
    </div>
</div>

            </div>
        </div>

        <div class="card-large">
            <div class="title">Mapeo de hangares</div>
            <div id="miniCardsContainer" runat="server" class="mini-cards-container">
            </div>
        </div>
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div id="myModal" class="modal">
    <div class="modal-content" style="width: 80%; max-width: 1700px;"">
        <span class="close" onclick="closeModal()">&times;</span>
        <div class="card-container">
            <div class="card" style="width: 500px;">
                <div class="title">Lista de Hangares</div>
                <div id="hangarListVacioContainer" class="hangar-list" runat="server">
                    <div class="title">Vacios</div>
                </div>
                <div id="hangarListContainer" class="hangar-list" runat="server">
                    <div class="title">Ocupados</div>
                </div>
            </div>
            <div class="card" style="width: 500px;">
                <div class="title">Información del Avión</div>
                <div class="details">
                    <asp:UpdatePanel ID="UpdatePanelAvion" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" style="width: 100%; padding: 8px; border: 1px solid #ccc; border-radius: 4px; box-sizing: border-box;" placeholder="Digite"></asp:TextBox>
                            <br />
                            <asp:Button ID="btnBuscarAviones" runat="server" Text="Buscar" CssClass="info-btn" OnClick="btnBuscarAviones_Click" />
                            <br />
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="custom-gridview">
                                <Columns>
                                    <asp:BoundField DataField="ModeloAvion" HeaderText="Modelo" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnBuscarAviones" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="card" style="width: 600px;">
                <div class="title">Operaciones Avión</div>
                <div class="details">
                    <asp:UpdatePanel ID="updatePanel" runat="server">
                        <ContentTemplate>
                            <div style="max-width: 250px;">
                                <asp:TextBox ID="txtNombreHangar" runat="server" placeholder="Nombre Hangar" style="width: 100%; padding: 8px; border: 1px solid #ccc; border-radius: 4px; box-sizing: border-box;" />
                            </div>
                            <div style="max-width: 250px; margin-top: 10px;">
                                <asp:TextBox ID="txtNumeroRegistroAvion" runat="server" placeholder="Número de Registro Avión" style="width: 100%; padding: 8px; border: 1px solid #ccc; border-radius: 4px; box-sizing: border-box;" />
                            </div>
                            <asp:Button ID="btnAgregarAvion" runat="server" Text="Enviar a Hangar" CssClass="info-btn" OnClientClick="mostrarMensaje('No hubo fallas');" OnClick="btnAgregarAvion_Click" />
                            <asp:Button ID="btnEliminarAvion" runat="server" Text="Sacar de Hangar" CssClass="info-btn" OnClientClick="mostrarMensaje('No hubo fallas');" OnClick="btnEliminarAvion_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="card" style="width: 800px;">
                <div class="title">Lista de Aviones</div>
                <div class="details">
                    <div class="scrollable-table">
                    <asp:GridView ID="gvAviones" runat="server" AutoGenerateColumns="False" CssClass="custom-gridview">
                        <Columns>
                            <asp:BoundField DataField="NumeroDeRegistro" HeaderText="Número de Registro" />
                            <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                            <asp:BoundField DataField="NombreHangar" HeaderText="Hangar" />
                        </Columns>
                    </asp:GridView>
                        </div>
                </div>
            </div>
        </div>
    </div>
</div>


<div id="ModalDesembarque" class="modal">
    <div class="modal-content" style="width: 850px;"> 
        <span class="close" onclick="closeModalDesembarque()">&times;</span>
        
        <div class="card" style="width: 800px;"> 
            <div class="title">Lista de Embarque</div>
            <div class="details">
<div class="scrollable-table">
    <asp:GridView ID="gvEmbarque" runat="server" AutoGenerateColumns="False" CssClass="custom-gridview">
        <Columns>
            <asp:BoundField DataField="IDEmbarque" HeaderText="Embarque" />
            <asp:BoundField DataField="IDEquipaje" HeaderText="Equipaje" />
            <asp:BoundField DataField="NombreCompleto" HeaderText="Pasajero" />
            <%-- Añade más columnas según sea necesario --%>
        </Columns>
    </asp:GridView>
</div>


            </div>
            
        </div>
        <asp:UpdatePanel ID="updatePanel1" runat="server">
<ContentTemplate>
<div class="card" style="width: 800px;"> 
    <div class="title">Area de Desembarque</div>
    <div class="details">
        <asp:TextBox ID="txtDatosEmbarque" runat="server" CssClass="custom-textbox" placeholder="Ingresar datos de embarque" ClientIDMode="Static" style="width: 30%; padding: 8px; border: 1px solid #ccc; border-radius: 4px; box-sizing: border-box;"></asp:TextBox>
        <asp:DropDownList ID="ddlPuertas" runat="server" CssClass="custom-dropdownlist">
            <%-- Aquí se cargará la lista de puertas desde el código detrás --%>
        </asp:DropDownList>
<asp:Label ID="lblValoresSeleccionados" runat="server" Text=""></asp:Label>


<asp:Button ID="BtnEnviar" runat="server" Text="Desembarcar" CssClass="info-btn" OnClick="BtnEnviar_Click" UseSubmitBehavior="false" />

    </div>
</div>


</div>
    </ContentTemplate>
            </asp:UpdatePanel>
</div>
</div>


    <script>
        function openModalDesembarque() {
            var modal = document.getElementById("ModalDesembarque");
            modal.style.display = "block";
            return false; // Evita que el botón envíe el formulario
        }


    </script>
<script>
    function mostrarValores() {
        // Obtener el valor del TextBox
        var valorTextBox = document.getElementById('<%= txtDatosEmbarque.ClientID %>').value;

        // Obtener el valor seleccionado del DropDownList
        var ddl = document.getElementById('<%= ddlPuertas.ClientID %>');
        var valorDropDownList = ddl.options[ddl.selectedIndex].value;

        // Mostrar los valores en una alerta
        alert("Valor del TextBox: " + valorTextBox + "\nValor del DropDownList: " + valorDropDownList);
    }
</script>



    <script>
        function closeModalDesembarque() {
            var modal = document.getElementById("ModalDesembarque");
            modal.style.display = "none";
        }


        window.onclick = function (event) {
            var modal = document.getElementById("ModalDesembarque");
            if (event.target == modal) {
                modal.style.display = "none";
            }
        };
    </script>
<script>
    document.getElementById('openModalBtn').addEventListener('click', openModal);

    function openModal() {
        var modal = document.getElementById("desembarqueModal"); // Asegúrate de usar el ID correcto del modal de desembarque
        modal.style.display = "block";
    }

</script>
    <script>
        function loadPlaneInfo(hangarId) {

            document.getElementById('planeInfo').innerHTML = '<div class="title">Información del Avión en ' + hangarId + '</div>';
        }

        function openModal() {

            var modal = document.getElementById("myModal");
            modal.style.display = "block";
        }


        function closeModal() {

            var modal = document.getElementById("myModal");
            modal.style.display = "none";
        }

  
        window.onclick = function (event) {
            var modal = document.getElementById("myModal");
            if (event.target == modal) {
                modal.style.display = "none";
            }
        };


        function MostrarIdHangar(idHangar) {
            alert("ID del hangar: " + idHangar);
            return false; 
        }

        function mostrarDatos() {
            var nombreHangar = document.getElementById('txtNombreHangar').value;
            var numeroRegistroAvion = document.getElementById('txtNumeroRegistroAvion').value;
            alert("Nombre Hangar: " + nombreHangar + "\nNúmero de Registro Avión: " + numeroRegistroAvion);
        }
        
            function mostrarMensaje(mensaje) {
                alert(mensaje);
            return true;
    }
    

    </script>
<script>
    function CargarInformacionHangar(hangarData) {
        try {

            var hangar = JSON.parse(hangarData);


            document.getElementById("idTipoHangar").innerText = hangar.IdTipoHangar;
            document.getElementById("idHangar").innerText = hangar.IdHangar;
            document.getElementById("NombreHangar").innerText = hangar.Nombre;
            document.getElementById("ubicacionHangar").innerText = hangar.Ubicacion;
            document.getElementById("capacidadHangar").innerText = hangar.Capacidad;
            document.getElementById("estadoHangar").innerText = hangar.Estado;
            document.getElementById("tamañoHangar").innerText = hangar.Tamaño;
            document.getElementById("alturaHangar").innerText = hangar.Altura;

            var divInformacionHangar = document.getElementById("divInformacionHangar");
            if (divInformacionHangar) {
                divInformacionHangar.style.display = "block";
            } else {
                console.log("No se encontró el contenedor de información del hangar.");
            }
        } catch (ex) {

            console.error("Error al cargar la información del hangar: " + ex.message);
        }
    }


</script>




<style>
    .details p {
    margin-bottom: 10px; 
}

            .title-outside-container span {
        font-size: 24px; 
        font-weight: bold;
        color: #000000; 
        margin: 10px; 
        padding: 10px; 
    }


.mini-cards-container {
    display: flex;
    flex-wrap: wrap;
    justify-content: space-between; /* Cambiado a "space-between" para llenar el espacio horizontal */
}

.mini-card {
    width: calc(30% - 10px); /* Cambiado a 30% para ajustar mejor el espacio */
    height: auto;
    border: 1px solid #ccc;
    border-radius: 10px; /* Aumentado el radio de borde para hacerlo más atractivo */
    margin-bottom: 20px; /* Aumentado el espacio inferior */
    padding: 20px; /* Aumentado el espacio de relleno para mayor legibilidad */
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    background-color: #f9f9f9; /* Agregado un color de fondo para resaltar */
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); /* Agregado un sombreado sutil */
    transition: background-color 0.3s, transform 0.3s; /* Agregado efecto de transición */
}

.mini-card:hover {
    background-color: #084c85; /* Cambio de color al pasar el cursor */
    transform: translateY(-5px); /* Efecto de elevación al pasar el cursor */
}

.mini-card h3 {
    margin-bottom: 10px; /* Espaciado inferior adicional para el título */
}

.mini-card p {
    text-align: center; /* Alineación del texto al centro */
}


.info-btn { 
    color: white; 
    border: none;
    border-radius: 5px;
    padding: 5px 10px;
    cursor: pointer;
    margin-top: 5px;
    font-family: 'Roboto', sans-serif;
    background-color: #093A84;
}

.info-btn:hover {
    background-color: #0d63b0;
}
    .select-button:hover {
        background-color: #0d63b0;
    }
        .custom-gridview {
        border-collapse: collapse;
        width: 100%;
    }

    .custom-gridview th,
    .custom-gridview td {
        padding: 8px;
        text-align: left;
        border-bottom: 1px solid #ddd;
    }

    .custom-gridview th {
        background-color: #f2f2f2;
    }

    .custom-gridview tr:hover {
        background-color: #f2f2f2;
    }

    .custom-gridview tr:nth-child(even) {
        background-color: #f2f2f2;
    }
.container .card {
    border: 1px solid #ccc;
    border-radius: 5px;
    width: 20%;
    margin: 25px;
    padding: 20px;
    box-shadow: 2px 2px 6px 0px #ccc;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
}
.custom-dropdownlist {
  width: 20%;
  padding: 8px;
  margin-bottom: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
  background-color: #fff;
  color: #333;
  font-size: 14px;
  outline: none;
}

.custom-dropdownlist:hover,
.custom-dropdownlist:focus {
  border-color: #007bff;
}

.custom-dropdownlist option {
  background-color: #fff;
  color: #333;
  font-size: 14px;
}




.card-mediana {
    border: 1px solid #ccc;
    border-radius: 10px;
    background-color: #f9f9f9; /* Cambiado el color de fondo */
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    transition: box-shadow 0.3s ease;
    margin-bottom: 20px;
    padding: 20px; /* Agregado espacio de relleno */
}

.card-mediana:hover {
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
}

.details {
    padding: 10px; /* Ajustado el espacio de relleno */
}

.details p {
    margin: 10px 0;
    font-size: 16px;
    line-height: 1.5;
}

.details strong {
    font-weight: bold;
}

#idTipoHangar,
#idHangar,
#NombreHangar,
#ubicacionHangar,
#capacidadHangar,
#estadoHangar,
#tamañoHangar,
#alturaHangar {
    color: #333; /* Color del texto */
}








/* CSS */
.modal-content {
    background-color: #fefefe; /* Fondo del modal */
    margin: 15% auto; /* Centra vertical y horizontalmente */
    padding: 20px;
    border: 1px solid #888;
    width: 80%; /* Ancho del modal */
    max-width: 1000px; /* Ancho máximo */
    position: relative; /* Posición relativa para alinear el botón de cierre */
    border-radius: 10px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2); /* Sombra */
}

.close {
    position: absolute;
    right: 10px;
    top: 10px;
    color: #aaa;
    font-size: 28px;
    font-weight: bold;
    cursor: pointer;
}

.close:hover,
.close:focus {
    color: black;
    text-decoration: none;
    cursor: pointer;
}

.scrollable-table {
    max-height: 400px; /* Ajusta este valor según el tamaño de tus filas */
    overflow-y: auto;
    display: block;
}
.custom-gridview {
    width: 100%;
    border-collapse: collapse;
    font-family: Arial, sans-serif;
}

.custom-gridview th, .custom-gridview td {
    border: 1px solid #ddd;
    padding: 8px;
}

.custom-gridview th {
    padding-top: 12px;
    padding-bottom: 12px;
    text-align: left;
    background-color: #093A84;
    color: white;
}

.custom-gridview tr:nth-child(even){background-color: #f2f2f2;}

.custom-gridview tr:hover {background-color: #ddd;}








    </style>


</asp:Content>


