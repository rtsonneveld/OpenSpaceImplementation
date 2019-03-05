using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenSpaceImplementation.AI {

    // Mechanics

    public partial class Perso {

        public bool Cond_IsCurrentStateCustomBitSet(int customBitCheck)
        {
            return false; // TODO: stub
        }

        public float NormSpeed()
        {
            return Speed().magnitude; // TODO: stub
        }

        public void Proc_FixePositionPerso(Perso perso, Vector3 location)
        {
            perso.transform.position = location; // TODO: test + adjust for physics
        }

        public void Proc_SetMechanicScale(DsgVarFloat scale)
        {
            return; // TODO: stub
        }

        public void Proc_SetMechanicSlide(DsgVarFloat slide)
        {
            return; // TODO: stub
        }

        public void fn_p_SetMechanicSpeedVector(Vector3 vector)
        {
            return; // TODO: stub
        }

        public void Proc_SetMechanicSpeedMax(Vector3 maxSpeed)
        {
            return; // TODO: stub
        }

        public void Proc_SetMechanicKeepSpeedZ(DsgVarBool keepSpeedZ)
        {
            return; // TODO: stub
        }

        public void Proc_SetMechanicInertiaXYZ(float x, float y, float z)
        {
            return; // TODO: stub
        }

        public void Proc_ImposeAbsoluteSpeedXY(float x, float y, float z)
        {
            return; // TODO: stub
        }

        public void Proc_SetMechanicStream(DsgVarBool stream)
        {
            return; // TODO: stub
        }

        public Vector3 Speed()
        {
            return Vector3.zero; // TODO: stub
        }
    }
}
