using GameNetcodeStuff;
using Newtonsoft.Json;
using RuinDayCompany.Interfaces;
using RuinDayCompany.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;

namespace RuinDayCompany.Models
{
    public class RuinEmployee : IRuinCrewmate
    {
        private readonly string _name;
        public string Name => _stuffPlayer != null ? _stuffPlayer.playerUsername : _name;

        public bool IsLocal => StartOfRound.Instance.localPlayerController == _stuffPlayer;

        private PlayerControllerB _cachedStuffPlayer;
        private PlayerControllerB _stuffPlayer
        {
            get
            {
                if (_cachedStuffPlayer != null) return _cachedStuffPlayer;
                _cachedStuffPlayer = StartOfRound.Instance.allPlayerScripts[_playerId];

                return _cachedStuffPlayer;
            }
        }

        [JsonProperty("playerId")]
        private int _playerId;

        [JsonConstructor]
        public RuinEmployee()
        {
        }

        public RuinEmployee(int playerId)
        {
            _playerId = playerId;
        }

        public RuinEmployee(IRuinCrewmate crewmate)
        {
            if (crewmate is RuinEmployee ruinDayEmployee)
            {
                _cachedStuffPlayer = ruinDayEmployee._cachedStuffPlayer;
                _playerId = ruinDayEmployee._playerId;
            }
            else
            {
                _name = crewmate.Name;
            }
        }

        public static IEnumerable<IRuinCrewmate> CreateFromGamePlayers(IEnumerable<PlayerControllerB> players)
        {
            for(var i = 0; i < players.Count(); i++)
            {
                yield return new RuinEmployee(i) { _cachedStuffPlayer = players.ElementAt(i) };
            }
        }

        public void GiveItem(GrabbableObject item)
        {
            item.InteractItem();
            item.parentObject = _stuffPlayer.localItemHolder;

            NetworkObjectReference networkObjectReference = item.NetworkObject;
            ReflectionHelper.InvokePrivateMethod(_stuffPlayer, "GrabObjectServerRpc", networkObjectReference);
        }

        public int FindEmptySlot()
        {
            if (_stuffPlayer.ItemSlots[_stuffPlayer.currentItemSlot] == null) return _stuffPlayer.currentItemSlot;
            else
            {
                for (int i = 0; i < this._stuffPlayer.ItemSlots.Length; i++)
                {
                    if (_stuffPlayer.ItemSlots[i] == null)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public void SwitchToEmptySlot() => SwitchToSlot(FindEmptySlot());

        public void SwitchToSlot(int index)
        {
            if (index == -1) return;

            ReflectionHelper.InvokePrivateMethod(_stuffPlayer, "SwitchToItemSlot", index, null);
        }
        //private PlayerControllerB
    }
}
