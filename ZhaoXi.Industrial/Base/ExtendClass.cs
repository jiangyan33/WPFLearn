namespace ZhaoXi.Industrial.Base
{
    public static class ExtendClass
    {
        public static float ByteArrayToFloat(this byte[] value)
        {
            float fValue = 0f;
            uint nRest = ((uint)value[2]) * 256
                + ((uint)value[3]) +
                65536 * (((uint)value[0]) * 256 + ((uint)value[1]));
            unsafe
            {
                float* ptemp;
                ptemp = (float*)(&nRest);
                fValue = *ptemp;
            }
            return fValue;
        }
    }
}
