using Akka.Actor;
using Game.ActorModel.Messages;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Game.Web.Models
{
    [HubName("gameHub")]
    public class GameHub : Hub
    {
        public void JoinGame(string playerName)
        {
            GameActorSystem
                .ActorReferences
                .SignalRBridge
                .Tell(new JoinGameMessage(playerName));
        }

        public void Attack(string playerName)
        {
            GameActorSystem
                .ActorReferences
                .SignalRBridge
                .Tell(new AttackPlayerMessage(playerName));
        }

    }
}