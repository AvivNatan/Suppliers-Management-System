<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="SupplierManagerForm.aspx.cs" Inherits="SuppliersManagement.App_Form.SupplierManagerForm"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="styles.css"/>
    <title> Supplier Application</title>
</head>
<body>
    <form id="form1" runat="server"> 
        <h1>Supplier Details</h1>
        <div>
            <!-- Supplier ID -->
            <label for="txtId">Supplier ID:</label><br />
            <asp:TextBox ID="txtId" runat="server" Placeholder="Enter Supplier ID"></asp:TextBox><br /><br />
            <asp:RequiredFieldValidator
                ID="requiredValidatorId"
                runat="server"
                ControlToValidate="txtId"
                ErrorMessage="Supplier ID is required."
                Display="Dynamic"
                CssClass="validator-error">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator
                ID="regexValidatorId"
                runat="server"
                ControlToValidate="txtId"
                ErrorMessage="Supplier ID must be a valid number."
                ValidationExpression="^[0-9]+$"
                Display="Dynamic"
                CssClass="validator-error">
            </asp:RegularExpressionValidator><br /><br />

            <!-- Supplier Name -->
            <label for="txtName">Supplier Name:</label><br />
            <asp:TextBox ID="txtName" runat="server" Placeholder="Enter Supplier Name"></asp:TextBox><br /><br />
            <asp:RequiredFieldValidator
                ID="requiredValidatorName"
                runat="server"
                ControlToValidate="txtName"
                ErrorMessage="Supplier Name is required."
                Display="Dynamic"
                CssClass="validator-error">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator
                ID="regexValidatorName"
                runat="server"
                ControlToValidate="txtName"
                ErrorMessage="Supplier Name must contain only letters."
                ValidationExpression="^[a-zA-Z ]+$"
                Display="Dynamic"
                CssClass="validator-error">
            </asp:RegularExpressionValidator><br /><br />

            <!-- Manager Name -->
            <label for="txtManagerName">Manager Name:</label><br />
            <asp:TextBox ID="txtManagerName" runat="server" Placeholder="Enter Manager Name"></asp:TextBox><br /><br />
            <asp:RequiredFieldValidator
                ID="requiredValidatorManagerName"
                runat="server"
                ControlToValidate="txtManagerName"
                ErrorMessage="Manager Name is required."
                Display="Dynamic"
                CssClass="validator-error">  
             </asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator
                ID="regexValidatorManagerName"
                runat="server"
                ControlToValidate="txtManagerName"
                ErrorMessage="Manager Name must contain only letters."
                ValidationExpression="^[a-zA-Z ]+$"
                Display="Dynamic"
                CssClass="validator-error">
            </asp:RegularExpressionValidator><br /><br />

            <!-- Manager Phone Number -->
            <label for="txtManagerPhone">Manager Phone Number:</label><br />
            <asp:TextBox ID="txtManagerPhone" runat="server" Placeholder="Enter Manager Phone"></asp:TextBox><br /><br />
            <asp:RequiredFieldValidator
                ID="requiredValidatorPhone"
                runat="server"
                ControlToValidate="txtManagerPhone"
                ErrorMessage="Phone number is required."
                Display="Dynamic"
                CssClass="validator-error">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator
                ID="regexValidatorPhone"
                runat="server"
                ControlToValidate="txtManagerPhone"
                ErrorMessage="Phone number must be valid (e.g., 10+ digits)."
                ValidationExpression="^[0-9]{10,}$"
                Display="Dynamic"
                CssClass="validator-error">
            </asp:RegularExpressionValidator><br /><br />

            <!-- Supplier Creation Date -->
            <label for="calendarCreateDate">Creation Date:</label><br />
             <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Calendar
                                    CssClass="calendar"
                                    ID="calendarCreateDate"
                                    runat="server"></asp:Calendar>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="calendarCreateDate" EventName="SelectionChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <br /><br />

            <!-- Supplier Type -->
            <label for="supplierTypeList">Choose Supplier Type:</label><br />
            <asp:DropDownList ID="supplierTypeList" OnSelectedIndexChanged="supplierTypeList_SelectedIndexChanged" AutoPostBack="True" runat="server">
                <asp:ListItem Text="Hotel" Value="Hotel"></asp:ListItem>
                <asp:ListItem Text="Flight" Value="Flight"></asp:ListItem>
                <asp:ListItem Text="Attraction" Value="Attraction"></asp:ListItem>
            </asp:DropDownList><br /><br />

            <!-- Extra Details -->
            <asp:label ID="labelExtraDetails" for="txtExtraDetails" runat="server"></asp:label><br />
            <!-- Dropdown for Hotel/Flight -->
            <asp:DropDownList ID="ddlExtraDetails" runat="server" Visible="false"></asp:DropDownList><br />

            <!-- Textbox for Attraction -->
            <asp:TextBox ID="txtExtraDetails" runat="server" Visible="false"></asp:TextBox><br />

            <!-- Validation for Attraction -->
            <asp:RegularExpressionValidator
                ID="regexValidatorExtraDetails"
                runat="server"
                ControlToValidate="txtExtraDetails"
                Display="Dynamic"
                ErrorMessage="Invalid input for Max Tickets. Please enter a valid number."
                ValidationExpression="^[0-9]+$"
                Visible="false"
                CssClass="validator-error">
            </asp:RegularExpressionValidator><br />

            <!-- Save Button -->
            <asp:Button ID="buttonSave" runat="server" Text="Save" OnClick="buttonSave_Click" />
        </div>
    </form>
</body>
</html>