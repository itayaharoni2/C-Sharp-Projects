namespace EX03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;
        private float m_Value;

        public ValueOutOfRangeException(float i_Value, float i_MinValue, float i_MaxValue)
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
            m_Value = i_Value;
        }

        public override string Message
        {
            get
            {
                return String.Format("{0} is out of range. Minimum value allowed: {1}, Maximum value allowed: {2}.", m_Value, m_MinValue, m_MaxValue);
            }
        }
    }
}
