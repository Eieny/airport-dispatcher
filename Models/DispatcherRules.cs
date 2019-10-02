using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDispatcher
{
    class DispatcherRules : NotifyPropertyChanged
    {
        /// <summary>
        /// Диспетчер садит в первую очередь те самолёты, у которых быстрее закончится топливо
        /// </summary>
        /// <param name="airplanes">Список самолётов в небе</param>
        /// <param name="onwayAirplanes">Массив самолётов, которые находятся на взлётно-посадочной полосе</param>
        /// <param name="landingAirplanes">Список севших самолётов</param>
        /// <param name="crashAirplanes">Список разбившихся самолётов</param>
        static public void FuelRunout(ObservableCollection<Airplane> airplanes, ObservableCollection<Airplane> onwayAirplanes,
            List<Airplane> landingAirplanes, List<Airplane> crashAirplanes)
        {
            List<Airplane> tmp = new List<Airplane>(airplanes);
            tmp.Sort((a, b) => a.LeftTime.CompareTo(b.LeftTime));
            int airplaneIndex;

            for (int i = 0; i < onwayAirplanes.Count; i++)
            {
                airplaneIndex = tmp.Count > 0 ? airplanes.IndexOf(tmp[0]) : 0;

                if (airplanes.Count > 0 && CheckCrash(airplanes))
                    CrashAirplane(airplanes, crashAirplanes);
                else if (onwayAirplanes[i] == null && airplanes.Count > 0)
                {
                    OnwayAirplane(airplanes, onwayAirplanes, airplaneIndex, i);
                    tmp.Remove(tmp[0]);
                }
                else if (onwayAirplanes[i] != null && onwayAirplanes[i].Time == TimeSpan.Zero)
                    LandingAirplane(landingAirplanes, onwayAirplanes, i);
            }
        }

        /// <summary>
        /// Диспетчер садит только те самолёты, которые выпущенyы определённой фирмой
        /// </summary>
        /// <param name="airplanes">Список самолётов в небе</param>
        /// <param name="onwayAirplanes">Массив самолётов, которые находятся на взлётно-посадочной полосе</param>
        /// <param name="landingAirplanes">Список севших самолётов</param>
        /// <param name="crashAirplanes">Список разбившихся самолётов</param>
        /// <param name="Company">Название фирмы</param>
        static public void SpecificCompany(ObservableCollection<Airplane> airplanes, ObservableCollection<Airplane> onwayAirplanes,
            List<Airplane> landingAirplanes, List<Airplane> crashAirplanes, string Company)
        {
            List<Airplane> tmp = new List<Airplane>(airplanes);
            tmp = tmp.Where(a => a.Company == Company).ToList();
            int airplaneIndex;

            for (int i = 0; i < onwayAirplanes.Count; i++)
            {
                airplaneIndex = tmp.Count > 0 ? airplanes.IndexOf(tmp[0]) : 0;

                if (airplanes.Count > 0 && CheckCrash(airplanes))
                    CrashAirplane(airplanes, crashAirplanes);
                else if (onwayAirplanes[i] == null && tmp.Count > 0)
                {
                    OnwayAirplane(airplanes, onwayAirplanes, airplaneIndex, i);
                    tmp.Remove(tmp[0]);
                }
                else if (onwayAirplanes[i] != null && onwayAirplanes[i].Time == TimeSpan.Zero)
                    LandingAirplane(landingAirplanes, onwayAirplanes, i);
            }
        }

        /// <summary>
        /// Диспетчер садит самолёты по очереди(первый прилетевший сел первым)
        /// </summary>
        /// <param name="airplanes">Список самолётов в небе</param>
        /// <param name="onwayAirplanes">Массив самолётов, которые находятся на взлётно-посадочной полосе</param>
        /// <param name="landingAirplanes">Список севших самолётов</param>
        /// <param name="crashAirplanes">Список разбившихся самолётов</param>
        static public void FirstInFirstOut(ObservableCollection<Airplane> airplanes, ObservableCollection<Airplane> onwayAirplanes, 
            List<Airplane> landingAirplanes, List<Airplane> crashAirplanes)
        {
            for (int i = 0; i < onwayAirplanes.Count; i++)
            {
                if (airplanes.Count > 0 && CheckCrash(airplanes))
                    CrashAirplane(airplanes, crashAirplanes);
                else if (onwayAirplanes[i] == null && airplanes.Count > 0)
                    OnwayAirplane(airplanes, onwayAirplanes, 0, i);
                else if (onwayAirplanes[i] != null && onwayAirplanes[i].Time == TimeSpan.Zero)
                    LandingAirplane(landingAirplanes, onwayAirplanes, i);
            }
        }



        /// <summary>
        /// Проверка на наличие самолётов, которые упали
        /// </summary>
        /// <param name="airplanes">Список самолётов</param>
        /// <returns></returns>
        static private bool CheckCrash(ObservableCollection<Airplane> airplanes)
        {
            return airplanes.Where(a => a.LeftTime == TimeSpan.Zero).ToList().Count > 0 ? true : false;
        }

        /// <summary>
        /// Самолёт разбился
        /// </summary>
        /// <param name="airplanes">Список приземлившихся самолётов</param>
        /// <param name="crashAirplanes">Список разбившихся самолётов</param>
        /// <param name="airplaneIndex">Индекс самолёта в списке airplanes, который забился</param>
        static private void CrashAirplane(ObservableCollection<Airplane> airplanes,  List<Airplane> crashAirplanes)
        {
            //var airplane = airplanes[airplaneIndex];
            for (int i = 0; i < airplanes.Count; i++)
                if (airplanes[i].LeftTime == TimeSpan.Zero)
                    Logger.Crash(airplanes[i].Company, airplanes[i].Number);

            List<Airplane> tmp = new List<Airplane>(airplanes.Where(a => a.LeftTime == TimeSpan.Zero));
            //airplanes.RemoveAll(a => a.LeftTime == TimeSpan.Zero);

            airplanes.Where(a => a.LeftTime == TimeSpan.Zero).ToList().All(i => airplanes.Remove(i));
            crashAirplanes.AddRange(tmp);
        }

        /// <summary>
        /// Приземление самолёта
        /// </summary>
        /// <param name="landingAirplanes">Список приземлившихся самолётов</param>
        /// <param name="onwayAirplane">Самолёт, который приземлился и отъехал с полосы в ангар</param>
        /// <param name="strip">Полоса, на которой был самолёт</param>
        static private void LandingAirplane(List<Airplane> landingAirplanes, ObservableCollection<Airplane> onwayAirplanes, int stripIndex)
        {
            var onwayAirplane = onwayAirplanes[stripIndex];
            Logger.Landing(onwayAirplane.Company, onwayAirplane.Number, stripIndex + 1);
            landingAirplanes.Add(onwayAirplane);
            onwayAirplanes[stripIndex] = null;
        }

        /// <summary>
        /// Заход самолёта на взлётно-посадочную полосу
        /// </summary>
        /// <param name="airplanes">Список самолётов в небе</param>
        /// <param name="onwayAirplane">Объект самолёта, который заходит на взлётно-посадочную полосу</param>
        /// <param name="airplaneIndex">Индекс самолёта в списке airplanes, которы заходит на полосу</param>
        static private void OnwayAirplane(ObservableCollection<Airplane> airplanes, ObservableCollection<Airplane> onwayAirplanes, 
            int airplaneIndex, int stripIndex)
        {
            var airplane = airplanes[airplaneIndex];
            onwayAirplanes[stripIndex] = new Airplane();
            onwayAirplanes[stripIndex] = airplane;
            airplanes.Remove(airplane);
        }
    }
}
