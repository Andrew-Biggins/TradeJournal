namespace Common.MicroTests
{
    public interface ICatcher<in T>
    {
        void Catch(object? sender, T e);
    }
}