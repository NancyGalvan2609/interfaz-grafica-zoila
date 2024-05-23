//using MySql.Data.MySqlClient;
using MySqlConnector;
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
    public class HorarioEmpleado
    {
        public int EmpleadoID { get; set; }
        public TimeSpan LunesInicio { get; set; }
        public TimeSpan LunesFin { get; set; }
        public TimeSpan MartesInicio { get; set; }
        public TimeSpan MartesFin { get; set; }
        public TimeSpan MiercolesInicio { get; set; }
        public TimeSpan MiercolesFin { get; set; }
        public TimeSpan JuevesInicio { get; set; }
        public TimeSpan JuevesFin { get; set; }
        public TimeSpan ViernesInicio { get; set; }
        public TimeSpan ViernesFin { get; set; }
        public TimeSpan SabadoInicio { get; set; }
        public TimeSpan SabadoFin { get; set; }
        public TimeSpan DomingoInicio { get; set; }
        public TimeSpan DomingoFin { get; set; }
    }



    /// <summary>
    /// Lógica de interacción para Empleado.xaml
    /// </summary>
    public partial class Empleado : Window
    {
        private int idEmpleado;

        public Empleado(int idEmpleado, string nombreEmpleado)
        {
            InitializeComponent();
            this.idEmpleado = idEmpleado;
            txtNombreEmpleado.Text = nombreEmpleado;
            UserName.Text = "Welcome " + nombreEmpleado + "!";
        }

        private void MostrarHorarioEmpleado()
        {
            // obtener los datos del horario del empleado desde la base de datos
            List<HorarioEmpleado> horarios = ObtenerHorarioEmpleadoDesdeBD(idEmpleado);

            // asigna los datos al control DataGrid
            dataGridHorario.ItemsSource = horarios;
        }

        private List<HorarioEmpleado> ObtenerHorarioEmpleadoDesdeBD(int idEmpleado)
        {
            List<HorarioEmpleado> horarios = new List<HorarioEmpleado>();

            string connectionString = "server=localhost;database=empresa;uid=root;password=root;";
            string query = "SELECT LunesInicio, LunesFin, " +
                           "MartesInicio, MartesFin, MiércolesInicio, MiércolesFin, " +
                           "JuevesInicio, JuevesFin, ViernesInicio, ViernesFin, " +
                           "SábadoInicio, SábadoFin, DomingoInicio, DomingoFin " +
                           "FROM Horarios WHERE EmpleadoID = @EmpleadoID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmpleadoID", idEmpleado);
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HorarioEmpleado horario = new HorarioEmpleado();
                            horario.EmpleadoID = idEmpleado;
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

        private double ObtenerSalarioPorHoraDesdeBD()
        {
            double salarioPorHora = 0.0;

            string connectionString = "server=localhost;database=empresa;uid=root;password=root;";
            string query = "SELECT Salario FROM Empleado WHERE ID = @EmpleadoID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmpleadoID", idEmpleado);
                    connection.Open();
                    salarioPorHora = Convert.ToDouble(command.ExecuteScalar());
                }
            }

            return salarioPorHora;
        }

        private double CalcularHorasTrabajadas()
        {
            double horasTrabajadas = 0.0;

            string connectionString = "server=localhost;database=empresa;uid=root;password=root;";
            string query = "SELECT LunesInicio, LunesFin, MartesInicio, MartesFin, " +
                           "MiércolesInicio, MiércolesFin, JuevesInicio, JuevesFin, " +
                           "ViernesInicio, ViernesFin, SábadoInicio, SábadoFin, " +
                           "DomingoInicio, DomingoFin FROM Horarios WHERE EmpleadoID = @EmpleadoID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmpleadoID", idEmpleado);
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Aquí obtenemos las horas de inicio y fin de cada día
                            TimeSpan lunesInicio = reader.GetTimeSpan("LunesInicio");
                            TimeSpan lunesFin = reader.GetTimeSpan("LunesFin");
                            TimeSpan martesInicio = reader.GetTimeSpan("MartesInicio");
                            TimeSpan martesFin = reader.GetTimeSpan("MartesFin");
                            TimeSpan miercolesInicio = reader.GetTimeSpan("MiércolesInicio");
                            TimeSpan miercolesFin = reader.GetTimeSpan("MiércolesFin");
                            TimeSpan juevesInicio = reader.GetTimeSpan("JuevesInicio");
                            TimeSpan juevesFin = reader.GetTimeSpan("JuevesFin");
                            TimeSpan viernesInicio = reader.GetTimeSpan("ViernesInicio");
                            TimeSpan viernesFin = reader.GetTimeSpan("ViernesFin");
                            TimeSpan sabadoInicio = reader.GetTimeSpan("SábadoInicio");
                            TimeSpan sabadoFin = reader.GetTimeSpan("SábadoFin");
                            TimeSpan domingoInicio = reader.GetTimeSpan("DomingoInicio");
                            TimeSpan domingoFin = reader.GetTimeSpan("DomingoFin");


                            // Calculamos las horas trabajadas para cada día
                            TimeSpan horasTrabajadasLunes = lunesFin - lunesInicio;
                            TimeSpan horasTrabajadasMartes = martesFin - martesInicio;
                            TimeSpan horasTrabajadasMiercoles = miercolesFin - miercolesInicio;
                            TimeSpan horasTrabajadasJueves = juevesFin - juevesInicio;
                            TimeSpan horasTrabajadasViernes = viernesFin - viernesInicio;
                            TimeSpan horasTrabajadasSabado = sabadoFin - sabadoInicio;
                            TimeSpan horasTrabajadasDomingo = domingoFin - domingoInicio;

                            // Sumamos las horas trabajadas de cada día
                            horasTrabajadas += horasTrabajadasLunes.TotalHours;
                            horasTrabajadas += horasTrabajadasMartes.TotalHours;
                            horasTrabajadas += horasTrabajadasMiercoles.TotalHours;
                            horasTrabajadas += horasTrabajadasJueves.TotalHours;
                            horasTrabajadas += horasTrabajadasViernes.TotalHours;
                            horasTrabajadas += horasTrabajadasSabado.TotalHours;
                            horasTrabajadas += horasTrabajadasDomingo.TotalHours;
                        }
                    }
                }
            }

            return horasTrabajadas;
        }

        private void CalcularNomina()
        {
            double salarioPorHora = ObtenerSalarioPorHoraDesdeBD();
            double horasTrabajadas = CalcularHorasTrabajadas();
      
            double neto = salarioPorHora * horasTrabajadas;
            double isr = CalcularISR(neto);

            double seguro = 100.00;
            double nomina = neto - (isr+seguro);
            

            txtTotalNeto.Text = "Sueldo neto: " + neto.ToString("C2"); // Formatear como moneda
            txtTotalDespuesISR.Text = "Sueldo despues de impuestos: " + nomina.ToString("C2");
            txtHorasTrabajadas.Text = horasTrabajadas.ToString() + " Horas trabajadas";
            txtDescuentoSeguro.Text = "Cuota IMSS: " + seguro.ToString("C2");

        }

        // Método CalcularISR adaptado para trabajar con valores double
        public double CalcularISR(double salarioNeto)
        {
            double isr = 0.0;

            
            if (salarioNeto <= 5000)
            {
                isr = salarioNeto * 0.1; // ISR del 10% para salarios menores o iguales a 5000
            }
            else if (salarioNeto <= 10000)
            {
                isr = salarioNeto * 0.15; // ISR del 15% para salarios mayores a 5000 y menores o iguales a 10000
            }
            else
            {
                isr = salarioNeto * 0.2; // ISR del 20% para salarios mayores a 10000
            }

            return isr;
        }



        private void CerrarSesion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void BtnHome_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GridManuales.Visibility = Visibility.Hidden;
            GridNominas.Visibility = Visibility.Hidden;
            GridHorario.Visibility = Visibility.Hidden;
            GridTDia.Visibility = Visibility.Hidden;
            GridPromo.Visibility = Visibility.Hidden;
            ImagenManuales();
            GridObjetivos.Visibility = Visibility.Hidden;
            GridInventario.Visibility = Visibility.Hidden;
            GridHome.Visibility = Visibility.Visible;
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        //Permite mover libremente por la pantalla
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Nomina_Click(object sender, RoutedEventArgs e)
        {
            GridHome.Visibility = Visibility.Hidden;
            GridNominas.Visibility = Visibility.Visible;
            CalcularNomina();

        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Horario_Click(object sender, RoutedEventArgs e)
        {
            GridHome.Visibility= Visibility.Hidden;
            GridHorario.Visibility= Visibility.Visible;
            MostrarHorarioEmpleado();
        }

        private void Manuales_Click_1(object sender, RoutedEventArgs e)
        {
            GridHome.Visibility = Visibility.Hidden;
            GridManuales.Visibility = Visibility.Visible;
        }

        private void Objetivos_Click(object sender, RoutedEventArgs e)
        {
            GridHome.Visibility = Visibility.Hidden;
            GridObjetivos.Visibility = Visibility.Visible;
        }

        private void BtnTabla_Click(object sender, RoutedEventArgs e)
        {
            GridHome.Visibility = Visibility.Hidden;
            GridTDia.Visibility = Visibility.Visible;
            ImgTbl1.Visibility = Visibility.Hidden;
            ImgTabl2.Visibility = Visibility.Hidden;
            ImgTablaD.Visibility = Visibility.Visible;
        }

        private void btnSemanalTD_Click(object sender, RoutedEventArgs e)
        {
            ImgTablaD.Visibility = Visibility.Hidden;
            ImgTabl2.Visibility = Visibility.Visible;
            ImgTbl1.Visibility = Visibility.Hidden;
        }

        private void BtnFinsemanaTD_Click(object sender, RoutedEventArgs e)
        {
            ImgTablaD.Visibility = Visibility.Hidden;
            ImgTbl1.Visibility = Visibility.Visible;
            ImgTabl2.Visibility = Visibility.Hidden;
        }

        private void BtnPromo_Click(object sender, RoutedEventArgs e)
        {
            GridHome.Visibility = Visibility.Hidden;
            GridPromo.Visibility = Visibility.Visible;
        }

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {
            GridHome.Visibility = Visibility.Hidden;
            GridInventario.Visibility = Visibility.Visible;
        }

        private void BtnComida_Click(object sender, RoutedEventArgs e)
        {
            ImagenManuales();
            MSandwich.Visibility = Visibility.Visible;   
        }

        private void BtnBebidasF_Click(object sender, RoutedEventArgs e)
        {
            ImagenManuales();
            mFrappe.Visibility = Visibility.Visible;
        }

        private void BtnBebidasC_Click(object sender, RoutedEventArgs e)
        {
            ImagenManuales();
            mCafeCaliente.Visibility = Visibility.Visible;
        }

        private void BtnPostres_Click(object sender, RoutedEventArgs e)
        {
            ImagenManuales();
            mPastel.Visibility = Visibility.Visible;
        }

        public void ImagenManuales()
        {
            mFrappe.Visibility = Visibility.Hidden;
            mCafeCaliente.Visibility = Visibility.Hidden;
            mPastel.Visibility = Visibility.Hidden;
            MSandwich.Visibility = Visibility.Hidden;
        }

        private void Flecha1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ImgPromos1.Visibility = Visibility.Hidden;
            ImgPromos2.Visibility = Visibility.Visible;
        }

        private void Flecha2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ImgPromos2.Visibility = Visibility.Hidden;
            ImgPromos1.Visibility = Visibility.Visible;
            
        }
    }
}
