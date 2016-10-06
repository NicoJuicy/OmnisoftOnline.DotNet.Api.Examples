using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using Omnisoft.Api.Connector.Synchronous;

namespace WindowsForms
{
    public partial class Form1 : Form
    {

        string ApiUrl; 
        string ApiKey;

        Omnisoft.Api.Connector.Services.ClientService clientService;
        Omnisoft.Api.Connector.Services.InvoiceService invoiceService;
        Omnisoft.Api.Connector.Services.OrderService orderService;
        public Form1()
        {
            InitializeComponent();

            //init params
            ApiUrl = WindowsForms.Properties.Resources.ApiUrl;
            ApiKey = WindowsForms.Properties.Resources.ApiKey;

            //init services
            clientService = new Omnisoft.Api.Connector.Services.ClientService(ApiKey, ApiUrl);
            invoiceService = new Omnisoft.Api.Connector.Services.InvoiceService(ApiKey, ApiUrl);
            orderService = new Omnisoft.Api.Connector.Services.OrderService(ApiKey, ApiUrl);
        }

        #region panelSearchClients
      

        private void btnDoSearchClients_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearchClient.Text;

            //using the async method in an synchronious method == .Result
            try
            {

            var clients = clientService.AsyncList(
                10).Result;

                dtSearchedClientsDTO.DataSource = clients;

            }
            catch(Exception ex)
            {
                string t = "";
            }

            //string.Format("indexof(Naam,'{0}') gt -1  or indexof(Adres/AdresLijn1,'{0}') gt -1 or indexof(Adres/Gemeente,'{0}') gt -1 or Adres/PostCode eq 'tes' or indexof(Email,'{0}') gt -1 or indexof(RoepNaam,'{0}') gt -1"
            //var clients =  clientService.AsyncList(
            //    filterQuery: string.Format("indexof(Naam,'{0}') gt -1  or indexof(Adres/AdresLijn1,'{0}') gt -1 or indexof(Adres/Gemeente,'{0}') gt -1 or Adres/PostCode eq 'tes' or indexof(Email,'{0}') gt -1 or indexof(RoepNaam,'{0}') gt -1", searchTerm),
            //    orderQuery : "Naam desc").Result;

          
        }

        #endregion
    }
}
