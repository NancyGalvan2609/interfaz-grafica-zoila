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

        private List<HorarioEmpleado> ObtenerHorarioEmpleadoDesdeBD()
        {
            List<HorarioEmpleado> horarios = new List<HorarioEmpleado>();

            string connectionString = "server=localhost;database=empresa;uid=root;password=root;";
            string query = "SELECT EmpleadoID, LunesInicio, LunesFin, " +
                           "MartesInicio, MartesFin, MiércolesInicio, MiércolesFin, " +
                           "JuevesInicio, JuevesFin, ViernesInicio, ViernesFin, " +
                           "SábadoInicio, SábadoFin, DomingoInicio, DomingoFin " +
                           "FROM Horarios";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HorarioEmpleado horario = new HorarioEmpleado();
                            horario.EmpleadoID = reader.GetInt32("EmpleadoID");
                            horario.LunesInicio = reader.GetTimeSpan("LunesInicio");
                            horario.LunesFin = reader.GetTimeSpan("LunesFin");
                            horario.MartesInicio = reader.GetTimeSpan("MartesInicio");
                            horario.MartesFin = reader.GetTimeSpan("MartesFin");
                            horario.MiercolesInicio = reader.GetTimeSpan("MiércolesInicio");
                            horario.MiercolesFin = reader.GetTimeSpan("MiércolesFin");
                            horario.JuevesInicio = reader.GetTimeSpan("JuevesInicio");
                            horario.JuevesFin = reader.GetTimeSpan("JuevesFin");
                            horario.ViernesInicio = reader.GetTimeSpan("ViernesInicio");
                            horario.ViernesFin = reader.GetTimeSpan("ViernesFin");
                            horario.SabadoInicio = reader.GetTimeSpan("SábadoInicio");
                            horario.SabadoFin = reader.GetTimeSpan("SábadoFin");
                            horario.DomingoInicio = reader.GetTimeSpan("DomingoInicio");
                            horario.DomingoFin = reader.GetTimeSpan("DomingoFin");

                            horarios.Add(horario);
                        }
                    }
                }
            }

            return horarios;
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
            GridHorario.Visibility = Visibility.Hidden;
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

                double salarioPorHora;
                switch (tipoEmpleado)
                {
                    case "General":
                        salarioPorHora = 41.50; // Salario para empleados generales
                        break;
                    case "Lider":
                        salarioPorHora = 65.50; // Salario para líderes
                        break;
                    default:
                        salarioPorHora = 0; // Establecer un valor predeterminado o manejar el caso por defecto según sea necesario
                        break;
                }

                // Generar una contraseña aleatoria
                string password = GenerarContraseñaAleatoria();
                // Conectar a la base de datos
                string connectionString = "server=localhost;database=empresa;uid=root;password=root;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Insertar los datos del nuevo usuario en la tabla Empleado
                    string query = "INSERT INTO Empleado (Nombre, Genero, Salario, Contraseña, Tipo) VALUES (@nombre, @genero, @salario ,@contraseña, @tipo)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@nombre", TxtNombre.Text);
                    cmd.Parameters.AddWithValue("@genero", genero);
                    cmd.Parameters.AddWithValue("@salario", salarioPorHora);
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


        private void BtnReporte_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnHorarios_Click(object sender, RoutedEventArgs e)
        {
            GridHome.Visibility = Visibility.Hidden; 
            GridHorario.Visibility = Visibility.Visible;
        }

        private void BtnMostrarHrs_Click(object sender, RoutedEventArgs e)
        {
            StackGenerarHrs.Visibility = Visibility.Hidden;
            StackMostrarHrs.Visibility = Visibility.Visible;
            ObtenerHorarioEmpleadoDesdeBD();
        }

        private void BtnGenerarHrs_Click(object sender, RoutedEventArgs e)
        {
            StackMostrarHrs.Visibility = Visibility.Hidden;
            StackGenerarHrs.Visibility = Visibility.Visible;
        }


        private void BtnGuardarHoras_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obtener el ID del empleado
                int idEmpleado;
                if (!int.TryParse(TxtNombreHr.Text, out idEmpleado))
                {
                    MessageBox.Show("Por favor, ingrese el ID válido del empleado.");
                    return;
                }


                // Crear una conexión a la base de datos
                string connectionString = "server=localhost;database=empresa;uid=root;password=root;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Guardar las horas ingresadas para cada día de la semana en la base de datos
          
                    try
                    {
                        string query = @"INSERT INTO Horarios (EmpleadoID, LunesInicio, LunesFin, MartesInicio, MartesFin, MiércolesInicio, MiércolesFin, JuevesInicio, JuevesFin, ViernesInicio, ViernesFin, SábadoInicio, SábadoFin, DomingoInicio, DomingoFin) 
                     VALUES (@EmpleadoID, @inicioLunes, @finLunes, @inicioMartes, @finMartes, @inicioMiércoles, @finMiércoles, @inicioJueves, @finJueves, @inicioViernes, @finViernes, @inicioSábado, @finSábado, @inicioDomingo, @finDomingo)";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@EmpleadoID", idEmpleado);
                        cmd.Parameters.AddWithValue("@inicioLunes", TxtInicioLunes.Text);
                        cmd.Parameters.AddWithValue("@finLunes", TxtFinLunes.Text);
                        cmd.Parameters.AddWithValue("@inicioMartes", TxtInicioMartes.Text);
                        cmd.Parameters.AddWithValue("@finMartes", TxtFinMartes.Text);
                        cmd.Parameters.AddWithValue("@inicioMiércoles", TxtInicioMiercoles.Text);
                        cmd.Parameters.AddWithValue("@finMiércoles", TxtFinMiercoles.Text);
                        cmd.Parameters.AddWithValue("@inicioJueves", TxtInicioJueves.Text);
                        cmd.Parameters.AddWithValue("@finJueves", TxtFinJueves.Text);
                        cmd.Parameters.AddWithValue("@inicioViernes", TxtInicioViernes.Text);
                        cmd.Parameters.AddWithValue("@finViernes", TxtFinViernes.Text);
                        cmd.Parameters.AddWithValue("@inicioSábado", TxtInicioSabado.Text);
                        cmd.Parameters.AddWithValue("@finSábado", TxtFinSabado.Text);
                        cmd.Parameters.AddWithValue("@inicioDomingo", TxtInicioDomingo.Text);
                        cmd.Parameters.AddWithValue("@finDomingo", TxtFinDomingo.Text);
                        cmd.ExecuteNonQuery();
                    }
                    catch (MySqlException ex)
                    {
                        if (ex.Number == 1062)
                        {
                            string updateQuery = @"UPDATE Horarios 
                               SET LunesInicio = @inicioLunes, LunesFin = @finLunes,
                                   MartesInicio = @inicioMartes, MartesFin = @finMartes,
                                   MiércolesInicio = @inicioMiércoles, MiércolesFin = @finMiércoles,
                                   JuevesInicio = @inicioJueves, JuevesFin = @finJueves,
                                   ViernesInicio = @inicioViernes, ViernesFin = @finViernes,
                                   SábadoInicio = @inicioSábado, SábadoFin = @finSábado,
                                   DomingoInicio = @inicioDomingo, DomingoFin = @finDomingo
                               WHERE EmpleadoID = @EmpleadoID";
                            MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection);
                            updateCmd.Parameters.AddWithValue("@EmpleadoID", idEmpleado);
                            updateCmd.Parameters.AddWithValue("@inicioLunes", TxtInicioLunes.Text);
                            updateCmd.Parameters.AddWithValue("@finLunes", TxtFinLunes.Text);
                            updateCmd.Parameters.AddWithValue("@inicioMartes", TxtInicioMartes.Text);
                            updateCmd.Parameters.AddWithValue("@finMartes", TxtFinMartes.Text);
                            updateCmd.Parameters.AddWithValue("@inicioMiércoles", TxtInicioMiercoles.Text);
                            updateCmd.Parameters.AddWithValue("@finMiércoles", TxtFinMiercoles.Text);
                            updateCmd.Parameters.AddWithValue("@inicioJueves", TxtInicioJueves.Text);
                            updateCmd.Parameters.AddWithValue("@finJueves", TxtFinJueves.Text);
                            updateCmd.Parameters.AddWithValue("@inicioViernes", TxtInicioViernes.Text);
                            updateCmd.Parameters.AddWithValue("@finViernes", TxtFinViernes.Text);
                            updateCmd.Parameters.AddWithValue("@inicioSábado", TxtInicioSabado.Text);
                            updateCmd.Parameters.AddWithValue("@finSábado", TxtFinSabado.Text);
                            updateCmd.Parameters.AddWithValue("@inicioDomingo", TxtInicioDomingo.Text);
                            updateCmd.Parameters.AddWithValue("@finDomingo", TxtFinDomingo.Text);
                            updateCmd.ExecuteNonQuery();
                        }
                        else
                        {
                            // Manejar otros errores...
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar guardar las horas: " + ex.Message);
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
