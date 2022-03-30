<%@ Page Language="C#"  MasterPageFile="~/Vista/vMasterModulo.Master" AutoEventWireup="true" CodeBehind="vModulo.aspx.cs" Inherits="Web_PersonasFisicas.Vista.vModulo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphReporte" runat="server">
    <asp:Panel ID="Panel1" runat="server" Visible="true" CssClass="container-fluid">
        <div class="row">
            <div class="col-sm-4" style="text-align:justify">
                <asp:Button ID="btnInsertar" runat="server" Text="Insertar" CssClass="btn btn-success btn-sm" OnClick="btnInsertar_Click"/>
            </div>
            <div class="col-sm-4" style="text-align:justify">
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="btn btn-success btn-sm" OnClick="btnConsultar_Click"/>
            </div>
            <div class="col-sm-4" style="text-align:justify">
                <asp:Button ID="btnReporte" runat="server" Text="Reportes" CssClass="btn btn-success btn-sm" OnClick="btnReporte_Click"/>
            </div>
        </div>
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlCrud" runat="server" CssClass="container-fluid" ScrollBars="Auto" Visible="true">
        <div id="dvInsertar" runat="server" visible="false">            
            <table  width="100%" align="center" >
                <tr style="background-color: #092E86 ;color: white; height:25px; font-weight: bold;"><td colspan="6" class="auto-style2" align="center">Datos</td> </tr>
                <tr>
                    <td><asp:Label  runat="server" Text="Nombre:" ForeColor="CornflowerBlue" Font-Bold="true"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNombre" runat="server"  MaxLength="50" CssClass="form-control texto-espacio" ></asp:TextBox>
                    </td>
                    <td><asp:Label  runat="server" Text="Apellido Paterno:" ForeColor="CornflowerBlue" Font-Bold="true"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtApPaterno" runat="server"  MaxLength="50" CssClass="form-control texto-espacio" ></asp:TextBox>
                    </td>
                    <td><asp:Label  runat="server" Text="ApellidoMaterno:" ForeColor="CornflowerBlue" Font-Bold="true"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtApMaterno" runat="server"  MaxLength="50" CssClass="form-control texto-espacio" ></asp:TextBox>
                    </td>
                    
                </tr>
                <tr>
                    <td><asp:Label  runat="server" Text="RFC:" ForeColor="CornflowerBlue" Font-Bold="true"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtRFC" runat="server"  MaxLength="13" Width="100%"  CssClass="form-control texto-espacio" ></asp:TextBox>
                    </td>
                    <td><asp:Label  runat="server" Text="Fecha Nacimiento:" ForeColor="CornflowerBlue" Font-Bold="true"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtFechaNac" runat="server"  MaxLength="10" Width="100%"  CssClass="form-control texto-espacio" ></asp:TextBox>
                        <asp:CalendarExtender runat="server" BehaviorID="TextBox1_CalendarExtender" TargetControlID="txtFechaNac" ID="TextBox1_CalendarExtender" Format="dd/MM/yyyy"/>
                          <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server"  TargetControlID="txtFechaNac" Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"/>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;
                    </td>
                    
                </tr>
                <tr>
                    <td colspan="6" class="auto-style2" align="center">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-primary" OnClick="btnGuardar_Click"/>
                    </td>
                </tr>
            </table>
        </div>
        <div id="dvConsultar" runat="server" Visible ="true">            
            <asp:GridView ID="gvInforme" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="IdPersonaFisica,Nombre,ApellidoPaterno,ApellidoMaterno,RFC,FechaNacimiento" CellPadding="3" ForeColor="#333333" PageSize="20"  
                Visible="true" OnPageIndexChanging="gvInforme_PageIndexChanging" OnRowCommand="gvInforme_RowCommand" Width="100%">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="IdPersonaFisica" HeaderText="IdPersonaFisica" SortExpression="IdPersonaFisica" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Visible="false"/>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>                             
                                <asp:BoundField DataField="ApellidoPaterno" HeaderText="Apellido Paterno" SortExpression="ApellidoPaterno" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="ApellidoMaterno" HeaderText="Apellido Materno" SortExpression="ApellidoMaterno" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="RFC" HeaderText="RFC" SortExpression="RFC" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha Nacimiento" SortExpression="FechaNacimiento" HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>                                
                                        <asp:LinkButton runat="server" ID="btnActualizar" class="btn btn-success btn-sm" CommandName="Actualizar" CommandArgument='<%# Eval("IdPersonaFisica") %>'>Actualizar</asp:LinkButton>    
                                    </ItemTemplate>                           
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>                                
                                        <asp:LinkButton runat="server" ID="btnEliminar" class="btn btn-success btn-sm" CommandName="Eliminar" CommandArgument='<%# Eval("IdPersonaFisica") %>'>Eliminar</asp:LinkButton>    
                                    </ItemTemplate>                           
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                     </asp:GridView>
        </div>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cont" runat="server">

<asp:ScriptManager ID="script" runat="server" EnablePageMethods="true"></asp:ScriptManager>                                 

    <asp:HiddenField runat="server" ID="hIdPersonaFisica" Value="0" />

</asp:Content>

