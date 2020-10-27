namespace Common.MicroTests
{
    public static class SuppressedNullable
    {
        public static T Default<T>() => default!;

        public static T Null<T>() where T : class => null!;

        public static object NullObject => null!;

        public static string NullString => null!;
    }
}
