using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace SuppliersManagement.App_Form
{
    public partial class SupplierManagerForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateExtraDetailsField();
        }
        protected void supplierTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateExtraDetailsField();
        }
        protected async void buttonSave_Click(object sender, EventArgs e)
        {

            string extraDetails = GetExtraDetailsBasedOnSupplierType(supplierTypeList.SelectedValue);

            if (!Page.IsValid)
            {
                Response.Write("<script>alert('Invalid input')</script>");
                ClearForm();
                return;
            }
            DtoSupplierDetails dtoSupplierDetails = BuildSupplier(extraDetails);

            try
            {
                string result = await SendSupplierToWebService(dtoSupplierDetails);
                Response.Write($"<div style='color: green; font-weight: bold;'>Web Service Response: {result}</div>");
                ClearForm();
            }
            catch (Exception ex)
            {
                Response.Write($"<div style='color: red; font-weight: bold;'>Error: {ex.Message}</div>");
            }

        }
        private void UpdateExtraDetailsField()
        {
            string supplierType = supplierTypeList.SelectedValue;

            ddlExtraDetails.Visible = false;
            txtExtraDetails.Visible = false;
            regexValidatorExtraDetails.Visible = false;
            UpdateFieldBasedOnSupplierType(supplierType);
        }
        private void UpdateFieldBasedOnSupplierType(string supplierType)
        {
            switch (supplierType)
            {
                case "Hotel":
                    labelExtraDetails.Text = "Chain Name:";
                    // Populate dropdown for Hotel
                    ddlExtraDetails.Items.Clear();
                    ddlExtraDetails.Items.Add(new ListItem("Isrotel", "Isrotel"));
                    ddlExtraDetails.Items.Add(new ListItem("Dan", "Dan"));
                    ddlExtraDetails.Items.Add(new ListItem("Fatal", "Fatal"));
                    ddlExtraDetails.Visible = true;
                    break;

                case "Flight":
                    labelExtraDetails.Text = "Carrier Name:";

                    // Populate dropdown for Flight
                    ddlExtraDetails.Items.Clear();
                    ddlExtraDetails.Items.Add(new ListItem("El-Al", "El-Al"));
                    ddlExtraDetails.Items.Add(new ListItem("Arkia", "Arkia"));
                    ddlExtraDetails.Items.Add(new ListItem("Turkish Airlines", "Turkish Airlines"));
                    ddlExtraDetails.Visible = true;
                    break;
                case "Attraction":
                    labelExtraDetails.Text = "Max Tickets Allowed:";
                    txtExtraDetails.Visible = true;
                    regexValidatorExtraDetails.Visible = true;
                    break;
            }
        }
        private string GetExtraDetailsBasedOnSupplierType(string supplierType)
        {
            string extraDetails = null;

            if (supplierType == "Hotel" || supplierType == "Flight")
            {
                extraDetails = ddlExtraDetails.SelectedValue;
            }
            else if (supplierType == "Attraction")
            {
                extraDetails = txtExtraDetails.Text.Trim();
            }
            return extraDetails;
        }
        private DtoSupplierDetails BuildSupplier(string extraDetails)
        {
            DtoSupplierDetails dtoSupplierDetails = new DtoSupplierDetails
            {
                Id = txtId.Text,
                Name = txtName.Text,
                ManagerName = txtManagerName.Text,
                ManagerPhoneNumber = txtManagerPhone.Text,
                CreateDate = calendarCreateDate.SelectedDate.ToShortDateString(),
                SupplierType = supplierTypeList.SelectedValue,
                ExtraDetails = extraDetails
            };
            return dtoSupplierDetails;
        }
        private void ClearForm()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtManagerName.Text = "";
            txtManagerPhone.Text = "";
            calendarCreateDate.SelectedDate = DateTime.Now;
            txtExtraDetails.Text = "";
            supplierTypeList.SelectedIndex = 0;
        }
        private async Task<string> SendSupplierToWebService(DtoSupplierDetails supplier)
        {
            try
            {
                // Create the SOAP XML with CandidateFullName and SupplierDetails
                XElement soapXml = CreateSoapXmlFromSupplier(supplier);

                Console.WriteLine("Generated SOAP XML: " + soapXml.ToString());

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));

                    client.DefaultRequestHeaders.Add("SOAPAction", "http://tempuri.org/SendSuppliersDetails");

                    StringContent content = new StringContent(soapXml.ToString(), Encoding.UTF8, "text/xml");
                    HttpResponseMessage response = await client.PostAsync("http://web27.agency2000.co.il/Test/TestService.asmx", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        throw new Exception($"HTTP POST failed: {response.StatusCode}, Response: {responseContent}");
                    }

                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
        }
        private XElement CreateSoapXmlFromSupplier(DtoSupplierDetails supplier)
        {
            // Define namespaces
            XNamespace soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
            XNamespace web = "http://tempuri.org/";

            // Create the SupplierDetails XML (assuming you have a single supplier to send)
            XElement supplierDetailsXml = new XElement(web + "SupplierDetails",
                new XElement(web + "Id", supplier.Id),
                new XElement(web + "Name", supplier.Name),
                new XElement(web + "ManagerName", supplier.ManagerName),
                new XElement(web + "ManagerPhoneNumber", supplier.ManagerPhoneNumber),
                new XElement(web + "CreateDate", supplier.CreateDate),
                new XElement(web + "SupplierType", supplier.SupplierType),
                new XElement(web + "ExtraDetails", supplier.ExtraDetails)
            );

            // Wrap the supplier details in a list (even if you have one supplier, it should be in a list)
            XElement resultXml = new XElement(soapenv + "Envelope",
                new XAttribute(XNamespace.Xmlns + "soapenv", soapenv),
                new XAttribute(XNamespace.Xmlns + "web", web),
                new XElement(soapenv + "Header"),
                new XElement(soapenv + "Body",
                    new XElement(web + "SendSuppliersDetails",
                        new XElement(web + "CandidateFullName", "Aviv Natan"), // Your name
                        new XElement(web + "SupplierDetails",
                            supplierDetailsXml // Send the supplier details list (even if it's just one)
                        )
                    )
                )
            );

            return resultXml;
        }

    }
}