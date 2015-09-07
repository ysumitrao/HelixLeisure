using HelixLeisureRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Web.Script.Serialization;

namespace HelixLeisureUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _webAPIUrl = ConfigurationManager.AppSettings["WebAPIUrl"];
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BindSalesList();
        }

        private void BindSalesList()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_webAPIUrl);
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/sales").Result;
            if (response.IsSuccessStatusCode)
            {
                var sales = response.Content.ReadAsAsync<IEnumerable<Sale>>().Result;
                grdSales.ItemsSource = sales;

            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        private void grdSalesRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            grdProducts.Visibility = Visibility.Hidden;
            string saleId = ((Sale)(((System.Windows.Controls.DataGridRow)(sender)).Item)).id;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_webAPIUrl);
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/sales/getproducts?saleId=" + saleId).Result;
            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsAsync<IEnumerable<Products>>().Result;
                grdProducts.ItemsSource = products;
                grdProducts.Visibility = Visibility.Visible;
                ProductSalesHeader.Visibility = Visibility.Visible;

            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_webAPIUrl);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Sale jsonObj = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Sale>(txtSalesJSONInput.Text);
                var response = client.PostAsJsonAsync("api/sales", jsonObj).Result;

                if (response.IsSuccessStatusCode)
                {
                    lblMessage.Content = "Sale Added Successfully.";
                    BindSalesList();
                }
                else
                {
                    lblMessage.Content = "Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase;
                }
            }
            catch (ArgumentException ex)
            {
                lblMessage.Content = ex.Message;
            }
            catch (Exception ex)
            {
                lblMessage.Content = ex.Message;
            }
        }
    }
}
