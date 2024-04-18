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
using System.Windows.Shapes;

namespace interfaz_grafica_zoila
{
    /// <summary>
    /// Lógica de interacción para Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
        }
        //Movimiento de la pantalla
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        //Minimizar la pantalla 
        private void MinimizeWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        //Cerrar pantalla
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //Cierra pantalla de admin y te refresa al login 
        private void CerrarSesion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        //Regresa al grid principal 
        private void BtnHome_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AltaUsuariosGrid.Visibility = Visibility.Hidden;
            GridHome.Visibility = Visibility.Visible;
        }

        private void AltaUsuarios_Click(object sender, RoutedEventArgs e)
        {
            GridHome.Visibility = Visibility.Hidden;
            AltaUsuariosGrid.Visibility = Visibility.Visible;
            TxtNombre.GotFocus += TxtNombre_GotFocus;
            TxtGenero.GotFocus += TxtGenero_GotFocus;
            TxtNombre.TextChanged += TxtNombre_TextChanged;
            TxtGenero.TextChanged += TxtGenero_TextChanged;
            if (string.IsNullOrWhiteSpace(TxtNombre.Text))
            {
                TxtNombre.Text = "*Nombre:";
            }
            if (string.IsNullOrWhiteSpace(TxtGenero.Text))
            {
                TxtGenero.Text = "*Genero (F/M)";
            }
        }


        private void TxtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TxtGenero.Focus();
            }
        }

        private void Genero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnGuardarInfoNewUsuario_Click(sender, e);
            }
        }


        private void BtnGuardarInfoNewUsuario_Click(object sender, RoutedEventArgs e)
        {
            try
            {   
                string genero = TxtGenero.Text;

                if (comboBoxOpciones.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, seleccione el tipo de empleado.");
                    return;
                }

                // Obtener el tipo de empleado seleccionado del ComboBox
                string tipoEmpleado = ((ComboBoxItem)comboBoxOpciones.SelectedItem).Content.ToString();

                if (genero != "Masculino" && genero != "Femenino")
                {
                    MessageBox.Show("El valor del género debe ser 'Masculino' o 'Femenino'.");
                    return; 
                }
                // Generar una contraseña aleatoria
                string password = GenerarContraseñaAleatoria();
                // Conectar a la base de datos
                string connectionString = "server=localhost;database=empresa;uid=root;password=root;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Insertar los datos del nuevo usuario en la tabla Empleado
                    string query = "INSERT INTO Empleado (Nombre, Genero, Contraseña, Tipo) VALUES (@nombre, @genero, @contraseña, @tipo)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@nombre", TxtNombre.Text);
                    cmd.Parameters.AddWithValue("@genero", genero);
                    cmd.Parameters.AddWithValue("@contraseña", password);
                    cmd.Parameters.AddWithValue("@tipo", tipoEmpleado);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Se ha guardado la información del nuevo usuario.\nContraseña generada: " + password);
                TxtNombre.Text = string.Empty;
                TxtGenero.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar guardar la información del nuevo usuario: " + ex.Message);
            }
        }

        // Método para generar una contraseña aleatoria
        private string GenerarContraseñaAleatoria()
        {
            const string caracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Random random = new Random();
            string contraseña = new string(Enumerable.Repeat(caracteresPermitidos, 8).Select(s => s[random.Next(s.Length)]).ToArray());
            return contraseña;
        }

        private void TxtNombre_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtNombre.Text = string.Empty;
        }

        private void TxtGenero_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtGenero.Text = string.Empty;
        }


        private void TxtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "*Nombre:")
            {
                textBox.Text = string.Empty;
            }
        }

        private void TxtGenero_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "*Genero (F/M)")
            {
                textBox.Text = string.Empty;
            }
        }


      



        /*private void TxtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Verificar si ambas TextBox tienen texto
            if (!string.IsNullOrEmpty(TxtNombre.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                // Si ambas están llenas, aumentar la opacidad del botón a 1 (completamente visible)
                BtnGuardarInfoNewUsuario.Opacity = 1.0;
            }
            else
            {
                // Si alguna está vacía, reducir la opacidad del botón a 0.5 (semi-transparente)
                BtnGuardarInfoNewUsuario.Opacity = 0.5;
            }
        }*/

    }
}
