namespace _2k19Extractor
{
    public class Offset
    {
        //if not all data can be gathered from the base player stat, we may need to add which base to use in order to have both base and offset
        public string Name;
        public int OffsetInt;
        public StatDataType Type;

        public Offset(string name, int offsetInt, StatDataType type)
        {
            Name = name;
            OffsetInt = offsetInt;
            Type = type;
        }
    }
}
