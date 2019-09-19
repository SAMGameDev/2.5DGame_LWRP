﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class AttackInfo : MonoBehaviour
    {
        public CharacterControl Attacker = null;
        public Attack AttackAbility;
        public List<string> ColliderNames = new List<string>();
        public bool LaunchintoAir;

        public bool MustCollide;
        public bool MustFaceAttacker;
        public float LethalRange;
        public int MaxHits;
        public int CurrentHits;
        public bool isRegisterd;
        public bool isFinsihed;

        public void ResetInfo(Attack attack, CharacterControl attacker)
        {
            isRegisterd = false;
            isFinsihed = false;
            AttackAbility = attack;
            Attacker = attacker;
        }
        public void Register(Attack attack)
        {
            isRegisterd = true;

            AttackAbility = attack;
            ColliderNames = attack.ColliderNames;
            LaunchintoAir = attack.LaunchintoAir;
            MustCollide = attack.MustCollide;
            MustFaceAttacker = attack.MustFaceAttacker;
            LethalRange = attack.LethalRange;
            MaxHits = attack.MaxHits;
            CurrentHits = 0;
        }

        private void OnDisable()
        {
            isFinsihed = true;
        }
    }
}
