using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine  //electric engine class inherts from engine
    {
        private float m_batteryTimeLeft;
        private float m_maximalBatteryTime;
        public ElectricEngine(List<string> io_vehicleDataMembers)//constractor of electric engine
        {
            this.m_batteryTimeLeft = float.Parse(io_vehicleDataMembers[9]);
            this.m_maximalBatteryTime = float.Parse(io_vehicleDataMembers[10]);
            this.m_IsElectric = true;
        }
        public static void AddElectricEngineQuestions(ref List<string> io_questions)
        {
            io_questions.Add("What is your current battery time left? ");
            io_questions.Add("What is your maximal battery time? ");
        }
        public float batteryTimeLeft
        {
            get { return m_batteryTimeLeft; }
            set { m_batteryTimeLeft = value; }
        }
        
        public override float CalcEnginePrecent()
        {
            float enginePrecent;
            enginePrecent = (batteryTimeLeft / maximalBatteryTime) * 100;
            return enginePrecent;
        }
        public float maximalBatteryTime
        {
            get { return m_maximalBatteryTime; }
            set { m_maximalBatteryTime = value; }
        }
        public override bool ChargeOrFuleVehicle(float i_amountToAdd)//overrid abtract method to charge electric engine
        {
            bool isAmountLegal = true;

            i_amountToAdd /= 60;// the amount to add is recived in minutes so we divide to get it in hours
            if(m_batteryTimeLeft + i_amountToAdd > m_maximalBatteryTime)//if battery amount to add exeeded maximal battery then amount is not legal
            {
                isAmountLegal = false;
            }
            else                                                    // if amount is legal then charge battery
            {
                m_batteryTimeLeft += i_amountToAdd;
            }
                return isAmountLegal;
        }

        public static float CheckUserIputForCurrAndMaxBatteryLife(string i_userInput)
        {
            float batteyLife;
            try
            {
                batteyLife = float.Parse(i_userInput);
            }
            catch(ArgumentException)
            {
                throw;
            }
            return batteyLife;
        }

        public override void GetEngineData(ref List<string> io_dataList)
        {
            io_dataList.Add("Battery time left: " + m_batteryTimeLeft.ToString());
            io_dataList.Add("Maximal battery time:" + m_maximalBatteryTime.ToString());
        }
    }
}
