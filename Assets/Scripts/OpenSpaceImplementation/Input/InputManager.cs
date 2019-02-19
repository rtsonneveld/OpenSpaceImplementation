using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenSpaceImplementation.Input {
    public class InputManager : MonoBehaviour {

        protected Dictionary<string, EntryAction> entryActions = new Dictionary<string, EntryAction>();
        protected string currentSequence = "";
        protected int sequenceTimer = 0;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            // Update the sequence string
            bool letterPressed = false;

            for (KeyCode keyCode = UnityEngine.KeyCode.A; keyCode < UnityEngine.KeyCode.Z; keyCode++) {
                if (UnityEngine.Input.GetKeyDown(keyCode)) {
                    letterPressed = true;
                    currentSequence += keyCode.ToString();
                    sequenceTimer = 0;
                }
            }

            if (!letterPressed) {
                sequenceTimer++;
                if (UnityEngine.Input.anyKeyDown) {
                    currentSequence = "";
                }
            }

            // Update all EntryActions
            foreach (EntryAction action in entryActions.Values) {
                action.Update();
            }
        }

        protected EntryAction GetEntryAction(string actionName)
        {
            if (entryActions.ContainsKey(actionName)) {
                EntryAction action = entryActions[actionName];
            } else {
                throw new System.Exception("EntryAction " + actionName + " not in InputManager.entryActions");
            }

            return null;
        }

        public bool IsActionValidated(string actionName)
        {
            EntryAction entryAction = GetEntryAction(actionName);
            if (entryAction!=null) {
                return entryAction.IsValidated();
            }
            return false;
        }

        public bool IsActionJustValidated(string actionName)
        {
            EntryAction entryAction = GetEntryAction(actionName);
            if (entryAction != null) {
                return entryAction.IsJustValidated();
            }
            return false;
        }

        public bool IsActionInvalidated(string actionName)
        {
            EntryAction entryAction = GetEntryAction(actionName);
            if (entryAction != null) {
                return entryAction.IsInvalidated();
            }
            return false;
        }

        public bool IsActionJustInvalidated(string actionName)
        {
            EntryAction entryAction = GetEntryAction(actionName);
            if (entryAction != null) {
                return entryAction.IsJustInvalidated();
            }
            return false;
        }

        public bool CheckSequence(string sequence)
        {
            return currentSequence.ToLower().EndsWith(sequence.ToLower());
        }

        public bool CheckSequenceJustEntered(string sequence)
        {
            return CheckSequence(sequence) && sequenceTimer == 0;
        }

        // Returns true if the specified axis is between minValue and maxValue
        public bool JoystickAxeValue(string axis, int minValue, int maxValue)
        {            
            float axisValue = UnityEngine.Input.GetAxisRaw(axis);
            float minValueFloat = minValue / (float)(short.MinValue);
            float maxValueFloat = maxValue / (float)(short.MaxValue);

            return axisValue >= minValueFloat && axisValue <= maxValueFloat;
        }
    }
}