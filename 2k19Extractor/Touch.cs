using System;


namespace _2k19Extractor
{
    [Serializable] 
    public class Touch
    {
        public int Quarter;
        public Single StartTime;
        public Single EndTime;

        public Touch(int quarter, Single startTime)
        {
            Quarter = quarter;
            StartTime = startTime;
        }

        public Single TimeOfTouch
        {
            get { return StartTime - EndTime; }
        }
    }
}
