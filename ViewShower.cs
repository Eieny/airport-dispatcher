using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDispatcher
{
    class ViewShower
    {
        public static void Show(object dataContex)
        {
            AddAirplane window = new AddAirplane();
            window.DataContext = dataContex;
            window.ShowDialog();
        }
    }
}
