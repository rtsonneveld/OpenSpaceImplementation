using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace OpenSpaceImplementation.AI {

    // Maths

    public partial class Perso {

        public bool fn_p_stNullVector(Vector3 vector)
        {
            return vector.Equals(Vector3.zero);
        }

        public float Func_AbsoluteValue(float v)
        {
            return (float)Math.Abs(v);
        }

        public float Func_Cosinus(float a)
        {
            return (float)Math.Cos((a / 180) * Math.PI);
        }

        public Vector3 Func_GetPersoSighting()
        {
            return Vector3.zero; // TODO: stub
        }

        public int Func_Int(float value)
        {
            return (int)value;
        }

        public float Func_MaximumReal(float a, float b)
        {
            return Math.Max(a, b);
        }

        public float Func_MinimumReal(float a, float b)
        {
            return Math.Min(a, b);
        }

        public Vector3 Func_Normalize(Vector3 vector3)
        {
            return vector3.normalized;
        }

        public int Func_RandomInt(int v1, int v2)
        {
            return UnityEngine.Random.Range(v1, v2);
        }

        public float Func_Sinus(float a)
        {
            return (float)Math.Sin((a / 180) * Math.PI);
        }
        public float VEC_AngleVector(Vector3 a, Vector3 b, int mode) // Dot product, mode = radians, degrees or something
        {
            float result = Vector3.Dot(a, b);
            return result;
        }

        public float VEC_GetVectorNorm(Vector3 vector3)
        {
            return vector3.magnitude;
        }
        // Interpolate between two vectors linearly with a certain factor. Temporal is used to indicate that it should be t
        public Vector3 VEC_TemporalVectorCombination(Vector3 a, float factor, Vector3 b)
        {
            return a + (b - a) * factor;
        }
    }
}
