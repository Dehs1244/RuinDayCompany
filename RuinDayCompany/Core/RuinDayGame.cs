using GameNetcodeStuff;
using Newtonsoft.Json;
using RuinDayCompany.Interfaces;
using RuinDayCompany.Models;
using RuinDayCompany.Modules;
using RuinDayCompany.Utils;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace RuinDayCompany.Core
{
    public class RuinDayGame : IImpostorGameMode
    {
        [JsonProperty("crew")]
        public InfestedCrew Crew { get; private set; }

        [JsonConstructor]
        public RuinDayGame()
        {

        }

        public RuinDayGame(InfestedCrew crew)
        {
            Crew = crew;
        }

        public RuinDayGame(IEnumerable<PlayerControllerB> gameStuffPlayers)
        {
            var shuffle = new CrewShuffler(RuinEmployee.CreateFromGamePlayers(gameStuffPlayers));

            Crew = shuffle.Shuffle();
        }

        public T SpawnObject<T>(string itemName)
            where T : GrabbableObject
        {
            foreach(var item in StartOfRound.Instance.allItemsList.itemsList)
            {
                Plugin.Log(item.itemName);
            }

            GameObject gameObject = Object.Instantiate
                (StartOfRound.Instance.allItemsList.itemsList.First(x=> x.itemName == itemName).spawnPrefab, 
                Vector3.zero, Quaternion.identity, StartOfRound.Instance.propsContainer);
            gameObject.GetComponent<NetworkObject>().Spawn(false);
            var grabbable = gameObject.GetComponent<T>();
            grabbable.fallTime = 0;

            return grabbable;
        }

        public void DisplayIntro()
        {
            if(Crew.IsImpostor(Crew.CurrentLocalCrewmate))
            {
                Plugin.SendLocalMessage(Properties.Resources.YouImpostor);
                Plugin.Instance.Translator.LocalTransmitMessage(Properties.Resources.Impostor);
            }
            else
            {
                Plugin.SendLocalMessage(Properties.Resources.YouCrewmate);
            }

            HUDManager.Instance.DisplayTip("It is Ruin Day!!!", "С днем рождения.");
        }
    }
}
