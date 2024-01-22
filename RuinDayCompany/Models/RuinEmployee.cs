﻿using GameNetcodeStuff;
using RuinDayCompany.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinDayCompany.Models
{
    public class RuinEmployee : IRuinCrewmate
    {
        private readonly string _name;
        public string Name => _stuffPlayer != null ? _stuffPlayer.playerUsername : _name;
        private readonly PlayerControllerB _stuffPlayer;

        public RuinEmployee(PlayerControllerB gameStuffController)
        {
            _stuffPlayer = gameStuffController;
        }

        public RuinEmployee(IRuinCrewmate crewmate)
        {
            if (crewmate is RuinEmployee ruinDayEmployee)
            {
                _stuffPlayer = ruinDayEmployee._stuffPlayer;
            }
            else
            {
                _name = crewmate.Name;
            }
        }
        //private PlayerControllerB
    }
}