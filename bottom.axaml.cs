using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

//здесь поменять название проекта
namespace AvaloniaApplication1;

public partial class bottom : Window
{
    
    private MySqlConnection _connection;
    // здесь необходимо поменять строку подключения)
    private string connectionString = "server=localhost;port=3301;database=abd;user id=root;password=Ghost45)";
    //здесь используется классы при необходимости можно поменять
    private List<users_pon> _pons;
    
    //этот класс используется для фильтрации
    private List<filters> _filters;
    public bottom()
    {
        InitializeComponent();
        //Это испольузуется для передачи отображения запроса таблицы
        string sql = "SELECT * FROM stuff";
        //Это метод отображения таблицы
        ShowTable(sql);
        //Это метод для фильтрации combobox)
        filter_user();
    }

    public class users_pon
    {
        //Это поля из таблицы которую необходимо отобразить
        public int id { get; set; }
        public string name { get; set; }
        public string last_name { get; set; }
        public int phone { get; set; }
        public DateTime year { get; set; }
    }

    public class filters
    {
        //Это для фильтрации в combobox
        public int id { get; set; }
        public string name { get; set; }
    }

    private void ShowTable(string sql)
    {
        //Это метод для отображения таблицы
        _pons = new List<users_pon>();
        _connection = new MySqlConnection(connectionString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        
        while (reader.Read() && reader.HasRows)
        {
            var current = new users_pon()
            {
                //Названия полей необходимо поменять) брать из созданного класса)
                id = reader.GetInt32("id"),
                name = reader.GetString("name"),
                last_name = reader.GetString("last_name"),
                phone = reader.GetInt32("phone"),
                year = reader.GetDateTime("year")
            };
            _pons.Add(current);
        }

        Grid.ItemsSource = _pons;
    }

    private void Add_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
            //Запрос необходимо поменять поля таблицы и название таблицы)
            string insert = "INSERT INTO stuff (name, last_name, phone, year) VALUES ('"+text2.Text+"', '"+text3.Text+"', '"+text4.Text+"', '"+text5.Text+"')";
            MySqlCommand command = new MySqlCommand(insert, _connection);
            command.ExecuteNonQuery();
            _connection.Close();
            text1.Text = "Succesfully data";
        }
        catch (Exception exception)
        {
            text1.Text = "Incorrect data";
        }
    }

    private void Update_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
            string update = "UPDATE stuff SET name = '"+text2.Text+"', last_name = '"+text3.Text+"', phone = '"+text4.Text+"', year = '"+text5.Text+"' WHERE id = '"+text1.Text+"'";
            MySqlCommand command = new MySqlCommand(update, _connection);
            command.ExecuteNonQuery();
            _connection.Close();
            text1.Text = "Succesfully data";
        }
        catch (Exception exception)
        {
            text1.Text = "Incorrect data";
        }
    }

    private void Delete_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
            string update = "DELETE FROM Stuff WHERE id = '"+text1.Text+"'";
            MySqlCommand command = new MySqlCommand(update, _connection);
            command.ExecuteNonQuery();
            _connection.Close();
            text1.Text = "Succesfully data";
        }
        catch (Exception exception)
        {
            text1.Text = "Incorrect data";
        }
    }

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        _pons = new List<users_pon>();
        _connection = new MySqlConnection(connectionString);
        _connection.Open();
        string sql = "SELECT * FROM stuff";
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read() && reader.HasRows)
        {
            var current = new users_pon()
            {
                id = reader.GetInt32("id"),
                name = reader.GetString("name"),
                last_name = reader.GetString("last_name"),
                phone = reader.GetInt32("phone"),
                year = reader.GetDateTime("year")
            };
            _pons.Add(current);
        }

        Grid.ItemsSource = _pons;
    }

    private void filter_user()
    {
        _filters = new List<filters>();
        _connection = new MySqlConnection(connectionString);
        _connection.Open();
        string sql = "SELECT id, name FROM stuff";
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read() && reader.HasRows)
        {
            var current = new filters()
            {
                id = reader.GetInt32("id"),
                name = reader.GetString("name"),
            };
            _filters.Add(current);
        }

        var combobox = this.Find<ComboBox>("Box");
        combobox.ItemsSource = _filters;
        _connection.Close();
        

    }

    private void Box_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var combobox = (ComboBox)sender;
        var current = combobox.SelectedItem as filters;
        var filter = _pons.Where(x => x.name == current.name).ToList();
        Grid.ItemsSource = filter;
    }

    private void Search_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        string sql1 = "SELECT id, name, last_name, phone, year FROM stuff WHERE name LIKE '%"+search.Text+"%' OR last_name LIKE '%"+search.Text+"%' ";
        ShowTable(sql1);
    }

    private void A_Z_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        MainWindow _mainWindow = new MainWindow();
        this.Hide();
        _mainWindow.Show();
    }
}