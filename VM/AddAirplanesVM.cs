using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AutoDispatcher
{
    class AddAirplanesVM
    {
        public ObservableCollection<Airplane> airplanes { get; set; }
        public ObservableCollection<Airplane> airplanesSet { get; set; }

        /// <summary>
        /// Добавление самолётов из набора в симуляцию
        /// </summary>
        public ICommand AddAirplanesCommand { get; set; }
        private void AddAirplanes(Collection<object> items)
        {
            List<Airplane> list = items.Cast<Airplane>().ToList();
            for (int i = 0; i < list.Count; i++)
            {
                Airplane item = list[i];
                airplanes.Add(new Airplane(item.FuelSupply, item.FuelConsumption, item.Company, item.Number, item.Time));
            }
            Application.Current.Windows[1].Close();
        }
        public AddAirplanesVM()
        {
            //Инициализация команды
            AddAirplanesCommand = new SimpleCommand(AddAirplanes);
        }
    }
}
