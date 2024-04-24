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

namespace CarRental_desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += (sender, args) =>
            {
                try
                {
                    Statisztika.Beolvasas();
                    grid.ItemsSource = Statisztika.cars;
                }
                catch
                {
                    MessageBox.Show("Adatbázis hiba","Hiba",MessageBoxButton.OK);
                    this.Close();
                }
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Car selected = grid.SelectedItem as Car;
            if(selected == null)
            {
                MessageBox.Show("A törléshez vélasszon ki egy elemet", "Hiba", MessageBoxButton.OK);
            }
            else
            {
                MessageBoxResult delete = MessageBox.Show("Biztos törölni akarja ezt az autót?\n" + selected.Brand + " - " + selected.Model, "Törlés", MessageBoxButton.YesNo);
                if(delete == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=probavizsga;Uid=root;Pwd=;"))
                        {
                            conn.Open();

                            string sql = "DELETE FROM cars WHERE id = @carId";

                            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                            {
                                cmd.Parameters.AddWithValue("@carId", selected.Id);
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Sikeres törlés","Törlés",MessageBoxButton.OK);
                                }
                                else
                                {
                                    MessageBox.Show("Nincs ilyen autó", "Hiba", MessageBoxButton.OK);
                                }
                            }
                        }
                        Statisztika.Beolvasas();
                        grid.ItemsSource = null;
                        grid.ItemsSource = Statisztika.cars;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }
    }
}
