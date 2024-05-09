using ChatAppServer.WebAPI.Contex;
using ChatAppServer.WebAPI.Enums;
using ChatAppServer.WebAPI.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatAppServer.WebAPI.Hubs;

public sealed class ChatHub(ApplicationDbContex contex):Hub
{
    public static Dictionary<string, int> Users = new();

    public async Task Connect(int userId)
    {
        if(Users.Any(p=>p.Value== userId))
        {
           string key= Users.First(p => p.Value == userId).Key;
            Users.Remove(key);
        }
      

        Users.Add(Context.ConnectionId, userId);
        
        User? user = await contex.Users.FindAsync(userId);
        if (user is null) return;

        user.Status = UserStatusEnum.Online;
        contex.Users.Update(user);
        await contex.SaveChangesAsync();

        await Clients.All.SendAsync("Users", user);


    }
}
