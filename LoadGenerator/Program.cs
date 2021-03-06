﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;
using PlayerProgression.Player;

namespace LoadGenerator
{
    class Generator
    {
        private int startIndex;
        private int numberOfPlayers;

        public Generator(int startIndex, int numberOfPlayers)
        {
            this.startIndex = startIndex;
            this.numberOfPlayers = numberOfPlayers;
        }

        private async Task<Guid> QuickMatch(int playerId)
        {
            IPlayerGrain player = GrainClient.GrainFactory.GetGrain<IPlayerGrain>(playerId);
            Guid roomId = await player.QuickMatch();
            Console.WriteLine("Player {0} joined game room {1}", playerId, roomId);
            return roomId;
        }

        public async void Start()
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                await QuickMatch(i + startIndex);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GrainClient.Initialize("DevTestClientConfiguration.xml");

            if (args.Length != 2) {
                throw new ArgumentException("Wrong number of arguments");
            }

            int startIndex = Convert.ToInt32(args[0]);
            int numberOfPlayers = Convert.ToInt32(args[1]);
            Generator generator = new Generator(startIndex, numberOfPlayers);
            generator.Start();
            

            Console.ReadLine();
        }
    }
}
