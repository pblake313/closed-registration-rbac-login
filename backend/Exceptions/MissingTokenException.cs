namespace SAConstruction.Exceptions
{
    public class MissingTokenException : Exception
    {
        public MissingTokenException() 
            : base("Access token is missing.") { }

        public MissingTokenException(string message) 
            : base(message) { }
    }
}
