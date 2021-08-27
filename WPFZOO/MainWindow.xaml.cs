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
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace WPFZOO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection sqlConnection;
        private object AssociatedAnimalList;

        public MainWindow()
        {
            InitializeComponent();



            string connectionString = ConfigurationManager.ConnectionStrings["WPFZOO.Properties.Settings.UdemyDBConnectionString"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);

            ShowZoos();
            ShowAnimals();


        }

        private void ShowZoos()
        {
            try
            {
                string query = "select * from Zoo";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);

                using (sqlDataAdapter)
                {
                    DataTable zooTable = new DataTable();
                    sqlDataAdapter.Fill(zooTable);

                    listZoos.DisplayMemberPath = "Location";
                    listZoos.SelectedValuePath = "Id";
                    listZoos.ItemsSource = zooTable.DefaultView;


                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void ShowAssociatedAnimals()
        {
            try
            {
                string query = "select * from Animal a inner join ZooAnimal za on a.id = za.AnimalId " +
                              " where za.ZooId = @ZooID";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                using (sqlDataAdapter)
                {

                    sqlCommand.Parameters.AddWithValue("@ZooID", listZoos.SelectedValue);


                    DataTable animalTable = new DataTable();
                    sqlDataAdapter.Fill(animalTable);

                    listAssociatedAnimals.DisplayMemberPath = "Name";
                    listAssociatedAnimals.SelectedValuePath = "Id";
                    listAssociatedAnimals.ItemsSource = animalTable.DefaultView;


                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }


        private void listZoos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(listZoos.SelectedValue.ToString());

            ShowAssociatedAnimals();
            ShowSelectedZooInTheTextBox();

        }
        private void ShowAnimals()
        {
            try
            {
                string query = "select * from Animal";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);

                using (sqlDataAdapter)
                {
                    DataTable animalTable = new DataTable();
                    sqlDataAdapter.Fill(animalTable);

                    listAnimals.DisplayMemberPath = "Name";
                    listAnimals.SelectedValuePath = "Id";
                    listAnimals.ItemsSource = animalTable.DefaultView;


                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }


        }

        private void DeleteZoo_click(object sender, RoutedEventArgs e) 
        {
            //MessageBox.Show("delete zoo clicked");

            try
            {
                string query = "Delete from Zoo where Id = @ZooId";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);


                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@ZooID", listZoos.SelectedValue);
                sqlCommand.ExecuteScalar();
                sqlConnection.Close();
                ShowZoos();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

        }

        private void AddZoo_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string query = "INSERT INTO Zoo VALUES (@Location)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@Location", ZooName.Text);
                sqlCommand.ExecuteScalar();
                //sqlConnection.Close();
                //ShowZoos();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                sqlConnection.Close();
                ShowZoos();
            }


        }

        private void AddAnimalToZoo_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("Add animal to zoo"+ listAnimals.SelectedValue.ToString());

            try
            {
                string query = "INSERT INTO ZooAnimal VALUES (@ZooID, @AnimalID)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@ZooID", listZoos.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@AnimalID", listAnimals.SelectedValue);
                sqlCommand.ExecuteScalar();
                //sqlConnection.Close();
                //ShowZoos();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                sqlConnection.Close();
                ShowAssociatedAnimals();
            }

        }

        private void RemoveAnimal_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(listAssociatedAnimals.SelectedValue.Equals, NULL);

            try
            {
                string query = "Delete from ZooAnimal where ZooID = @ZooId and AnimalId = @AssociatedAnimal";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@ZooID", listZoos.SelectedValue);
                    sqlCommand.Parameters.AddWithValue("@AssociatedAnimal", listAssociatedAnimals.SelectedValue);
                    sqlCommand.ExecuteScalar();



            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("select animal to be removed");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                sqlConnection.Close();
                ShowAssociatedAnimals();
            }
        }

        private void AddAnimal_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Inside add animal");

            try
            {
                string query = "INSERT INTO Animal VALUES (@Animal)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@Animal", ZooName.Text);
                sqlCommand.ExecuteScalar();
                //sqlConnection.Close();
                //ShowZoos();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                sqlConnection.Close();
                ShowAnimals();
            }
        }

        private void DeleteAnimal_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Inside delete Animal");
            try
            {
                string query = "DELETE FROM Animal where Id = @Animal";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@Animal", listAnimals.SelectedValue);
                sqlCommand.ExecuteScalar();
                //sqlConnection.Close();
                //ShowZoos();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                sqlConnection.Close();
                ShowAnimals();
            }
        }

        private void ShowSelectedZooInTheTextBox()
        {
            //MessageBox.Show("HEY");
            try
            {
                string query = "SELECT * FROM Zoo WHERE Id = @ZooId";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                using (sqlDataAdapter)
                {

                    sqlCommand.Parameters.AddWithValue("@ZooID", listZoos.SelectedValue);

                    DataTable zooTable = new DataTable();
                    sqlDataAdapter.Fill(zooTable);

                    ZooName.Text = zooTable.Rows[0]["Location"].ToString();

                }
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void UpdateZoo_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("test");
            try
            {
                string query = "UPDATE Zoo SET LOCATION = @Location WHERE Id = @ZooId";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@ZooId", listZoos.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@Location", ZooName.Text);
                sqlCommand.ExecuteScalar();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally 
            {
                sqlConnection.Close();
                ShowZoos();
                ShowAssociatedAnimals();


            }
        }

        private void ShowSelectedAnimalInTheTextBox()
        {
            //MessageBox.Show("HEY");
            try
            {
                string query = "SELECT * FROM Animal WHERE Id = @AnimalId";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                using (sqlDataAdapter)
                {

                    sqlCommand.Parameters.AddWithValue("@AnimalId", listAnimals.SelectedValue);

                    DataTable animalTable = new DataTable();
                    sqlDataAdapter.Fill(animalTable);

                    ZooName.Text = animalTable.Rows[0]["Name"].ToString();

                }
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void UpdateAnimal_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("test");
            try
            {
                string query = "UPDATE Animal SET Name = @Animal WHERE Id = @AnimalId";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@AnimalId", listAnimals.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@Animal", ZooName.Text);
                sqlCommand.ExecuteScalar();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                sqlConnection.Close();
                ShowAnimals();
            }
        }

        private void listAnimals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowSelectedAnimalInTheTextBox();
          
        }
    }
}
