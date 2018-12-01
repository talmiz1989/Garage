using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
     public abstract class Engine
    {
        public bool m_IsElectric;
        public abstract bool ChargeOrFuleVehicle(float i_amountToAdd);//abstract method to charge or fuel engine
        public static void AddEngineQuestions(ref List<string> io_questions, bool io_isElectric)// add engine questions to questions list  
        {
            if(io_isElectric == false)
            {
                GasolineEngine.AddGasolineEngineQuestions(ref io_questions);//add gasoline questions
            }
            else
            {
                ElectricEngine.AddElectricEngineQuestions(ref io_questions);//add electric questions
            }
        }
        public bool IsElectric
        {
            get { return m_IsElectric; }
            set { m_IsElectric = value; }
        }
        public abstract float CalcEnginePrecent();
        public abstract void GetEngineData(ref List<string> io_dataList);
    }
}
