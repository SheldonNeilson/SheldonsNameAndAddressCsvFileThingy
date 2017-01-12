namespace SheldonsNameAndAddressCsvFileThingy
{
    public struct Address
    {
        public uint StreetNumber { get; set; }
        public string StreetName { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", StreetNumber, StreetName);
        }
    }
}