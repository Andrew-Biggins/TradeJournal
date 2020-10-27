using Xunit;

namespace Common.MicroTests
{
    public sealed class Gwt : FactAttribute
    {
        public Gwt(string given, string when, string then)
        {
            DisplayName = $"{given}, {when}, {then}.";
        }
    }

    public sealed class GwtTheory : TheoryAttribute
    {
        public GwtTheory(string given, string when, string then)
        {
            DisplayName = $"{given}, {when}, {then}.";
        }
    }
}
