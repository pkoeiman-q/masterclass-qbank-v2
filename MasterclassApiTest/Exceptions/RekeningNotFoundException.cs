namespace MasterclassApiTest.Exceptions
{
    public class RekeningNotFoundException : Exception
    {
        public int KlantId;
        public string RekeningId;

        public RekeningNotFoundException()
        {

        }
        public RekeningNotFoundException(int klantId, string rekeningId)
        {
            KlantId = klantId;
            RekeningId = rekeningId;
        }
        public RekeningNotFoundException(string message) : base(message)
        {

        }
        public RekeningNotFoundException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
