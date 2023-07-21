namespace ControllerSimulator.Exceptions
{
    public class NoSuchElementException : Exception
    {
        public NoSuchElementException() : base() { }

        public NoSuchElementException(string message) : base(message) { }
    }
}
