using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle     //an abstract class vehicle 
    {
        private string m_modelName;     //vehicles model name
        private string m_licenseNumber; //vehicles license number
        private float m_energyPrecentLeft;//vehicles energy precent left
        public List<Wheel> m_wheels;     //a list of the vehicle wheels
        private Engine m_engine;          //the vehicles engine
        public Vehicle(List<string> i_dataMembers,bool i_isElectric,string i_vehicleName)//constractor of the vehicle
        {
            this.m_modelName = i_dataMembers[2];
            this.m_licenseNumber = i_dataMembers[3];
            this.CreateNewEngine(i_dataMembers, i_isElectric);
            this.CreateNewWheelsList(i_dataMembers, i_vehicleName);
        }
        public Engine engine
        {
            get { return m_engine; }
            set { m_engine = value; }
        }

        public List<Wheel> Wheels
        {
            get { return m_wheels; }
            set { m_wheels = value; }
        }

        public static void AddVehicleQuestions(ref List<string> io_questions)// this method gets a questions list and add the truck questions to the list
        {
            io_questions.Add("What is the vehicle model name?  ");
            io_questions.Add("What is your license plate number?  ");
        }

        public void CreateNewWheelsList(List<string> i_dataMembers,string i_vehicleName)//this method creates a new wheel list according to the type of the vehice
        {
            int i;
            Wheel wheelToAdd;

            if(i_vehicleName.CompareTo("Car")==0)  //if the vehicle is a car
            {
                m_wheels = new List<Wheel>(4);       //create a list of 4 wheels
            }
           else if(i_vehicleName.CompareTo("Truck") == 0)//if the vehicle is a truck
            {
                m_wheels = new List<Wheel>(12);         //create a list of 12 wheels
            }

           else if(i_vehicleName.CompareTo("Motorcycle") == 0)//if the vehicle is a motorcycle
            {
                m_wheels = new List<Wheel>(2);              //create a list of 2 wheels
            }
            for(i = 0 ; i < m_wheels.Capacity ; ++i)//send every wheel to wheel contractor 
            {
                m_wheels.Add(wheelToAdd = new Wheel(i_dataMembers));
            }            
        }

        public void CreateNewEngine(List<string> i_dataMembers ,bool i_isElectric)//this method creates a new engine for the vehicle
        {
            if(i_isElectric == true)//if the vehicles engine is electric
            {
                m_engine = new ElectricEngine(i_dataMembers);//create a new electric engine
            }
            else                    // if the vehicles engine runs on gas
            {
                m_engine = new GasolineEngine(i_dataMembers);//create a new gasoline engine
            }
        }
      //  public abstract void Print(Vehicle i_v);
        public string modelName
        {
            get { return m_modelName; }
            set { m_modelName = value; }
        }
        public string licenseNumber
        {
            get { return m_licenseNumber; }
            set { m_licenseNumber = value; }
        }
        public float energyPrecentLeft
        {
            get { return m_energyPrecentLeft; }
            set { m_energyPrecentLeft = value; }
        }

        public static void CheckUserInputForNewVehivle(string io_userInput)
        {
            if(io_userInput != "Car" && io_userInput != "Truck" && io_userInput != "Motorcycle")
            {
                throw new FormatException();
            }
        }

        public void InflateAllWheelss()
        {
            foreach(var wheel in Wheels)
            {
                wheel.InflateWheelToMax();
            }
        }

        public static void CheckVehicleModuleName(string i_userInput)
        {
           if(System.Text.RegularExpressions.Regex.IsMatch(i_userInput, @"^[a-zA-Z0-9 ]+$") == false)
            {
                throw new FormatException();
            }
        }

        public static void CheckVehiclePlate(string i_userInput)
        {
            //Check the regex contidiion
            if (System.Text.RegularExpressions.Regex.IsMatch(i_userInput, @"^[a-zA-Z0-9]+$") == false)
            {
                throw new FormatException();
            }
        }

        public static void CheckUserEnergySource(string i_userInput)
        {
            float energySourceInPrecent;

            try
            {
                energySourceInPrecent = float.Parse(i_userInput);
            }
            catch(FormatException)
            {
                throw;
            }
        }

        public abstract void GetVehicleTypeData(ref List<string> io_dataList);
        public void GetVehicleData(ref List<string> io_dataList)
        {
            io_dataList.Add("Vehicels model name: " + modelName);
            io_dataList.Add("Vehicels license plate number: " + licenseNumber);
            io_dataList.Add("Energy precent left in vehicle: " + energyPrecentLeft.ToString()+"%");
            GetVehicleTypeData(ref io_dataList);
            io_dataList.Add("The vehicle has: " + m_wheels.Capacity + " wheels");
            m_wheels[0].GetWheeleData(ref io_dataList);
            m_engine.GetEngineData(ref io_dataList);
        }
    }
}
