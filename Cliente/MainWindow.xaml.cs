using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Net;

namespace Cliente
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string IpServidor = "";
        int Port = 0;
       

        public MainWindow(string ServerIp, int port)
        {
            InitializeComponent();
            IpServidor = ServerIp;
            Port = port;
         
        }

        public MainWindow()
        {
            InitializeComponent();
            CenterWindowOnScreen();

            IdTextBox.Text = "0";
            FechaCitaTextBox.SelectedDate = DateTime.Now;
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            IdTextBox.Text = "0";
            NombreTextBox.Text = String.Empty;
            ApellidoTextBox.Text = String.Empty;
            TelefonoTextBox.Text = String.Empty;
            DireccionTextBox.Text = String.Empty;
            FechaCitaTextBox.SelectedDate = DateTime.Now;
            HoraCitaTextBox.Text = String.Empty;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
