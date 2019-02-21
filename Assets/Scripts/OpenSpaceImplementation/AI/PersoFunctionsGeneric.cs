using OpenSpaceImplementation.Strings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenSpaceImplementation.AI {

    // Generic

    public partial class Perso {

        protected static bool u64 = false; // Nintendo 64 (Ultra 64)

        public async Task TIME_FrozenWait(int milliseconds)
        {
            await new WaitForSeconds(((float)milliseconds) / 1000.0f);
        }

        public void Proc_DesactivateObject(Perso perso)
        {
            perso.gameObject.SetActive(false);
            // TODO: test
        }

        public Perso Func_GenerateObject(Perso basePerso, Vector3 position)
        {
            return Func_GenerateObject(basePerso.GetType(), position);
        }

        static int instanceCounter = 0;

        public Perso Func_GenerateObject(Type persoType, Vector3 position)
        {
            GameObject gameObject = new GameObject("Instanciated_"+persoType.Name+"_"+instanceCounter);
            instanceCounter++;
            Perso perso = (Perso)gameObject.AddComponent(persoType);
            perso.DelayUpdate();

            return perso;
        }

        public Perso GetPerso(string name)
        {
            return GameObject.Find(name)?.GetComponent<Perso>();
        }

        public T GetPerso<T>(string name) where T:Perso
        {
            Perso perso = this.GetPerso(name);
            if (perso != null && perso is T) {
                return (T)perso;
            } else {
                throw new Exception("Object "+perso+" is not an instance of the requested type");
            }
        }

        public bool Cond_IsTimeElapsed(int timerVariable, int timeInMilliseconds)
        {
            int globalTimer = Func_GetTime();
            int addedTimers = 0;

            if (globalTimer < timerVariable) {
                addedTimers = timerVariable - globalTimer;
            } else {
                addedTimers = globalTimer - timerVariable;
            }

            return addedTimers >= timeInMilliseconds;
        }

        public int Func_GetTime()
        {
            // Return time since level load in milliseconds
            return (int)(Time.timeSinceLevelLoad * 1000);
        }

        public float Func_GetDeltaTime() // delta time as int is gross
        {
            return Time.deltaTime * 1000.0f;
        }

        public void fn_p_stKillPersoAndClearVariableProcedure1(Perso perso)
        {
            if (perso != null && perso.gameObject != null) {
                GameObject.Destroy(perso.gameObject);
            }
        }

        public Vector3 Position()
        {
            return this.transform.position;
        }

        public void Proc_ChangeOneCustomBit(int index, DsgVarBool value)
        {
            // Rayman 2 uses indexes here that start with 1 instead of 0
            customBits.SetCustomBit(index - 1, value);
        }

        public bool Cond_IsCustomBitSet(int index)
        {
            // Rayman 2 uses indexes here that start with 1 instead of 0
            return customBits.GetCustomBit(index - 1);
        }

        public bool Cond_IsValidObject(Perso perso)
        {
            return perso != null && perso.gameObject != null;
        }

        public int ACT_GetNumberOfBooleanInArray(List<DsgVarInt> array, int startIndex, int endIndex)
        {
            // TODO test
            int numberOfSetBits = 0;

            for (int i=startIndex;i<endIndex;i++) {
                if (ACT_GetBooleanInArray(array, i)) {
                    numberOfSetBits++;
                }
            }

            return numberOfSetBits;
        }

        public bool ACT_GetBooleanInArray(List<DsgVarInt> array, int index)
        {
            int arrayItem = array[(int)(index / 32)];
            int bitNumber = index % 32;
            return ((arrayItem & (1 << bitNumber - 1)) != 0 ? true : false);

        }

        public void MAP_GetUsedExitIdentifier(DsgVarInt a, DsgVarInt b)
        {
            return; // TODO, stub
        }

        public void ACT_SetBrainComputeFreq(int frequency)
        {
            return; // TODO, stub
        }

        public void ActivateMenuMap(int activateMenu)
        {
            return; // TODO, stub
        }

        public void Proc_HierFreezeEngine(int freezeEngine)
        {
            return; // TODO, stub
        }

        public void TEXT_FormatText(string text, TextReference a, TextReference b)
        {
            return; // TODO, stub
        }

        public int Func_GetHitPointsMax(Perso perso)
        {
            return perso.MaxHitPoints;
        }

        public int Func_GetHitPoints(Perso perso)
        {
            return perso.HitPoints;
        }

        public void Proc_SetHitPointsMax(Perso perso, int MaxHitPoints)
        {
            perso.MaxHitPoints = MaxHitPoints;
        }

        public void Proc_SetHitPoints(Perso perso, int HitPoints)
        {
            perso.HitPoints = HitPoints > MaxHitPoints ? MaxHitPoints : HitPoints;
        }

        public void Proc_ChangeMap(string mapName)
        {
            return; // TODO, stub
        }
    }
}
