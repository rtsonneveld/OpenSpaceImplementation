using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenSpaceImplementation {

    public partial class Perso {

        public void fn_p_stJFFTXTProcedure(int textIndex, Vector3 position, string text, DsgVarByte alpha)
        {
            Rayman2Text.DrawText(textIndex, position, text, alpha);
        }

        public int Func_RandomInt(int v1, int v2)
        {
            return UnityEngine.Random.Range(v1, v2);
        }

        public float Func_Sinus(float a)
        {
            return (float)Math.Sin((a / 180) * Math.PI);
        }

        public float Func_Cosinus(float a)
        {
            return (float)Math.Cos((a / 180) * Math.PI);
        }

        public async Task TIME_FrozenWait(int milliseconds)
        {
            await new WaitForSeconds(((float)milliseconds) / 1000.0f);
        }

        int counter = 0;

        public Perso Func_GenerateObject(Perso basePerso, Vector3 position)
        {
            return Func_GenerateObject(basePerso.GetType(), position);
        }

        public Perso Func_GenerateObject(Type persoType, Vector3 position)
        {
            GameObject gameObject = new GameObject("Instanciated_"+persoType.Name+"_"+counter);
            counter++;
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

        public float VEC_GetVectorNorm(Vector3 vector3)
        {
            return vector3.magnitude;
        }

        public int Func_GetTime()
        {
            // Return time since level load in milliseconds
            return (int)(Time.timeSinceLevelLoad * 1000);
        }

        public Vector3 Func_Normalize(Vector3 vector3)
        {
            return vector3.normalized;
        }

        // Interpolate between two vectors linearly with a certain factor. Temporal is used to indicate that it should be t
        public Vector3 VEC_TemporalVectorCombination(Vector3 a, float factor, Vector3 b)
        {
            return a + (b - a) * factor;
        }

        public int Func_Int(float value)
        {
            return (int)value;
        }

        public float Func_GetDeltaTime() // delta time as int is gross
        {
            return Time.deltaTime * 1000.0f;
        }

        public float VEC_AngleVector(Vector3 a, Vector3 b, int mode) // Dot product, mode = radians, degrees or something
        {
            float result = Vector3.Dot(a, b);
            return result;
        }

        public void fn_p_stKillPersoAndClearVariableProcedure1(Perso perso)
        {
            if (perso != null && perso.gameObject != null) {
                GameObject.Destroy(perso.gameObject);
            }
        }

        public float PadHorizontalAxis()
        {
            return (Input.GetAxisRaw("Horizontal") * 160) *0.5f;
        }

        public float PadVerticalAxis()
        {
            return (-Input.GetAxisRaw("Vertical") * 160) * 0.5f;
        }

        public float Func_AbsoluteValue(float v)
        {
            return (float)Math.Abs(v);
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

        public void SOUND_SendSoundRequest()
        {
            // TODO
        }

        public void SOUND_SetVolumeAnim(int volume)
        {
            // TODO
        }

        public void Proc_TransparentDisplay(int alpha)
        {
            // TODO
        }
    }
}
