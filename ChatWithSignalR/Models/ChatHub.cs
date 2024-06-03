using ChatWithSignalR;
using ChatWithSignalR.Models;
using Microsoft.AspNetCore.SignalR;

namespace signalR.Models
{
    public class ChatHub :Hub
    {
        private readonly AppDbContext _context;
        public ChatHub(AppDbContext context)
        {
            _context = context;
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task JoinGroup(string Gname , string name)
        {
           await Groups.AddToGroupAsync( Context.ConnectionId , Gname);

           await Clients.OthersInGroup(Gname).SendAsync("NewMember", Gname, name);
            
        }

        public async Task SendToGroup(string groupName , string name , string message)
        {
            await Clients.Group(groupName).SendAsync("SendGroup", name, message, groupName);
        }
        //public async Task SendPrivate(string message, string id, string toid)
        //{
        //    var Sender = _context.Users.FirstOrDefault(e => e.id == int.Parse(id));
        //    var Receiver = _context.Users.FirstOrDefault(e => e.id == int.Parse(toid));
        //    if (Sender != null && Receiver != null)
        //    {
        //        _context.Messages.Add(new Message
        //        {
        //            SenderId = Sender.id,
        //            RecieverId = Receiver.id,
        //            MessageContent = message,
        //            Time = DateTime.UtcNow
        //        });
        //        await _context.SaveChangesAsync();
        //        await Clients.Client(Receiver.ConId).SendAsync("Private", /*Sender.name*/ id, message);
        //    }
        //}
        //public async Task SendMessageToUser(int senderId, int targetUserId, string message)
        //{
        //    var sender = _context.Users.SingleOrDefault(u => u.id == senderId);
        //    var targetUser = _context.Users.SingleOrDefault(u => u.id == targetUserId);

        //    if (targetUser != null && sender != null)
        //    {
        //        var chatMessage = new Message
        //        {
        //            SenderId = sender.id,
        //            RecieverId = targetUser.id,
        //            MessageContent = message,
        //            Time = DateTime.UtcNow
        //        };

        //        _context.Messages.Add(chatMessage);
        //        await _context.SaveChangesAsync();

        //        var connectionId = Context.ConnectionId;
        //        await Clients.Client(connectionId).SendAsync("ReceiveMessageee", sender.id.ToString(), message);
        //        await Clients.User(targetUser.id.ToString()).SendAsync("ReceiveMessageee", sender.id.ToString(), message);
        //    }
        //}

        public async Task SendMessageToUser(int senderId, int targetUserId, string message)
        {
            var sender = _context.Users.SingleOrDefault(u => u.id == senderId); // Corrected the property name to 'Id'
            var targetUser = _context.Users.SingleOrDefault(u => u.id == targetUserId); // Corrected the property name to 'Id'

            if (targetUser != null && sender != null)
            {
                var chatMessage = new Message
                {
                    SenderId = sender.id, // Corrected the property name to 'Id'
                    RecieverId = targetUser.id, // Corrected the property name to 'Id'
                    MessageContent = message,
                    Time = DateTime.UtcNow
                };

                _context.Messages.Add(chatMessage);
                await _context.SaveChangesAsync();

                var connectionId = Context.ConnectionId;
                await Clients.Client(targetUser.ConId).SendAsync("ReceiveMessageee", sender.id.ToString(), message); // Changed 'id' to 'Id'
                await Clients.Client(sender.ConId.ToString()).SendAsync("ReceiveMessageee", sender.id.ToString(), message); // Changed 'id' to 'Id'
            }
        }


        public override Task OnConnectedAsync()
        {
            var userName = Context.GetHttpContext();
                
          //      Request.Query["username"];

            var user = new User
            {
                ConId = Context.ConnectionId,
                
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;

            // Find the user by connection ID
            var user = _context.Users.SingleOrDefault(u => u.ConId == connectionId);

            if (user != null)
            {
                // Remove user from the database
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            await base.OnDisconnectedAsync(exception);
        }

    }
}
