using RuinDayCompany.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using Newtonsoft.Json;
using UnityEngine;
using RuinDayCompany.Core;

namespace RuinDayCompany.Modules
{
    public class RuinGameModule : NetworkBehaviour
    {
        [field: SerializeField]
        public RuinDayGame Game { get; set; }

        public static RuinGameModule Instance { get; set; }

        public void Awake()
        {
            Instance = this;
        }

        internal void StartGame(RuinDayGame game)
        {
            if(NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsServer)
            {
                Plugin.Log("Starting Ruin Day game");
                Game = game;
                SyncGameServerRpc(JsonConvert.SerializeObject(Game, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                }));
            }
        }

        internal void EndGame()
        {
            Destroy(this);
            Instance = null;
        }

        [ServerRpc(RequireOwnership = false)]
        public void SyncGameServerRpc(string serializedGame)
        {
            SyncGameClientRpc(serializedGame);
        }

        [ClientRpc]
        public void SyncGameClientRpc(string serializedGame)
        {
            Game = JsonConvert.DeserializeObject<RuinDayGame>(serializedGame, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });

            Game.DisplayIntro();

            foreach (var impostor in Game.Crew.Impostors)
            {
                impostor.GiveImpostorGun(Game);
            }
        }
    }
}
