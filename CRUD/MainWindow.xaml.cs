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
using System.Data.SqlClient;
using System.Data;

namespace CRUD
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadGrid();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-IOLA8FO;Initial Catalog=NewDB;Integrated Security=True");

        public void clearData()
        {
            name_txt.Clear();
            age_txt.Clear();
            gender_txt.Clear();
            city_txt.Clear();
            search_txt.Clear();

        }
        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * from FirstTable", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            datagrid.ItemsSource = dt.DefaultView;

        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ClearDataBtn_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }
        public bool isValid()
        {
            if (name_txt.Text == string.Empty)
            {
                MessageBox.Show("Требуется указать имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (age_txt.Text == string.Empty)
            {
                MessageBox.Show("Требуется указать имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (gender_txt.Text == string.Empty)
            {
                MessageBox.Show("Требуется указать имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (city_txt.Text == string.Empty)
            {
                MessageBox.Show("Требуется указать имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                try
                {
                    if (isValid())
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO FirstTable VALUES (@Name, @Age, @Gender, @City)", con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Name", name_txt.Text);
                        cmd.Parameters.AddWithValue("@Age", age_txt.Text);
                        cmd.Parameters.AddWithValue("@Gender", gender_txt.Text);
                        cmd.Parameters.AddWithValue("@City", city_txt.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        LoadGrid();
                        MessageBox.Show("Вы успешно зарегистрировались", "Сохранено", MessageBoxButton.OK, MessageBoxImage.Information);
                        clearData();

                    }
                }
                catch (SqlException ex)

                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from FirstTable where ID = " + search_txt.Text + " ", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("ID Пользователя полностью удалено  ", "Удалено", MessageBoxButton.OK, MessageBoxImage.Information);
                con.Close();
                clearData();
                LoadGrid();
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not Deleted " + ex.Message);
            }
            finally
            {
                con.Close();
            }


        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update FirstTable set Name = '" + name_txt.Text + "', Gender = '" + gender_txt.Text + "', City = '" + city_txt.Text + "' WHERE ID = '" + search_txt.Text + "' ", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been updated succesfuly", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                clearData();
                LoadGrid();
            }
        }
    }
}
        
    