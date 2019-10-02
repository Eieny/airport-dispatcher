using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace AutoDispatcher 
{
    class Airport
    {
        const int stripCount = 3;

        List<Airplane> landingAirplanes = new List<Airplane>();
        List<Airplane> crashAirplanes = new List<Airplane>();
        //ObservableCollection<Airplane> airplanesSet = new ObservableCollection<Airplane>();

        public bool firstInFirstOut { get; set; }
        public bool fuelRunout { get; set; } = true;
        public bool specificCompany { get; set; }
        public string companyName { get; set; }
        public ObservableCollection<Airplane> onwayAirplanes { get; set; }
        public ObservableCollection<Airplane> airplanes { get; set; }
        public ObservableCollection<Airplane> airplanesSet { get; set; }


        ///Команда для отрытия нового окна
        public ICommand AddAirplanesCommand { get; set; }
        private void ShowView()
        {
            AddAirplanesVM view = new AddAirplanesVM()
            {
                airplanes = airplanes,
                airplanesSet = airplanesSet
            };
            ViewShower.Show(view);
        }

        public Airport()
        {
            //Инициализация команды
            AddAirplanesCommand = new SimpleCommand(ShowView);
            AirplanesInit();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Update;
            timer.Start();
        }

        public void Update(object sen, EventArgs e)
        {

            if (airplanes.Count > 0)
                for (int i = 0; i < airplanes.Count; i++)
                    airplanes[i].FuelTimeLeft();

            for (int i = 0; i < onwayAirplanes.Count; i++)
                if (onwayAirplanes[i] != null)
                    onwayAirplanes[i].TimeLeft();



            if (specificCompany)
                DispatcherRules.SpecificCompany(airplanes, onwayAirplanes, landingAirplanes, crashAirplanes, companyName);
            else if (firstInFirstOut)
                DispatcherRules.FirstInFirstOut(airplanes, onwayAirplanes, landingAirplanes, crashAirplanes);
            else if (fuelRunout)
                DispatcherRules.FuelRunout(airplanes, onwayAirplanes, landingAirplanes, crashAirplanes);
        }

        /// <summary>
        /// Инициализация списков с самолётами
        /// </summary>
        private void AirplanesInit()
        {
            onwayAirplanes = new ObservableCollection<Airplane>();
            for (int i = 0; i < stripCount; i++)
                onwayAirplanes.Add(null);

            airplanes = new ObservableCollection<Airplane>()
            {
                new Airplane(3000, 50, "3Xtrim", 1, new TimeSpan(0, 0, 20)),
                new Airplane(3001, 10, "Gloster", 2, new TimeSpan(0, 0, 10)),
                new Airplane(500, 1, "Fuji2", 3, new TimeSpan(0, 0, 15)),
                new Airplane(3000, 15, "3Xtrim", 4, new TimeSpan(0, 0, 25)),
                new Airplane(3000, 20, "Folland", 5, new TimeSpan(0, 0, 50)),
                new Airplane(6000, 30, "Eclipse", 6, new TimeSpan(0, 0, 10)),
                new Airplane(200, 5, "Eclipse‎", 7, new TimeSpan(0, 0, 80)),
                new Airplane(1000, 20, "3Xtrim", 8, new TimeSpan(0, 0, 50))
            };

            airplanesSet = new ObservableCollection<Airplane>()
            {
                new Airplane(3000, 50, "3Xtrim", 9, new TimeSpan(0, 0, 20)),
                new Airplane(3001, 10, "Gloster‎", 10, new TimeSpan(0, 0, 10)),
                new Airplane(500, 1, "Fuji", 11, new TimeSpan(0, 0, 15)),
                new Airplane(3000, 15, "3Xtrim", 12, new TimeSpan(0, 0, 25)),
                new Airplane(3000, 20, "Folland", 13, new TimeSpan(0, 0, 50)),
                new Airplane(6000, 30, "Eclipse", 14, new TimeSpan(0, 0, 50)),
                new Airplane(200, 5, "Eclipse", 15, new TimeSpan(0, 0, 50)),
                new Airplane(1000, 20, "3Xtrim", 16, new TimeSpan(0, 0, 50)),
                new Airplane(3000, 50, "3Xtrim", 17, new TimeSpan(0, 0, 20)),
                new Airplane(3001, 10, "Gloster", 18, new TimeSpan(0, 0, 10)),
                new Airplane(500, 1, "Fuji‎", 19, new TimeSpan(0, 0, 15)),
                new Airplane(3000, 15, "3Xtrim", 20, new TimeSpan(0, 0, 25)),
                new Airplane(3000, 20, "Folland", 21, new TimeSpan(0, 0, 50)),
                new Airplane(6000, 30, "Eclipse", 22, new TimeSpan(0, 0, 50)),
                new Airplane(200, 5, "Eclipse", 23, new TimeSpan(0, 0, 50)),
                new Airplane(1000, 20, "3Xtrim", 24, new TimeSpan(0, 0, 50))
            };
        }
    }
}
