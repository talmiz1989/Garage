using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Checker
    {
        public static float m_CurrAirPressure = 0;
        public static float m_MaxAirPressure = 0;
        public static float m_CurrBatteryTime = 0;
        public static float m_MaxBatteryTime = 0;
        public static float m_CurrFuelTank = 0;
        public static float m_MaxFuelTank = 0;

        public static void InputChecker(int i_index, bool i_isElectric, string i_VehicleType, string i_input)
        {
            
            string StringForTheValOutOfRangeException = " is lower than the current pressure-> ";
            try
            {
                switch (i_index)
                {
                    case 1://Whats your owner name
                        {
                            Client.CheckUserNameInput(i_input);
                            break;
                        }
                    case 2://Whats the phone number
                        {
                            Client.CheckUserPhoneNumber(i_input);
                            break;
                        }
                    case 3://Car model name
                        {
                            Vehicle.CheckVehicleModuleName(i_input);
                            break;
                        }
                    case 4: //License plate number
                        {
                            Vehicle.CheckVehiclePlate(i_input);
                            break;
                        }
                    case 5://car:color | truck: does have toxic | motor: licese type
                        {
                            FunctionForQ_6(i_VehicleType, i_input);
                            break;
                            //function with switch to car truck and motor. need to send the input and the vehcile type
                        }
                    case 6://car: how many doors | truck: capacity | motor: engine volume
                        {
                            FunctionForQ_7(i_VehicleType, i_input);
                            break;
                        }
                    case 7://Wheel manufracture name
                        {
                            Wheel.CheckWheelManufacturer(i_input);
                            break;
                        }
                    case 8://current air pressure
                        {
                            m_CurrAirPressure = Wheel.CheckUserInputCuurAndMaxAirPressure(i_input);
                            break;
                        }
                    case 9://max air pressure
                        {
                            m_MaxAirPressure = Wheel.CheckUserInputCuurAndMaxAirPressure(i_input);
                            if(m_MaxAirPressure < m_CurrAirPressure)
                            {
                                throw new ValueOutOfRangeExeption(m_CurrAirPressure,m_MaxAirPressure,StringForTheValOutOfRangeException);  //OUT OF RANGE - TAL
                            }
                            break;
                        }
                    case 10://elctric:battry life | fuel: fuel type
                        {
                            FunctionForQ_11(i_isElectric, i_input);
                            break;
                        }
                    case 11://electric: max battery life  | fuel: curr fule tank
                        {
                            FunctionForQ_12(i_isElectric, i_input);
                            if(m_MaxBatteryTime < m_CurrBatteryTime)
                            {
                                throw new ValueOutOfRangeExeption(m_CurrBatteryTime, m_MaxBatteryTime, StringForTheValOutOfRangeException);  //OUT OF RANGE - TAL
                            }
                            break;
                        }
                    case 12://max fuel at gasoline engine
                        {
                            m_MaxFuelTank = GasolineEngine.CheckUserInputForCurrAndMaxGasAmount(i_input);
                            if(m_MaxFuelTank < m_CurrFuelTank)
                            {
                                throw new ValueOutOfRangeExeption(m_CurrFuelTank, m_MaxFuelTank, StringForTheValOutOfRangeException);  //OUT OF RANGE - TAL
                            }
                            break;
                        }
                }
            }
            catch
            {
                throw;
            }
           // throw new NotImplementedException();
        }

        public static void FunctionForQ_6(string i_vehicleType, string i_userInput)
        {
            try
            {
                if(i_vehicleType == "Car")
                {
                    Car.CheckUserColorInput(i_userInput);
                }
                else if(i_vehicleType == "Truck")
                {
                    Truck.CheckUserToxicInput(i_userInput); //Check the condition
                }
                else
                {
                    Motorcycle.CheckLiceseType(i_userInput);
                }
            }
            catch
            {
                throw;
            }
        }

        //car: how many doors | truck: capacity | motor: engine volume
        public static void FunctionForQ_7(string i_vehicleType, string i_userInput)
        {
            try
            {
                if(i_vehicleType == "Car")
                {
                    Car.CheckIfDoorsIsValid(i_userInput);
                }
                else if(i_vehicleType == "Truck")
                {
                    Truck.CheckUserCarryWight(i_userInput);
                }
                else
                {
                    Motorcycle.CheckEngineVolume(i_userInput);
                }
            }
            catch
            {
                throw;
            }
        }

        public static void FunctionForQ_11(bool i_isElectric, string i_userInput)
        {
            try
            {
                if(i_isElectric == true)
                {
                    m_CurrBatteryTime = ElectricEngine.CheckUserIputForCurrAndMaxBatteryLife(i_userInput);
                }
                else
                {
                    GasolineEngine.CheckIGasTypeIsValid(i_userInput);
                }
            }
            catch
            {
                throw;
            }
        }

        //elctric:battry life | fuel: fuel type
        public static void FunctionForQ_12(bool i_isElectric, string i_userInput)
        {
            try
            {
                if(i_isElectric == true)
                {
                    m_MaxBatteryTime = ElectricEngine.CheckUserIputForCurrAndMaxBatteryLife(i_userInput);
                }
                else
                {
                    m_CurrFuelTank = GasolineEngine.CheckUserInputForCurrAndMaxGasAmount(i_userInput);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
