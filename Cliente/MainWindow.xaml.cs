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
using System.Net.Sockets;
using Cliente.Entidades;
using Newtonsoft.Json;

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

        public Citas LlenaClase()
        {
            Citas cita = new Citas();

            cita.CitasId = Convert.ToInt32(IdTextBox.Text);
            cita.Nombres = NombreTextBox.Text;
            cita.Apellidos = ApellidoTextBox.Text;
            cita.Telefono = TelefonoTextBox.Text;
            cita.Direccion = DireccionTextBox.Text;
            cita.FechaCita = FechaCitaTextBox.DisplayDate;
            cita.HoraCita = HoraCitaTextBox.Text;

            return cita;
        }

        public void LlenaCampo(Citas cita)
        {
            IdTextBox.Text = Convert.ToString(cita.CitasId);
            NombreTextBox.Text = cita.Nombres;
            ApellidoTextBox.Text = cita.Apellidos;
            TelefonoTextBox.Text = cita.Telefono;
            DireccionTextBox.Text = cita.Direccion;
            FechaCitaTextBox.SelectedDate = cita.FechaCita;
            HoraCitaTextBox.Text = cita.HoraCita;
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
            Citas cita = new Citas();
            IPEndPoint endP = new IPEndPoint(IPAddress.Parse(IpServidor), Port);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            byte[] messages = new byte[1024];

            socket.Connect(endP);

            messages = Encoding.UTF8.GetBytes("Hola, vine a buscar un cliente .....");

            socket.Send(messages);

            int bytemessages = 0;
            messages = new byte[1024];
            bytemessages = socket.Receive(messages);

            String mensajeRecibido = Encoding.UTF8.GetString(messages, 0, bytemessages);
            if (mensajeRecibido.Contains("aceptada"))
            {
                messages = new byte[1024];
                messages = Encoding.UTF8.GetBytes(IdTextBox.Text);

                socket.Send(messages);

                //Receive

                int Byte;
                messages = new byte[1024];
                Byte = socket.Receive(messages);
                string dato = string.Empty;

                dato += Encoding.UTF8.GetString(messages, 0, Byte);

                cita = JsonConvert.DeserializeObject<Citas>(dato);

                LlenaCampo(cita);
            }

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

        public void Modificar(int id){
            
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Citas citas;

            bool paso = false;
            //Validar
            citas = LlenaClase();

            if(Convert.ToInt32(IdTextBox.Text) == 0)
            {
                IPEndPoint endP = new IPEndPoint(IPAddress.Parse(IpServidor), Port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                byte[] messages = new byte[1024];

                socket.Connect(endP);

                messages = Encoding.UTF8.GetBytes("Hola, vine a guardar un cliente .....");

                socket.Send(messages);

                int bytemessages = 0;
                messages = new byte[1024];
                bytemessages = socket.Receive(messages);
                String mensajeRecibido = Encoding.UTF8.GetString(messages, 0, bytemessages);
                if (mensajeRecibido.Contains("aceptada"))
                {
                    string output = JsonConvert.SerializeObject(citas);
                    messages = Encoding.UTF8.GetBytes(output);
                    bytemessages = socket.Send(messages);
                }                
            }

            if (IdTextBox.Text != "0")
            {
                if (citas != null)
                {
                    Modificar(Convert.ToInt32(IdTextBox.Text));
                }
                
            }
            
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
