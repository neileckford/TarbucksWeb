<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="TarbucksWeb.Customers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%; margin-top: 20px;">
    <tr>
        <td>
            <h3>All Customers</h3>
            <asp:GridView ID="AllCustomers" CssClass="dataTable" runat="server"></asp:GridView>
            <h3>Number of purchases per customer</h3>
            <asp:GridView ID="MostPurchases" CssClass="dataTable" runat="server"></asp:GridView>
        </td>
        <td style="vertical-align:top; padding-top: 40px;">
            <h4>Add Customer</h4>
            <form method="post" action="">
            <table style="width:100%">
            <tr>
                <td><asp:Label ID="Label1" runat="server" Text="Name:"></asp:Label></td>
                <td><asp:TextBox ID="txtCustName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label2" runat="server" Text="Address:"></asp:Label></td>
                <td><asp:TextBox ID="txtAddress" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label3" runat="server" Text="Phone No:"></asp:Label></td>
                <td><asp:TextBox ID="txtPhone" runat="server"></asp:TextBox></td>
            </tr>
      </table>
                <asp:Button ID="Button1" runat="server" Text="Add" OnClick="Button1_Click" />
                </form>
                <br /><br />
            
            <h4>Delete Customer</h4>
            <asp:DropDownList ID="RemoveCustomer" runat="server" OnSelectedIndexChanged="RemoveCustomer_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList><asp:Button ID="btnCustRemove" runat="server" Text="Refresh Customer List" OnClick="btnCustRemove_Click" /><br />
            <asp:GridView ID="CheckCustomer" CssClass="dataTable" runat="server"></asp:GridView>
            <asp:Button ID="btnDeleteCustomer" runat="server" Text="Delete Customer" Visible="False" OnClick="btnDeleteCustomer_Click" /><br />  
        </td>
    </tr>
</table>
    
</asp:Content>
