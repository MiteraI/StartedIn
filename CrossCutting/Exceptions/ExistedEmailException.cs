namespace CrossCutting.Exceptions
{
    public class ExistedEmailException  : Exception {  
        public ExistedEmailException(string message) : base(message)
        {
            
        }
    }
}
