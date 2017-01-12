namespace SheldonsNameAndAddressCsvFileThingy
{
    public interface IParser<R, A>
    {
        R Parse(A input);
    }
}
