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
        //Cerrar panralla
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

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
            TxtNombre.GotFocus += TxtGotFocus;
        }

        private void TxtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox textBox = (TextBox)sender;
            }
        }

        private void BtnGuardarInfoNewUsuario_Click(object sender, RoutedEventArgs e)
        {  
            MessageBox.Show("oki-doki");
            

        }

       private void TxtGotFocus(object sender, RoutedEventArgs e)
        {
            TxtNombre.Clear();
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
