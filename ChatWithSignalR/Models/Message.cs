namespace ChatWithSignalR.Models
{
    public class Message
    {
        public int id { get; set; }
        public int SenderId { get; set; }
        public int RecieverId { get; set; }
        public string MessageContent { get; set; }
        public DateTime Time { get; set; }
    }
}
