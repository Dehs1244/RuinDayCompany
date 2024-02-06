using LethalNetworkAPI;
using RuinDayCompany.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;

namespace RuinDayCompany.Network
{
    internal class GameNetworkSynchronization : ISynchronizationModule<IImpostorGameMode>
    {
        private readonly LethalClientMessage<IImpostorGameMode> _clientImpostorMessage;
        private readonly LethalServerMessage<IImpostorGameMode> _impostorServerMessage;
        
        public GameNetworkSynchronization(string identifier)
        {
            _clientImpostorMessage = new LethalClientMessage<IImpostorGameMode>(identifier);
            _impostorServerMessage = new LethalServerMessage<IImpostorGameMode>(identifier);

            _clientImpostorMessage.OnReceived += RecievedData;
            _impostorServerMessage.OnReceived += OnRecievedFromClient;
        }

        private void OnRecievedFromClient(IImpostorGameMode arg1, ulong clientId)
        {
            Plugin.Instance.CurrentGame = arg1;
            Plugin.Instance.CurrentGame.DisplayIntro();
        }

        private void RecievedData(IImpostorGameMode obj)
        {
            Plugin.Instance.CurrentGame = obj;
            Plugin.Instance.CurrentGame.DisplayIntro();
        }

        public void Synchronize(IImpostorGameMode data)
        {
            if(NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsServer)
            {
                _impostorServerMessage.SendAllClients(data);
            }
        }

        public void Dispose()
        {
        }
    }
}
