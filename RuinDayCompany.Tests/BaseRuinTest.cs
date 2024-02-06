using GameNetcodeStuff;
using HarmonyLib;
using RuinDayCompany.Interfaces;
using RuinDayCompany.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RuinDayCompany.Tests
{
    public abstract class BaseRuinTest
    {
        protected Plugin _plugin;

        public BaseRuinTest()
        {
            _plugin = TestableObjectFactory.CreateTestObject<Plugin>();
            var pluginType = _plugin.GetType();
            _plugin.InitLogger(Mock.Of<IRuinLogger>());

            var awakeMethod = _plugin.GetType().GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic);
            awakeMethod?.Invoke(_plugin, Array.Empty<object>());

            var startOfRound = TestableObjectFactory.CreateTestObject<StartOfRound>();
            startOfRound.GetType().GetProperty("Instance")!.SetValue(startOfRound, startOfRound);

            PlayerControllerB localPlayer = TestableObjectFactory.CreateTestObject<PlayerControllerB>();
            StartOfRound.Instance.localPlayerController = localPlayer;
        }

        protected void _CreateFakeUnityPlayers(int count)
        {
            StartOfRound.Instance.allPlayerScripts = new PlayerControllerB[count];

            for(var i = 0; i < count; i++)
            {
                StartOfRound.Instance.allPlayerScripts[i] = TestableObjectFactory.CreateTestObject<PlayerControllerB>();
                StartOfRound.Instance.allPlayerScripts[i].playerUsername = "Test player" + (i + 1);
            }
        }
    }
}
