namespace MasterclassApiTest.Exceptions
{
    public class KlantNotFoundException : Exception
    {
        public int? Id;
        public KlantNotFoundException()
        {
            
        }
        public KlantNotFoundException(int id)
        {
            Id = id;
        }
        public KlantNotFoundException(string message) : base(message)
        {
            
        }
        public KlantNotFoundException(string message, Exception inner) : base(message, inner)
        {
            
        }
    }
}
