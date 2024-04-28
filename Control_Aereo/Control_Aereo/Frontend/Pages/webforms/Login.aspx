<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Frontend.Pages.webforms.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="../css/Login.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-wrap">
            <div class="login-html">

                <h1>Control Aereo</h1>
                <asp:LinkButton ID="lnkSIGN_IN" runat="server" OnClick="lnklnkSIGN_IN_Click" CssClass="styled-linkbutton" AutoPostBack="true" style="margin-right:15px;">Ingresar</asp:LinkButton>
                <asp:LinkButton ID="lnkSIGN_UP" runat="server" OnClick="lnklnkSIGN_UP_Click" CssClass="styled-linkbutton" AutoPostBack="true">Registrar</asp:LinkButton>

                <div class="login-form">

                    <div class="sign-in-form" style="margin-top: 20px;">
                        <div class="group">
                            <asp:Label ID="LabelUser" runat="server" Text="Usuario"></asp:Label>
                            <asp:TextBox ID="txbUser" runat="server" CssClass="input"></asp:TextBox>
                        </div>
                        <div class="group">
                            <asp:Label ID="LabelPassword" runat="server" Text="Contraseña"></asp:Label>
                            <asp:TextBox ID="txbPassword" runat="server" CssClass="input" TextMode="Password"></asp:TextBox>
                        </div>
                         <div class="group">
                            <asp:Button ID="btnsignIn" runat="server" CssClass="button" Text="Ingresar" OnClick="signInButton_Click" />
                        </div>
                    </div>

                    <div class="sign-up-html">
                        <div class="group">
                            <asp:Label ID="LabelNombre" runat="server" Text="Nombre"></asp:Label>
                            <asp:TextBox ID="txbNombre" runat="server" CssClass="input"></asp:TextBox>
                        </div>
                         <div class="group">
                            <asp:Label ID="LabelApellidos" runat="server" Text="Apellidos"></asp:Label>
                            <asp:TextBox ID="txbApellidos" runat="server" CssClass="input"></asp:TextBox>
                        </div>
                         <div class="group">
                            <asp:Label ID="LabelCedula" runat="server" Text="Cédula"></asp:Label>
                            <asp:TextBox ID="txbCedula" runat="server" CssClass="input"></asp:TextBox>
                        </div>
                        <div class="group">
                            <asp:Label ID="LabelUsuario" runat="server" Text="Usuario"></asp:Label>
                            <asp:TextBox ID="txbUsuario" runat="server" CssClass="input"></asp:TextBox>
                        </div>
                        <div class="group">
                            <asp:Label ID="LabelEmail" runat="server" Text="Correo electrónico"></asp:Label>
                            <asp:TextBox ID="txbEmail" runat="server" CssClass="input" TextMode="Email"></asp:TextBox>
                        </div>
                        <div class="group">
                            <asp:Label ID="LabelPassword_registro" runat="server" Text="Contraseña"></asp:Label>
                            <asp:TextBox ID="txbPassword_registro" runat="server" CssClass="input" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="group">
                            <asp:Label ID="LabelRol" runat="server" Text="Rol"></asp:Label>
                            <asp:DropDownList ID="DDL_Rol" runat="server" CssClass="input">
                                <asp:ListItem Value="Embarque">Embarque</asp:ListItem>
                                <asp:ListItem Value="Desembarque">Desembarque</asp:ListItem>
                                <asp:ListItem Value="Despegue">Despegue</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="group">
                            <asp:Button ID="btnUpButton" runat="server" CssClass="button" Text="Registrar" OnClick="signUpButton_Click" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </form>
    
</body>

</html>
