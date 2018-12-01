using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GasolineEngine : Engine  //gasoline engine class inherts from engine class
    {
        public enum eGasType { Soler, Octan95, Octan96, Octan98 };// type of gas enum
        private eGasType m_typeOfGas;
        private float m_currentGasAmount;//current gas amount in engine
        private float m_maximalGasAmount;//maximal gas amount in engine
        public static eGasType Parse(string i_strToEnum)
        {
            if ((i_strToEnum.CompareTo("Soler") != 0) && (i_strToEnum.CompareTo("Octan95") != 0) && (i_strToEnum.CompareTo("Octan96") != 0) && (i_strToEnum.CompareTo("Octan98") != 0) )
            {
                throw new FormatException();//Please try to check this condition
            }
            try
            {
                eGasType resEnum = (eGasType)Enum.Parse(typeof(eGasType), i_strToEnum);
                return resEnum;
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public void CheckFuelType(string i_gasType)
        {
            eGasType gasTypeAsEnum;
            // gasType = Parse(i_userInput);
            //Soler, Octan95, Octan96, Octan98
            if(i_gasType == "Soler" || i_gasType == "Octan95" || i_gasType == "Octan96" || i_gasType == "Octan98")
            {
                gasTypeAsEnum = Parse(i_gasType);
                if(gasTypeAsEnum != gasType )
                {
                    Console.Write(Environment.NewLine);
                    throw new ArgumentException();
                }                
            }
            else
            {
                throw new FormatException();
            }
        }
        public eGasType gasType
        {
            get { return m_typeOfGas; }
            set { m_typeOfGas = value; }
        }

        public float currentGasAmount
        {
            get { return m_currentGasAmount; }
            set { m_currentGasAmount = value; }
        }
        public override float CalcEnginePrecent()
        {
            float enginePrecent;
            enginePrecent = (currentGasAmount / maximalGasAmount) * 100;
            return enginePrecent;
        }
        public float maximalGasAmount
        {
            get { return m_maximalGasAmount; }
            set { m_maximalGasAmount = value; }
        }
        public GasolineEngine(List<string> io_vehicleDataMembers)//constractor of gasoline engine
        {
            this.m_typeOfGas = (eGasType)Parse(io_vehicleDataMembers[9]);
            this.m_currentGasAmount = float.Parse(io_vehicleDataMembers[10]);
            this.m_maximalGasAmount = float.Parse(io_vehicleDataMembers[11]);
            this.m_IsElectric = false;
        }
        public override bool ChargeOrFuleVehicle(float i_amountToAdd)// override abstract method to put gas in the engine
        {
            bool isAmountLegal = true;

            if (m_currentGasAmount + i_amountToAdd > m_maximalGasAmount)// if the amount to add exeeded the maximal engine amount
            {
                isAmountLegal = false;
            }
            else                                                         // if the amount is legal the fuel the engine
            {
                m_currentGasAmount += i_amountToAdd;
            }
            return isAmountLegal;
        }
        public static void AddGasolineEngineQuestions(ref List<string> io_questions)// this method gets a questions list and add the gasoline engine questions to the list
        {
            io_questions.Add("What is your Gasoline type?   <Soler, Octan95, Octan96, Octan98>  ");
            io_questions.Add("What is your current gasoline amount ? ");
            io_questions.Add("What is your maximal gasoline amount ? ");
         }

        public static float CheckUserInputForCurrAndMaxGasAmount(string i_userInput)
        {
            float gasAmount;
            try
            {
                gasAmount = float.Parse(i_userInput);
            }
            catch(FormatException)
            {
                throw;
            }
            return gasAmount;
        }

        public static void CheckIGasTypeIsValid(string i_userInput)
        {
            eGasType gasType;

            try
            {
                gasType = Parse(i_userInput);
            }
            catch(ArgumentException)
            {
                throw;
            }
        }

        public override void GetEngineData(ref List<string> io_dataList)
        {
            io_dataList.Add("Gas type: " + m_typeOfGas.ToString());
            io_dataList.Add("Current gas amount: " + m_currentGasAmount.ToString());
            io_dataList.Add("Maximal gas amount: " + m_maximalGasAmount.ToString());
        }
    }
}