using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace WpfApp9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string ImagePath { get; set; }
        public string Minute { get; set; }
        public string Description { get; set; }

        public dynamic Data { get; set; }
        public dynamic SingleData { get; set; }
        int i = 0;

        

        HttpClient httpClient = new HttpClient();

        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Backbtn.Visibility = Visibility.Hidden;
            Nextbtn.Visibility = Visibility.Hidden;
            backbor.Visibility = Visibility.Hidden;
            nextbor.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var name = movieTextbox.Text;
            HttpResponseMessage response = new HttpResponseMessage();
            response = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=bdba957b&s={name}&plot=full").Result;
            var str = response.Content.ReadAsStringAsync().Result;

            Data = JsonConvert.DeserializeObject(str);

            response = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=bdba957b&t={Data.Search[0].Title}&plot=full").Result;

            str = response.Content.ReadAsStringAsync().Result;

            SingleData = JsonConvert.DeserializeObject(str);
            movieImage.Source = SingleData.Poster;
            movieImage2.Source = SingleData.Poster;
            Minute = SingleData.Runtime;
            Description = SingleData.Genre;
            movieLabel.Content = Minute + Description;
            Nextbtn.Visibility = Visibility.Visible;
            nextbor.Visibility = Visibility.Visible;
        }

        private void movieTextbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            movieTextbox.Text = string.Empty;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (i>0)
            {

            
            i--;
            var name = movieTextbox.Text;
            HttpResponseMessage response = new HttpResponseMessage();
            response = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=bdba957b&t={Data.Search[i].Title}&plot=full").Result;
               

               
            var str = response.Content.ReadAsStringAsync().Result;

            str = response.Content.ReadAsStringAsync().Result;

            SingleData = JsonConvert.DeserializeObject(str);
            movieImage.Source = SingleData.Poster;
            movieImage2.Source = SingleData.Poster;
            Minute = SingleData.Runtime;
            Description = SingleData.Genre;
            movieLabel.Content = Minute + Description;
               
            }
            
            if(i == 0) { Backbtn.Visibility = Visibility.Hidden;
                backbor.Visibility = Visibility.Hidden;
                Nextbtn.Visibility = Visibility.Visible;
                nextbor.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            i++;
            Backbtn.Visibility = Visibility.Visible;
            backbor.Visibility = Visibility.Visible;
            var name = movieTextbox.Text;
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
            response = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=bdba957b&t={Data.Search[i].Title}&plot=full").Result;

            }
            catch (Exception)
            {
                Nextbtn.Visibility = Visibility.Hidden;
                nextbor.Visibility = Visibility.Hidden;
                return;
            }
            if (response != null)
            {
                var str = response.Content.ReadAsStringAsync().Result;

            str = response.Content.ReadAsStringAsync().Result;

            SingleData = JsonConvert.DeserializeObject(str);
            movieImage.Source = SingleData.Poster;
            movieImage2.Source = SingleData.Poster;
            Minute = SingleData.Runtime;
            Description = SingleData.Genre;
            movieLabel.Content = Minute + Description;
            }
            else
            {
                return;
            }
        }
    }
}
