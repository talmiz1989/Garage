using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeExeption : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;
        private string m_StringToTheUser;

        public ValueOutOfRangeExeption(
           Exception i_InnerException,
            float i_maxvalue,
            float i_minvalue,
            string i_StringToTheUsere)
            : base(
            string.Format("{0} {1} {2} ", i_minvalue, i_StringToTheUsere, i_maxvalue, i_InnerException))
        {
            m_MaxValue = i_maxvalue;
            m_MinValue = i_minvalue;
            m_StringToTheUser = i_StringToTheUsere;
        }

        public ValueOutOfRangeExeption(
            float i_maxvalue,
            float i_minvalue,
            string i_StringToTheUsere)
            : base(
            string.Format("{0} {1} {2} ", i_minvalue, i_StringToTheUsere, i_maxvalue))
        {
            m_MaxValue = i_maxvalue;
            m_MinValue = i_minvalue;
            m_StringToTheUser = i_StringToTheUsere;
        }
        public float MaxValue
        {
            get { return m_MaxValue; }
            set { m_MaxValue = value; }
        }
        public float MinValue
        {
            get { return m_MinValue; }
            set { m_MinValue = value; }
        }
    }
}
