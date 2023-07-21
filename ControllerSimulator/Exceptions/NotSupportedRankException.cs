namespace ControllerSimulator.Exceptions
{
    public class NotSupportedRankException : Exception
    {
        public NotSupportedRankException() : base() { }

        public NotSupportedRankException(string message) : base(message) { }
    }
}
