﻿using Akka.Actor;
using Game.ActorModel.Actors;
using Game.ActorModel.ExternalSystems;

namespace Game.Web.Models
{
    public static class GameActorSystem
    {
        private static ActorSystem ActorSystem;
        private static IGameEventsPusher _gameEventsPusher;

        public static void Create()
        {
            _gameEventsPusher = new SignalRGameEventPusher();

            ActorSystem = Akka.Actor.ActorSystem.Create("GameSystem");

            ActorReferences.GameController = ActorSystem.ActorOf<GameControllerActor>();

            ActorReferences.SignalRBridge = ActorSystem.ActorOf(
                Props.Create(() => new SignalRBridgeActor(_gameEventsPusher, ActorReferences.GameController)),
                "SignalRBridge"
                );
        }

        public static void ShutDown()
        {
            ActorSystem.Terminate();

            //ActorSystem.WhenTerminated(TimeSpan.FromSeconds(1));
        }


        public static class ActorReferences
        {
            public static IActorRef GameController { get; set; }
            public static IActorRef SignalRBridge { get; set; }
        }
    }
}