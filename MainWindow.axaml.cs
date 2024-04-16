using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;

namespace AvaloniaApplication1;

public partial class MainWindow : Window
{
    private MySqlConnection _mySqlConnection;
    private string connectionString = "server=localhost;port=3301;database=abd;user id=root;password=Ghost45)";
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Enter_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            string sql1 = "SELECT login, password, role_id FROM users WHERE login = '" + login.Text + "' AND password = '" +
                          password.Text + "' AND role_id = '1'";
            _mySqlConnection = new MySqlConnection(connectionString);
            _mySqlConnection.Open();
            string sql2 = "SELECT login, password, role_id FROM users WHERE login = '" + login.Text + "' AND password = '" +
                          password.Text + "' AND role_id = '2'";
            MySqlCommand sqlCommand = new MySqlCommand(sql2, _mySqlConnection);
            MySqlCommand mySqlCommand = new MySqlCommand(sql1, _mySqlConnection);
            if (mySqlCommand.ExecuteScalar() != null)
            {
            
                bottom _bottom = new bottom();
                this.Hide();
                _bottom.Show();
            }
            if (sqlCommand.ExecuteScalar() != null)
            {
                bottom _bottom = new bottom();
                this.Hide();
                _bottom.Add.IsVisible = false;
                _bottom.Update.IsVisible = false;
                _bottom.Show();
                login.Text = "Sucessfully";
            }
        }
        catch (Exception exception)
        {
            login.Text = "Incorrect login or password)";
        }
        
    }

    private void Register_OnClick(object? sender, RoutedEventArgs e)
    {
        regis _regis = new regis();
        this.Hide();
        _regis.Show();
    }
}