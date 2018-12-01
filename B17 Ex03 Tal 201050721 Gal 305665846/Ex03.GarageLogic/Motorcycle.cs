using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle //motorcycle class inherts from vehicle
    {
        private enum elicenceType {A,AB,A2,B1 }; //type of license of the motorcycle
        private elicenceType m_licenceType;
        private int m_enginevolume; //tho motorcycle engine volume
        public static object elicenceTypeParse(string i_strToEnum)
        {
            try
            {
                elicenceType resEnum = (elicenceType)Enum.Parse(typeof(elicenceType), i_strToEnum);
                return resEnum;
            }
            catch (ArgumentException)
            {
                throw;
            }
        }       
        public Motorcycle(List<string> i_dataMembers, bool i_isElectric, string i_vehicleName) : base(i_dataMembers, i_isElectric, i_vehicleName)//constractor of the motorcycle
        {
            this.m_licenceType =(elicenceType)elicenceTypeParse(i_dataMembers[4]);
            this.m_enginevolume = int.Parse(i_dataMembers[5]);
        }
        public static void AddMotorcycleQuestions(ref List<string> io_questions)// this method gets a questions list and add the motorcycle questions to the list
        {
            io_questions.Add("What is your motorcycle licence type? <A,AB,A2,B1> ");
            io_questions.Add("What is your motorcycle engine volume? ");
        }
       
        public static void CheckLiceseType(string i_userInput)
        {
            if(i_userInput != "A" && i_userInput != "AB" && i_userInput != "A2" && i_userInput != "B1")
            {
                throw new FormatException();
            }
        }

        public static void CheckEngineVolume(string i_userInput)
        {
            int enginVolume;
            try
            {
                enginVolume = int.Parse(i_userInput);
            }
            catch(ArgumentException)
            {
                throw;
            }
        }

        public override void GetVehicleTypeData(ref List<string> io_dataList)
        {
            io_dataList.Add("License type: " + m_licenceType.ToString());
            io_dataList.Add("Engine volume: " + m_enginevolume.ToString());
        }
    }
}
