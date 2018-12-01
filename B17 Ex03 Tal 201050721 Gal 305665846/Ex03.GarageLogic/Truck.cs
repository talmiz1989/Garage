using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle  //truck class inherts from vehicle
    {
        private bool m_doesCarryToxicItem;  // boolen variable if the truck contain toxic itrms
        private float m_maxCarryWhight;     // the truck maximal weight
        public Truck(List<string> i_dataMembers, bool i_isElectric, string i_vehicleName) : base(i_dataMembers, false, i_vehicleName)//constractor of the truck
        {
            this.m_doesCarryToxicItem = Truck.ParseBool(i_dataMembers[4]);
            this.m_maxCarryWhight = float.Parse(i_dataMembers[5]);
        }
        public static bool ParseBool(string str)
        {
            if(str.CompareTo("Y") == 0)
            {
                return true;
            }
            else if(str.CompareTo("N")==0)
            {
                return false;
            }
            else
            {
                return false; ;//throw exeption
            }
        }
       
        public static void AddTruckQuestions(ref List<string> io_questions)// this method gets a questions list and add the truck questions to the list
        {
            io_questions.Add("Does your truck contain hazardous material? <Y/N> ");
            io_questions.Add("What is your truck maximal weight capacity? ");
        }

        public static void CheckUserToxicInput(string i_userInput)
        {
            if(i_userInput != "Y" && i_userInput != "N")
            {
                throw new FormatException();
            }
        }

        public static void CheckUserCarryWight(string i_userInput)
        {
            float carryWight;
            try
            {
                carryWight = float.Parse(i_userInput);
            }
            catch(ArgumentException)
            {
                throw;
            }
        }

        public override void GetVehicleTypeData(ref List<string> io_dataList)
        {
            if (m_doesCarryToxicItem == true)
            {
                io_dataList.Add("The truck contain toxic items.");
            }
            else
            {
                io_dataList.Add("The truck does not contain toxic items.");
            }
            io_dataList.Add("Maximal carry whight: " + m_maxCarryWhight.ToString());
        }
    }
}
