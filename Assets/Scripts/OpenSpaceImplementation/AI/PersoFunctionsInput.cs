using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenSpaceImplementation.AI {

    // Input

    public partial class Perso {

        public float PadHorizontalAxis()
        {
            return (UnityEngine.Input.GetAxisRaw("Horizontal") * 160) * 0.5f;
        }

        public float PadVerticalAxis()
        {
            return (-UnityEngine.Input.GetAxisRaw("Vertical") * 160) * 0.5f;
        }

        public bool Cond_PressedBut(string action)
        {
            return Controller.InputManager.IsActionValidated(action);
        }

        public bool Cond_JustPressedBut(string action)
        {
            return Controller.InputManager.IsActionJustValidated(action);
        }

        public void Proc_DeactivateBut(string action)
        {
            return; // TODO, stub: should deactivate entry actions
        }

        public void PAD_ReadAnalogJoystickMarioMode(DsgVarFloat dsgVar_74, float v1, int v2, DsgVarFloat dsgVar_73, int v3, int v4, int v5)
        {
            return; // TODO, stub: this is a hard one
        }
    }
}
