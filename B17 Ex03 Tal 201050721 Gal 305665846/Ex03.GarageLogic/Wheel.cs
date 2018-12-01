using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel //wheel of vehicle class
    {
        private string m_manufacturer;//wheel manufacturer name
        private float m_currentPSI; //current air pressure of the wheel
        private float m_maximalPSI;  //maximal air pressure of the wheel
        public Wheel (List<string> i_dataMembers)//constractor of the wheel
        {
            this.manufacturer = i_dataMembers[6];
            this.currentPSI = float.Parse(i_dataMembers[7]);
            this.maximalPSI = float.Parse(i_dataMembers[8]);
        }
        public static void AddWheelQuestions(ref List<string> io_questions)// this method gets a questions list and add the wheel questions to the list
        {
            io_questions.Add("What is your wheels manufacturer name? ");
            io_questions.Add("what is your wheels current air pressure?  ");
            io_questions.Add("what is your wheels maximal air pressure?  ");
        }
        public string manufacturer
        {
            get { return m_manufacturer; }
            set { m_manufacturer = value; }
        }
        public float currentPSI
        {
            get { return m_currentPSI; }
            set { m_currentPSI = value; }
        }
        public float maximalPSI
        {
            get { return m_maximalPSI; }
            set { m_maximalPSI = value; }
        }
        public void InflateWheelToMax()//this method inflates the wheel
        {
            currentPSI = maximalPSI;
        }

        public static void CheckWheelManufacturer(string i_userInput)
        {
            if(System.Text.RegularExpressions.Regex.IsMatch(i_userInput, @"^[a-zA-Z ]+$") == false)
            {
                throw new FormatException();
            }
        }

        public static float CheckUserInputCuurAndMaxAirPressure(string i_userInput)
        {
            float airPressure;
            try
            {
                airPressure = float.Parse(i_userInput);
            }
            catch
            {
                throw;
            }
            return airPressure;
        }

        public void GetWheeleData(ref List<string> io_dataList)
        {
            io_dataList.Add("Wheels manufacturer name: " + manufacturer);
            io_dataList.Add("Current PSI of wheels: " + currentPSI.ToString());
            io_dataList.Add("Maximal PSI of wheels: " + maximalPSI.ToString());
        }
    }
}
