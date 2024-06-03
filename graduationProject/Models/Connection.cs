using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace graduationProject.Models
{
    public class Connection
    {
        public int Id { get; set; }
        public ApplicationUser Sender { get; set; }
        public ApplicationUser Receiver { get; set; }
        public bool IsConnected { get; set; }
    }
}
