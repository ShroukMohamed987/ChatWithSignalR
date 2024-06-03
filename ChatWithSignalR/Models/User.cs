using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChatWithSignalR.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string ConId { get; set; }
        //public string Name { get; set; }
        // public string Name { get; set; }
        public List<Message> MessagesSend { get; set; }
        public List<Message> MessagesRecive { get; set; }
    }
}
