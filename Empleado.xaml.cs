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
    /// Lógica de interacción para Empleado.xaml
    /// </summary>
    public partial class Empleado : Window
    {
        public Empleado()
        {
            InitializeComponent();
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

        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Horario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Manuales_Click_1(object sender, RoutedEventArgs e)
        {
            GridHome.Visibility = Visibility.Hidden;
            GridManuales.Visibility = Visibility.Visible;
        }

        private void Objetivos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnTabla_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnPromo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnComida_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
