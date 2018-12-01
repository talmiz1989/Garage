using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;
using Ex03.ConsoleUI;
namespace Ex03.ConsoleUI
{
    public class Program
    {
        public static void Main()
        {
            Garage MyGarage = new Garage() ;//create a new garage
                                            //  MyGarage.InsertVehicleToGarage("201050721");
            MyGarage.ShowMainMenu();
        }
    }
}
