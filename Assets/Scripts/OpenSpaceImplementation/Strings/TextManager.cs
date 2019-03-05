using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenSpaceImplementation.Strings {
    public abstract class TextManager {

        private string[,] textTable = null;
        private int languageCount = 0;
        private int stringCount = 0;

        public void InitTable(int languageCount, int stringCount)
        {
            this.languageCount = languageCount;
            this.stringCount = stringCount;
            this.textTable = new string[languageCount, stringCount];
        }

        public int CurrentLanguage { get; set; }

        private bool CheckIndexValidity(int languageIndex, int stringIndex)
        {
            if (textTable == null) {
                Debug.LogError("Trying to access uninitialized text table, please call InitTable first!");
                return false;
            }

            if (languageIndex < 0 || languageIndex >= languageCount) {
                Debug.LogError("Language index out of bounds: " + languageIndex);
                return false;
            }

            if (stringIndex < 0 || stringIndex >= stringCount) {
                Debug.LogError("String index out of bounds: " + stringIndex);
                return false;
            }

            return true;
        }

        public void SetStringForLanguage(int languageIndex, int stringIndex, string value)
        {
            if (CheckIndexValidity(languageIndex, stringIndex)) {
                this.textTable[languageIndex, stringIndex] = value;
            }
        }

        public string GetStringForLanguage(int languageIndex, int stringIndex)
        {
            if (CheckIndexValidity(languageIndex, stringIndex)) {
                return this.textTable[languageIndex, stringIndex];
            }

            return null;
        }

        public string GetString(int stringIndex)
        {
            return GetStringForLanguage(this.CurrentLanguage, stringIndex);
        }

        public void SetString(int stringIndex, string newString)
        {
            SetStringForLanguage(this.CurrentLanguage, stringIndex, newString);
        }

    }
}
