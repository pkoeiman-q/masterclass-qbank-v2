using MasterclassApiTest.Entities;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace MasterclassApiTest.Models
{
    public class RekeningDTO
    {
        public string RekeningNummer { get; set; }
        public int KlantNummer { get; set; }
        public string NaamEigenaar { get; set; }
        public string Type { get; set; }
        public string Saldo { get; set; }
        public string Status { get; set; }
        public DateTime BeginDatum { get; set; }
    }
}
