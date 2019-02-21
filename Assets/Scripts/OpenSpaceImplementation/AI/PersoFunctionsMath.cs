using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace OpenSpaceImplementation.AI {

    // Maths

    public partial class Perso {

        public float Func_Sinus(float a)
        {
            return (float)Math.Sin((a / 180) * Math.PI);
        }

        public float Func_Cosinus(float a)
        {
            return (float)Math.Cos((a / 180) * Math.PI);
        }

        public float Func_AbsoluteValue(float v)
        {
            return (float)Math.Abs(v);
        }

        public int Func_Int(float value)
        {
            return (int)value;
        }

        public float VEC_GetVectorNorm(Vector3 vector3)
        {
            return vector3.magnitude;
        }

        public Vector3 Func_Normalize(Vector3 vector3)
        {
            return vector3.normalized;
        }

        public float VEC_AngleVector(Vector3 a, Vector3 b, int mode) // Dot product, mode = radians, degrees or something
        {
            float result = Vector3.Dot(a, b);
            return result;
        }

        // Interpolate between two vectors linearly with a certain factor. Temporal is used to indicate that it should be t
        public Vector3 VEC_TemporalVectorCombination(Vector3 a, float factor, Vector3 b)
        {
            return a + (b - a) * factor;
        }

        public int Func_RandomInt(int v1, int v2)
        {
            return UnityEngine.Random.Range(v1, v2);
        }

        public Vector3 Func_GetPersoSighting()
        {
            return Vector3.zero; // TODO, stub
        }

    }
}
