<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="testwebSkiing._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Test Skiing</h1>
        <p class="lead">Test Skiing</p>

    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Test</h2>
        </div>
        <div class="col-md-4">
            <asp:Button ID="Button1" class="btn btn-default" OnClick="Button1_Click" runat="server" Text="Calculate" />    
            <asp:Label ID="TextBox1" runat="server"></asp:Label>

        </div>

    </div>

</asp:Content>
