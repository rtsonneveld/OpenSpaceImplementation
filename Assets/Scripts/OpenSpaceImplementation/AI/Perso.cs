﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using OpenSpaceImplementation.Animation;
using OpenSpaceImplementation.Object.Properties;

namespace OpenSpaceImplementation.AI {

    [Serializable]
    public partial class Perso : MonoBehaviour {

        // "Always" objects are spawnable
        public bool IsAlways { get; internal set; }

        public Perso3dData Perso3dData = new Perso3dData();

        protected StateMachine smRule;
        protected StateMachine smReflex;

        public CustomBits customBits;

        // Health, air magic
        
        public int HitPoints;
        public int MaxHitPoints;
        
        public int AirPoints;
        public int MaxAirPoints;
        
        public int MagicPoints;
        public int MaxMagicPoints;

        // Used for scripts:
        protected int globalRandomizer;
        protected float timeSinceLastFrame;

        public Perso CreatedBy { get; set; }
        public List<Perso> CreatedAlwaysObjects { get; set; }

        public string ActiveRule
        {
            get
            {
                return smRule?.ActiveState?.ToString();
            }
        }

        public string ActiveReflex
        {
            get
            {
                return smReflex?.ActiveState?.ToString();
            }
        }

        protected virtual void Start()
        {
            smRule = new StateMachine(this);
            smReflex = new StateMachine(this);

            customBits = new CustomBits(32);
        }

        protected async void Update()
        {
            timeSinceLastFrame += Time.deltaTime;
            if (timeSinceLastFrame > 1.0f/60.0f) {
                timeSinceLastFrame = 0.0f;
                globalRandomizer += 1;
            }

            if (!smRule.Busy) {
                await smRule.Update();
            }
            if (!smReflex.Busy) {
                await smReflex.Update();
            }

            // No more delaying of updates
            smRule.ResetDelayUpdate();
            smReflex.ResetDelayUpdate();

        }

        // Delay updates by a frame
        public void DelayUpdate()
        {
            this.smRule?.DelayUpdate();
            this.smReflex?.DelayUpdate();
        }

        public Animation.Action GetAction(int actionIndex)
        {
            return null; // TODO: stub
        }

        public Animation.Module GetModule(int moduleIndex)
        {
            return null; // TODO: stub
        }
    }
}
