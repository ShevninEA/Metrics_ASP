using LiveCharts;
using MetricsManagerReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace MetricsManager.WpfClient
{
    /// <summary>
    /// Логика взаимодействия для CpuChart.xaml
    /// </summary>
    public partial class CpuChart : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private SeriesCollection _columnSeriesValues;
        MetricsManagerClient metricsManagerClient;

        public SeriesCollection ColumnSeriesValues
        {
            get
            {
                return _columnSeriesValues;
            }
            set
            {
                _columnSeriesValues = value;
                OnPropertyChanged("ColumnSeriesValues");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public CpuChart()
        {
            InitializeComponent();
        }
        private void UpdateOnСlick(object sender, RoutedEventArgs e)
        {
            if (metricsManagerClient == null)
            {
                metricsManagerClient = new MetricsManagerClient("http://localhost:5123/", new HttpClient());
                DataContext = this;
            }

        }
    }
}
