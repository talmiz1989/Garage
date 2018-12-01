using System;
using System.Collections.Generic;
using System.Text;
using System.Threading; //REMOVE

namespace Ex03.GarageLogic
{
    public class Client
    {
        private string m_clientName;
        private string m_clientPhoneNUmber;
        public enum eVehicleStatus { Payed, Fixed, Inprocess };
        private eVehicleStatus m_vehicleStatus;
        private Vehicle m_clientVehicle;
        public Client(Vehicle i_vehicle, List<string> i_dataMembers)
        {
            clientVehicle = i_vehicle;
            vehicleStatus = eVehicleStatus.Inprocess;
            clientName = i_dataMembers[0];
            clientPhoneNumber = i_dataMembers[1];
        }
        public static eVehicleStatus EVehicleStatusParse(string i_strToEnum)
        {
            try
            {
                eVehicleStatus resEnum = (eVehicleStatus)Enum.Parse(typeof(eVehicleStatus), i_strToEnum);
                return resEnum;
            }
            catch(ArgumentException)
            {
                throw;
            }
        }
        public void CalcEnergyPrecent()
        {
            clientVehicle.energyPrecentLeft = clientVehicle.engine.CalcEnginePrecent();
        }
        public eVehicleStatus vehicleStatus
        {
            get { return m_vehicleStatus; }
            set { m_vehicleStatus = value; }
        }
        public string clientName
        {
            get { return m_clientName; }
            set { m_clientName = value; }
        }
        public string clientPhoneNumber
        {
            get { return m_clientPhoneNUmber; }
            set { m_clientPhoneNUmber = value; }
        }
        public Vehicle clientVehicle
        {
            get { return m_clientVehicle; }
            set { m_clientVehicle = value; }
        }
        public static void AddClientQuestions(ref List<string> io_questions)// this method gets a questions list and add the client questions to the list
        {
            io_questions.Add("What is the vehicles owner name? ");
            io_questions.Add("What is the vehicles owner phone number? ");
        }

        public void InflateAllWheels()
        {
            clientVehicle.InflateAllWheelss();
        }

        public static void CheckUserPhoneNumber(string i_clientPhoneNumber)
        {
            int number;

            try
            {
                number = int.Parse(i_clientPhoneNumber);
            }
            catch(FormatException)
            {
                throw;
            }
        }
        public static eVehicleStatus GetVehicleStatus(ref bool io_isInputLegal)
        {
            eVehicleStatus res;
            string newStatus;

            newStatus = Console.ReadLine();
            try
            {
                res = Client.EVehicleStatusParse(newStatus);
            }
            catch(FormatException)
            {
                throw;
            }
            io_isInputLegal = true;
            return res;
        }
    
        public static void CheckUserNameInput(string i_userInput)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(i_userInput, @"^[a-zA-Z ]+$") == false)
            {
                throw new FormatException();
            }
        }

        public List<string> GetVehicelsDataByUserChoise()
        {
            List<string> resDataList = new List<string>();

            GetClientData(ref resDataList);
            clientVehicle.GetVehicleData(ref resDataList);
            return resDataList;
        }
        public void GetClientData(ref List<string> io_dataList)
        {
            io_dataList.Add("Client name: " + clientName);
            io_dataList.Add("Client phone number:" + clientPhoneNumber);
            io_dataList.Add("Vehicle status: " + vehicleStatus.ToString());
        }

        public void CheckIfEngineIsElectricAndCharge(float i_minutesToCharge)
        {
            if(clientVehicle.engine.IsElectric == true)
            {
                ChargeVehicelsEngine(i_minutesToCharge);
            }
            else
            {
                throw new ArgumentException();
            }
        }
        public void ChargeVehicelsEngine(float i_minutesToCharge)
        {
            if(clientVehicle.engine.ChargeOrFuleVehicle(i_minutesToCharge) == false)
            {
                ElectricEngine ClientsElectricEngine = clientVehicle.engine as ElectricEngine;
                throw new ValueOutOfRangeExeption(ClientsElectricEngine.maximalBatteryTime, i_minutesToCharge, " will exceed the maximal capacity of ");
            }
        }

        public void CheckIfEngineIsGasolineAndFuel(float i_litersToFuel)
        {
            if(clientVehicle.engine.IsElectric == false)
            {
                FuelEngine(i_litersToFuel);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void FuelEngine(float i_litersToFuel)
        {
            if(clientVehicle.engine.ChargeOrFuleVehicle(i_litersToFuel) == false)
            {
                GasolineEngine ClientsGasEngine = clientVehicle.engine as GasolineEngine;
                throw new ValueOutOfRangeExeption(ClientsGasEngine.maximalGasAmount,i_litersToFuel, " will exceed the maximal capacity of ");
            }
        }
    }   
}
