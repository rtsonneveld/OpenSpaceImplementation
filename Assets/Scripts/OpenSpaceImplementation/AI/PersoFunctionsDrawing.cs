using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using OpenSpaceImplementation.Overlay;
using OpenSpaceImplementation.Materials;
using OpenSpaceImplementation.General;

namespace OpenSpaceImplementation.AI {

    // Drawing

    public partial class Perso {

        public void fn_p_stJFFTXTProcedure(int textIndex, int unknown, DsgVarByte alpha)
        {
            return; // TODO: stub
        }

        public void fn_p_stJFFTXTProcedure(int textIndex, Vector3 position, string text, DsgVarByte alpha)
        {
            Rayman2Text.DrawText(textIndex, position, text, alpha);
        }

        public void GMT_SetVisualGMTAsChromed(GameMaterial material, bool chromed)
        {
            material.Chromed = chromed;
        }

        public void GMT_SetVisualGMTFrame(GameMaterial material, int frame)
        {
            return; // TODO: stub
        }

        public void Proc_ACT_SetDrawFlag(Perso perso, int flagNumber, byte value)
        {
            return; // TODO: stub
        }

        public void Proc_FactorAnimationFrameRate(int frameRate)
        {
            return; // TODO: stub
        }

        public void Proc_RLIFixe(int a) // a is probably a bool
        {
            return; // TODO: stub
        }

        public void Proc_SetTransparency(int transparency)
        {
            return; // TODO: stub
        }

        public void Proc_TransparentDisplay(int alpha)
        {
            return; // TODO: stub
        }

        public void SHADOW_Display(bool shadow)
        {
            return; // TODO: stub
        }

        public void SetAGO(int type, Vector3 a, Vector3 b, GameMaterial material, DsgVarInt c)
        {
            return; // TODO: stub
        }

        public void SPO_SetSuperimposed(int x, int y, int z)
        {
            return; // TODO: stub
        }

        public void SPO_SwitchSuperimposedTab(int tab)
        {
            return; // TODO: stub
        }
    }
}
