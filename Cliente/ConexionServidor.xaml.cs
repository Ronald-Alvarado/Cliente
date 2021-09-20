using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;

namespace Cliente
{
    /// <summary>
    /// Lógica de interacción para ConexionServidor.xaml
    /// </summary>
    public partial class ConexionServidor : Window
    {
        public ConexionServidor()
        {
            InitializeComponent();
            CenterWindowOnScreen();
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

        private bool Validar()
        {
            bool paso = false;
            IPAddress address;
            bool CONVERTIR = IPAddress.TryParse(ServerIpTextBox.Text, out address);

            bool Numerico = int.TryParse(PortTextBox.Text, out int port);

            if (String.IsNullOrEmpty(ServerIpTextBox.Text)) 
            {
                MessageBox.Show("Por que dejaste este campo vacio?, Escribe algo valido!!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                ServerIpTextBox.Focus();
                paso = false;

            }
            else if (String.IsNullOrEmpty(PortTextBox.Text))
            {
                MessageBox.Show("Por que dejaste este campo vacio?, Escribe algo valido!!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                PortTextBox.Focus();
                paso = false;
            }
            else if (CONVERTIR == false)
            {
                MessageBox.Show("Ahora no sabes poner una Ip?, Introduce una bien Inutil!!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                ServerIpTextBox.Focus();
                paso = false;
            }
            else if (Numerico == false)
            {
                MessageBox.Show("Ahora no sabes poner un Puerto? Introduce uno bien Inutil!!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                ServerIpTextBox.Focus();
                paso = false;
            }else
                paso = true;

            return paso;
        }


        private void ConectarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validar())
            {
                IPEndPoint ServerIp = new IPEndPoint(IPAddress.Parse(ServerIpTextBox.Text),Convert.ToInt32(PortTextBox.Text)) ;
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.Tcp);
                byte[] messages = new byte[1024];
                int bytemessages = 0;

                socket.Connect(ServerIp);

                bytemessages = socket.Receive(messages);

                string mensajeRecibido = Encoding.UTF8.GetString(messages, 0, bytemessages);

                MessageBox.Show(mensajeRecibido);
    

                if (mensajeRecibido.Contains("aceptada"))
                {
                    MainWindow main = new MainWindow(ServerIpTextBox.Text,Convert.ToInt32(PortTextBox.Text));
                    main.Show();
                    this.Close();
                }

                //25.4.175.48

               socket.Shutdown(SocketShutdown.Both);

                socket.Close();
            }


        }
    }
}
