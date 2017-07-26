<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="TarbucksWeb.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%">
    <tr>
        <td>
            <br /><br />
            <h3>Current Products</h3>
            <asp:GridView ID="CurrentProducts" CssClass="dataTable" runat="server"></asp:GridView>
        </td>
        <td>
            <h3>Best selling products</h3>
            <asp:GridView ID="BestSelling" CssClass="dataTable" runat="server">
            </asp:GridView>
            <h3>Combinations purchased in a single transaction</h3>
            <p>Select two different items to see how many customers have purchased these together on the same day</p>
            <asp:Button ID="Button2" runat="server" Text="Refresh Items" OnClick="Button2_Click" />
            <asp:DropDownList ID="Items1" runat="server"></asp:DropDownList>
            <asp:DropDownList ID="Items2" runat="server"></asp:DropDownList>
            <asp:Button ID="Button1" runat="server" Text="Query" OnClick="Button1_Click" />
            <asp:GridView ID="Combination" CssClass="dataTable" runat="server"></asp:GridView>
        </td> 
    </tr>
</table>
    
    
</asp:Content>
