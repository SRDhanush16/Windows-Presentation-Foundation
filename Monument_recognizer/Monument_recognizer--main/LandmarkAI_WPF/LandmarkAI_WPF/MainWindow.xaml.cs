using LandmarkAI_WPF.Classes;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LandmarkAI_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialogs = new OpenFileDialog();
            dialogs.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg;*.jpeg|All files(*.*)|*.*";
            dialogs.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (dialogs.ShowDialog() == true)
            {
                String filename = dialogs.FileName;
                selectedImage.Source = new BitmapImage(new Uri(filename));
                MakePredictionAsyn(filename);
            }

        }

        private async void MakePredictionAsyn(string filename)
        {
            string url = "https://centralindia.api.cognitive.microsoft.com/customvision/v3.0/Prediction/994f9cfc-967b-4744-ab40-56a23bc0440e/classify/iterations/Iteration1/image";
            string prediction_key = "8f526ece46f4426da07430fa934ada4d";
            string content_type = "application/octet-stream";

            var file = File.ReadAllBytes(filename);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Prediction-Key", prediction_key);
                using (var content = new ByteArrayContent(file))
                {
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(content_type);
                    var response = await client.PostAsync(url, content);

                    var responseString = await response.Content.ReadAsStringAsync();

                    List<Prediction> predictions = new List<Prediction>((JsonConvert.DeserializeObject<CustomVision>(responseString)).Predictions);
                    predictionsListView.ItemsSource = predictions;


                }

            }

        }
    }
}