using MySql.Data.MySqlClient;
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


namespace interfaz_grafica_zoila
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Btn_Minimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Btn_Login_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "server=localhost;database=empresa;uid=root;password=root;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Empleado WHERE Nombre = @nombre AND Contraseña = @contraseña";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@nombre", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@contraseña", txtPassword.Password);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        query = "SELECT Tipo FROM Empleado WHERE Nombre = @nombre";
                        cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@nombre", txtUsername.Text);

                        string tipoEmpleado = cmd.ExecuteScalar().ToString();

                        switch (tipoEmpleado)
                        {
                            case "General":
                                MessageBox.Show("Inicio de sesión exitoso como empleado general!");
                                Empleado ventanaEmpleado = new Empleado();
                                ventanaEmpleado.Show();
                                break;
                            case "Lider":
                                MessageBox.Show("Inicio de sesión exitoso como líder!");
                                Lider ventanaLider = new Lider();
                                ventanaLider.Show();
                                break;
                            case "Gerente":
                                MessageBox.Show("Inicio de sesión exitoso como gerente!");
                                Admin ventanaGerente = new Admin();
                                ventanaGerente.Show();
                                break;
                            default:
                                MessageBox.Show("Tipo de empleado no válido.");
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nombre de usuario o contraseña incorrectos");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar iniciar sesión: " + ex.Message);
                }
            }
        }

    }
}
