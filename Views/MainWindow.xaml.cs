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
using System.Threading;
using System.Windows.Threading;

namespace AutoDispatcher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Airport();
        }

        private void t(object obj)
        {
            //try
            //{
            //    if (airplanes.Count != 0)
            //        for (int i = 0; i < airplanes.Count; i++)
            //            airplanes[i].FuelTimeLeft();

            //    for (int i = 0; i < onwayAirplanes.Length; i++)
            //        if (onwayAirplanes[i] != null)
            //            onwayAirplanes[i].TimeLeft();

            //    DispatcherRules.SpecificCompany(airplanes, onwayAirplanes, landingAirplanes, airplanesSet, "Кря2");
            //}
            //catch (Exception ex)
            //{
            //    //Dispatcher.Invoke(() => richTextBox.AppendText(ex.Message));
            //    //log.Write(ex.Message);
            //}
            //Dispatcher.Invoke(() => textBlock.Text = $"{airplanes[0].FuelSupply} - {airplanes[0].LeftTime}");
            //Dispatcher.Invoke(() => textBlock_Copy.Text = $"{airplanes[1].FuelSupply} - {airplanes[1].LeftTime}");
            //Dispatcher.Invoke(() => textBlock_Copy1.Text = $"{airplanes[2].FuelSupply} - {airplanes[2].LeftTime}");
            //Dispatcher.Invoke(() => textBlock_Copy2.Text = $"{airplanes[3].FuelSupply} - {airplanes[3].LeftTime}");
            //Dispatcher.Invoke(() => textBlock_Copy3.Text = $"{airplanes[4].FuelSupply} - {airplanes[4].LeftTime}");
        }
    }
}
