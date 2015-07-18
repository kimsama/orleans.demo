﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Orleans.Providers;

namespace PlayerProgression
{
    public class PlayerState : GrainState
    {
        public long Kills { get; set; }
        public long Death { get; set; }
        public long Experience { get; set; }
    }

    [StorageProvider(ProviderName = "TestStore")]
    public class Player : Grain<PlayerState>, IPlayerGrain
    {
        private IGameGrain currentGame;
        private Progression previous;

        public Task<IGameGrain> GetGame()
        {
            return Task.FromResult(currentGame);
        }
        public Task JoinGame(IGameGrain game)
        {
            currentGame = game;
            Console.WriteLine("Player {0} joined game {1}", this.GetPrimaryKeyLong(), game.GetPrimaryKey());

            if (previous == null)
            {
                previous = new Progression();
            }
            return TaskDone.Done;
        }
        public Task LeaveGame(IGameGrain game)
        {
            currentGame = null;
            Console.WriteLine("Player {0} left game {1}", this.GetPrimaryKey(), game.GetPrimaryKey());

            // TODO: Have to consider a player leave a game during a running game.
            previous = null;

            return TaskDone.Done;
        }
        public Task Progress(Progression data)
        {
            Console.WriteLine("Player.Progress called for player {0}", this.GetPrimaryKey());

            State.Kills += data.Kills - previous.Kills;
            State.Death += data.Death - previous.Death;
            State.Experience += data.Experience - previous.Experience;

            previous.Kills = data.Kills;
            previous.Death = data.Death;
            previous.Experience = data.Experience;

            return WriteStateAsync();
        }
    }
}
