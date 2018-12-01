using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle  //class car that inherts from vehicle
    {
        public enum eColor {Yellow,White,Black,Blue };// color types for the vehicle
        private eColor m_carColor;
        public enum edoorNum {Two=2,Three,Four,Five };// number of doors in the car
        private edoorNum m_doorNumber;
        public static eColor eColorParse(string i_strToEnum)
        {
            if(System.Text.RegularExpressions.Regex.IsMatch(i_strToEnum, @"^[a-zA-Z]+$") == false)
            {
                throw new FormatException();
            }
            try   
            {
                eColor resEnum = (eColor)Enum.Parse(typeof(eColor), i_strToEnum);
                return resEnum;
            }
            catch(ArgumentException)
            {
                throw;
            }
        }
        public static edoorNum edoorNumParse(string i_strToEnum)
        {
            try
            {            
                edoorNum resEnum = (edoorNum)Enum.Parse(typeof(edoorNum), i_strToEnum);
                if(resEnum < edoorNum.Two || resEnum > edoorNum.Five)
                {
                    throw new ArgumentException();
                }
                return resEnum;
            }
            catch (ArgumentException)
            {
                throw;
            }
        }
        public eColor carColor
        {
            get { return m_carColor; }
            set { m_carColor = value; }
        }
        public edoorNum doorNumber
        {
            get { return m_doorNumber; }
            set { m_doorNumber = value; }
        }
        //public override void Print(Vehicle i_car)
        //{
            
        //}

        public Car(List<string> i_dataMembers,bool i_isElectric,string i_vehicleName) :base(i_dataMembers, i_isElectric, i_vehicleName)//constractor of the car
        {
            this.m_carColor =(eColor)eColorParse(i_dataMembers[4]);
            this.m_doorNumber = (edoorNum)edoorNumParse(i_dataMembers[5]);
        }
        public static void AddCarQuestions(ref List<string> io_questions)// this method gets a questions list and add the car questions to the list
        {
            io_questions.Add("What is your car color?   <Yellow,White,Black,Blue> ");
            io_questions.Add("How many doors does your car have?    <Two,Three,Four,Five> ");
        }
        public static void CheckIfDoorsIsValid(string i_userInput)
        {
           
            edoorNum doorNumber;
            try
            {
                doorNumber = Car.edoorNumParse(i_userInput);
            }
            catch(ArgumentException)
            {
                throw;
            }
        }

        public static void CheckUserColorInput(string i_userInput)
        {
            eColor color;
            try
            {
                color = eColorParse(i_userInput);
            }
            catch(ArgumentException)
            {
                throw;
            }
        }

        public override void GetVehicleTypeData(ref List<string> io_dataList)
        {
            io_dataList.Add("Color: " + carColor.ToString());
            io_dataList.Add("Door number: " + doorNumber.ToString());
        }
    }
}
