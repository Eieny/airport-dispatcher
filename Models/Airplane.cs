using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoDispatcher
{    
    class Airplane : NotifyPropertyChanged
    {
        private double _FuelSupply;
        /// <summary>
        /// Запас топлива
        /// </summary>
        public double FuelSupply
        {
            get { return _FuelSupply; }
            private set
            {
                _FuelSupply = value;
                OnPropertyChanged(nameof(FuelSupply));
            }
        }

        /// <summary>
        /// Расход топлива в секунду
        /// </summary>
        public double FuelConsumption { get; }

        /// <summary>
        /// Фирма-производитель
        /// </summary>
        public string Company { get; }

        /// <summary>
        /// Бортовой номер
        /// </summary>
        public int Number { get; }

        private TimeSpan _Time;
        /// <summary>
        /// Время, на которое самолёт займёт взлётно-посадочную полосу
        /// </summary>
        public TimeSpan Time
        {
            get { return _Time; }
            private set
            {
                _Time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        private TimeSpan _LeftTime;
        /// <summary>
        /// Время, на которое хватит топлива
        /// </summary>
        public TimeSpan LeftTime
        {
            get { return _LeftTime; }
            private set
            {
                _LeftTime = value;
                OnPropertyChanged(nameof(LeftTime));
            }
        }



        /// <summary>
        /// Инициализирует новый экземпляр класса Airplane
        /// </summary>
        public Airplane()
        {
            FuelSupply = 3000;
            FuelConsumption = 30;
            Company = "Default Airplane";
            Number = 0;
            Time = new TimeSpan(0, 1, 0);
            LeftTime = new TimeSpan(0, 0, 100);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса Airplane
        /// </summary>
        /// <param name="fuelSup">Запас топлива</param>
        /// <param name="fuelCons">Расход топлива в секунду</param>
        /// <param name="firm">Фирма-производитель</param>
        /// <param name="num">Бортовой номер</param>
        /// <param name="time">Время, на которое самолёт займёт взлётно-посадочную полосу</param>
        public Airplane(double fuelSup, double fuelCons, string firm, int num, TimeSpan time)
        {
            FuelSupply = fuelSup;
            FuelConsumption = fuelCons;
            Company = firm;
            Number = num;
            Time = time;
            LeftTime = new TimeSpan(0, 0, (int)(fuelSup / fuelCons));
        }

        /// <summary>
        /// Отсчёт топлива и времени до падения самолёта
        /// </summary>
        public void FuelTimeLeft()
        {
            FuelSupply -= FuelConsumption;
            LeftTime -= new TimeSpan(0, 0, 1);
        }

        /// <summary>
        /// Отсчёт времени посадки самолёта
        /// </summary>
        public void TimeLeft()
        {
            Time -= new TimeSpan(0, 0, 1);
        }
    }
}
