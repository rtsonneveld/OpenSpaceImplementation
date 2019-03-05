using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenSpaceImplementation {
    public class Settings {
        public enum Mode {
            Rayman3PC, Rayman3GC,
            RaymanArenaPC, RaymanArenaGC,
            Rayman2PC, Rayman2DC, Rayman2IOS, Rayman2PS1, Rayman2PS2,
            Rayman2PCDemo2, Rayman2PCDemo1,
            DonaldDuckPC, DonaldDuckDC,
            TonicTroublePC, TonicTroubleSEPC,
            PlaymobilHypePC, PlaymobilAlexPC, PlaymobilLauraPC
        };
        public Mode mode = Mode.Rayman3PC;

        public enum EngineVersion {
            TT = 0,
            Montreal = 1,
            R2 = 2,
            R3 = 3
        };
        public enum Game { R3, RA, R2, TT, TTSE, R2Demo, R2Revolution, DD, PlaymobilHype, PlaymobilLaura, PlaymobilAlex };
        public enum Platform { PC, iOS, GC, DC, PS1, PS2 };
        public enum Endian { Little, Big };
        public enum Encryption { None, ReadInit, FixedInit, CalculateInit, Window };
		public enum Caps { All, AllExceptExtension, Normal, None };
		public enum CapsType { All, LevelFolder, LevelFile, Fix, FixLvl, FixRelocation, LangFix, LangLevelFolder, LangLevelFile, DSB };
        
        public EngineVersion engineVersion;
        public Game game;
        public Platform platform;
        public Endian endian;
		public bool hasObjectTypes = true;
        public bool hasNames = false;
        public bool hasDeformations = false;
        public int numEntryActions = 0;
        public bool hasExtraInputData = false;
        public bool hasMemorySupport = false;
        public Dictionary<string, uint> memoryAddresses = null;
        public bool loadFromMemory = false;
        public Encryption encryption = Encryption.None;
        public bool encryptPointerFiles = false;
        public bool hasLinkedListHeaderPointers = false;
        public bool snaCompression = false;
        public float textureAnimationSpeedModifier = 1f;
        public float luminosity = 0.5f;
        public bool saturate = true;
		public Dictionary<CapsType, Caps> caps = new Dictionary<CapsType, Caps>();
        public bool linkUncategorizedObjectsToScriptFamily = false;

        public bool IsLittleEndian {
            get { return endian == Endian.Little; }
        }

        public static void Init(Mode mode) {
            switch (mode) {
                case Mode.Rayman2IOS: s = Settings.R2IOS; break;
                case Mode.Rayman2DC: s = Settings.R2DC; break;
                case Mode.Rayman2PC: s = Settings.R2PC; break;
				case Mode.Rayman2PS1: s = Settings.R2PS1; break;
				case Mode.Rayman2PS2: s = Settings.R2PS2; break;
                case Mode.Rayman2PCDemo1: s = Settings.R2PCDemo1; break;
                case Mode.Rayman2PCDemo2: s = Settings.R2PCDemo2; break;
                case Mode.Rayman3GC: s = Settings.R3GC; break;
                case Mode.Rayman3PC: s = Settings.R3PC; break;
                case Mode.RaymanArenaGC: s = Settings.RAGC; break;
                case Mode.RaymanArenaPC: s = Settings.RAPC; break;
                case Mode.DonaldDuckPC: s = Settings.DDPC; break;
				case Mode.DonaldDuckDC: s = Settings.DDDC; break;
                case Mode.TonicTroublePC: s = Settings.TTPC; break;
                case Mode.TonicTroubleSEPC: s = Settings.TTSEPC; break;
                case Mode.PlaymobilHypePC: s = Settings.PlaymobilHypePC; break;
                case Mode.PlaymobilAlexPC: s = Settings.PlaymobilAlexPC; break;
                case Mode.PlaymobilLauraPC: s = Settings.PlaymobilLauraPC; break;
            }
            if (s != null) s.mode = mode;
        }


        public static Settings s = null;
        public static Settings R3PC = new Settings() {
            engineVersion = EngineVersion.R3,
            game = Game.R3,
            platform = Platform.PC,
            endian = Endian.Little,
            hasDeformations = true,
            hasMemorySupport = true,
            textureAnimationSpeedModifier = 10f,
            luminosity = 0.1f,
            saturate = false,
        };

        public static Settings R3GC = new Settings() {
            engineVersion = EngineVersion.R3,
            game = Game.R3,
            platform = Platform.GC,
            endian = Endian.Big,
            hasNames = true,
            hasDeformations = true,
            hasExtraInputData = true,
            hasLinkedListHeaderPointers = true,
            textureAnimationSpeedModifier = -10f,
            luminosity = 0.1f,
            saturate = false
        };

        public static Settings RAPC = new Settings() {
            engineVersion = EngineVersion.R3,
            game = Game.RA,
            platform = Platform.PC,
            endian = Endian.Little,
            hasDeformations = true,
            textureAnimationSpeedModifier = 10f,
            luminosity = 0.3f,
            saturate = false
        };

        public static Settings RAGC = new Settings() {
            engineVersion = EngineVersion.R3,
            game = Game.RA,
            platform = Platform.GC,
            endian = Endian.Big,
            hasDeformations = true,
            textureAnimationSpeedModifier = -10f,
            luminosity = 0.1f,
            saturate = false
        };

        public static Settings R2PC = new Settings() {
            engineVersion = EngineVersion.R2,
            game = Game.R2,
            platform = Platform.PC,
            endian = Endian.Little,
            numEntryActions = 43,
            encryption = Encryption.ReadInit,
            luminosity = 0.5f,
            saturate = true,
            hasMemorySupport = true,
            linkUncategorizedObjectsToScriptFamily = true,
        };

        public static Settings R2PCDemo1 = new Settings() {
            engineVersion = EngineVersion.R2,
            game = Game.R2Demo,
            platform = Platform.PC,
            endian = Endian.Little,
            encryption = Encryption.ReadInit,
            luminosity = 0.5f,
            saturate = true,
            numEntryActions = 1
        };

        public static Settings R2PCDemo2 = new Settings() {
            engineVersion = EngineVersion.R2,
            game = Game.R2Demo,
            platform = Platform.PC,
            endian = Endian.Little,
            numEntryActions = 7,
            encryption = Encryption.ReadInit,
            luminosity = 0.5f,
            saturate = true
        };

        public static Settings R2DC = new Settings() {
            engineVersion = EngineVersion.R2,
            game = Game.R2,
            platform = Platform.DC,
            endian = Endian.Little,
            numEntryActions = 43,
            encryption = Encryption.None,
            luminosity = 0.5f,
            saturate = true,
            hasExtraInputData = false,
			caps = new Dictionary<CapsType, Caps>() {
				{ CapsType.All, Caps.All }
			}
        };

		public static Settings R2PS2 = new Settings() {
			engineVersion = EngineVersion.R2,
			game = Game.R2Revolution,
			platform = Platform.PS2,
			endian = Endian.Little,
			numEntryActions = 42,
			encryption = Encryption.None,
			luminosity = 0.5f,
			saturate = true,
			//textureAnimationSpeedModifier = 2f,
			hasExtraInputData = false,
			hasObjectTypes = false,
			caps = new Dictionary<CapsType, Caps>() {
				{ CapsType.All, Caps.None }
			}
		};

		public static Settings R2IOS = new Settings() {
            engineVersion = EngineVersion.R2,
            game = Game.R2,
            platform = Platform.iOS,
            endian = Endian.Little,
            numEntryActions = 43,
            encryption = Encryption.ReadInit,
            hasExtraInputData = true,
            luminosity = 0.5f,
            saturate = true,
			caps = new Dictionary<CapsType, Caps>() {
				{ CapsType.All, Caps.All }
			}
		};

		public static Settings R2PS1 = new Settings() {
			engineVersion = EngineVersion.R2,
			game = Game.R2,
			platform = Platform.PS1,
			endian = Endian.Little,
			encryption = Encryption.ReadInit,
			luminosity = 0.5f,
			saturate = true,
			numEntryActions = 1
		};

		public static Settings DDPC = new Settings() {
            engineVersion = EngineVersion.R2,
            game = Game.DD,
            platform = Platform.PC,
            endian = Endian.Little,
            numEntryActions = 44, // 43 for demo
            encryption = Encryption.ReadInit,
            luminosity = 0.5f,
            saturate = true,
			caps = new Dictionary<CapsType, Caps>() {
				{ CapsType.FixRelocation, Caps.AllExceptExtension }
			}
        };

		public static Settings DDDC = new Settings() {
			engineVersion = EngineVersion.R2,
			game = Game.DD,
			platform = Platform.DC,
			endian = Endian.Little,
			numEntryActions = 43,
			encryption = Encryption.None,
			luminosity = 0.5f,
			saturate = true,
			hasExtraInputData = false,
			caps = new Dictionary<CapsType, Caps>() {
				{ CapsType.All, Caps.All }
			}
		};

		public static Settings TTPC = new Settings() {
			engineVersion = EngineVersion.TT,
			game = Game.TT,
			platform = Platform.PC,
			endian = Endian.Little,
			numEntryActions = 1,
			encryption = Encryption.Window,
			encryptPointerFiles = true,
			hasLinkedListHeaderPointers = true,
			luminosity = 1f,
			saturate = true,
			caps = new Dictionary<CapsType, Caps>() {
				{ CapsType.Fix, Caps.None },
				{ CapsType.FixRelocation, Caps.None },
				{ CapsType.DSB, Caps.None }
			}
        };

		public static Settings TTSEPC = new Settings() {
			engineVersion = EngineVersion.TT,
			game = Game.TTSE,
			platform = Platform.PC,
			endian = Endian.Little,
			numEntryActions = 1,
			hasLinkedListHeaderPointers = true,
			luminosity = 0.5f,
			saturate = true,
			caps = new Dictionary<CapsType, Caps>() {
				{ CapsType.All, Caps.All }
			}
		};

        public static Settings PlaymobilHypePC = new Settings() {
            engineVersion = EngineVersion.Montreal,
            game = Game.PlaymobilHype,
            platform = Platform.PC,
            endian = Endian.Little,
            numEntryActions = 1,
            hasLinkedListHeaderPointers = true,
            snaCompression = true,
            luminosity = 0.5f,
            saturate = true,
			caps = new Dictionary<CapsType, Caps>() {
				{ CapsType.FixLvl, Caps.None }
			}
		};

        public static Settings PlaymobilAlexPC = new Settings() {
            engineVersion = EngineVersion.Montreal,
            game = Game.PlaymobilAlex,
            platform = Platform.PC,
            endian = Endian.Little,
            numEntryActions = 1,
            hasLinkedListHeaderPointers = true,
            snaCompression = true,
            luminosity = 0.5f,
            saturate = true,
			caps = new Dictionary<CapsType, Caps>() {
				{ CapsType.All, Caps.All }
			}
		};

        public static Settings PlaymobilLauraPC = new Settings() {
            engineVersion = EngineVersion.Montreal,
            game = Game.PlaymobilLaura,
            platform = Platform.PC,
            endian = Endian.Little,
            numEntryActions = 1,
            hasLinkedListHeaderPointers = true,
            snaCompression = false,
            luminosity = 0.5f,
            saturate = true,
			caps = new Dictionary<CapsType, Caps>() {
				{ CapsType.LevelFile, Caps.None },
				{ CapsType.FixRelocation, Caps.None },
				{ CapsType.LangFix, Caps.None },
				{ CapsType.LangLevelFile, Caps.None },
				{ CapsType.LangLevelFolder, Caps.None }
			}
        };
    }
}