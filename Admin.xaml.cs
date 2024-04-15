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

        private void BtnHome_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AltaUsuariosGrid.Visibility = Visibility.Hidden;
            GridHome.Visibility = Visibility.Visible;
        }

        private void AltaUsuarios_Click(object sender, RoutedEventArgs e)
        {
            GridHome.Visibility = Visibility.Hidden;
            AltaUsuariosGrid.Visibility = Visibility.Visible;
        }

        private void CerrarSesion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
