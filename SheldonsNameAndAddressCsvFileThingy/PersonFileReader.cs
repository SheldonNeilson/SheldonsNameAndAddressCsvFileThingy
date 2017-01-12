namespace SheldonsNameAndAddressCsvFileThingy
{
    public sealed class PersonFileReader : FileReader<Person>
    {               
        public PersonFileReader(IParser<Person,string> personParser) : base(personParser)
        {
            
        }        
    }
}
