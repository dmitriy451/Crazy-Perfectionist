using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace GameAnalyticsSDK.Setup
{
    /// <summary>
    ///     The Settings object contains an array of options which allows you to customize your use of GameAnalytics. Most
    ///     importantly you will need to fill in your Game Key and Secret Key on the Settings object to use the service.
    /// </summary>
    public class Settings : ScriptableObject
    {
        /// <summary>
        ///     Types of help given in the help box of the GA inspector
        /// </summary>
        public enum HelpTypes
        {
            None,
            IncludeSystemSpecsHelp,
            ProvideCustomUserID
        }

        public enum MessageTypes
        {
            None,
            Error,
            Info,
            Warning
        }

        /// <summary>
        ///     A message and message type for the help box displayed on the GUI inspector
        /// </summary>
        public struct HelpInfo
        {
            public string Message;
            public MessageTypes MsgType;
            public HelpTypes HelpType;
        }

        #region public static values

        /// <summary>
        ///     The version of the GA Unity Wrapper plugin
        /// </summary>
        [HideInInspector] public static string VERSION = "7.3.24";

        [HideInInspector] public static bool CheckingForUpdates = false;

        #endregion

        #region public values

        public int TotalMessagesSubmitted;
        public int TotalMessagesFailed;

        public int DesignMessagesSubmitted;
        public int DesignMessagesFailed;
        public int QualityMessagesSubmitted;
        public int QualityMessagesFailed;
        public int ErrorMessagesSubmitted;
        public int ErrorMessagesFailed;
        public int BusinessMessagesSubmitted;
        public int BusinessMessagesFailed;
        public int UserMessagesSubmitted;
        public int UserMessagesFailed;

        public string CustomArea = string.Empty;

        [SerializeField] private List<string> gameKey = new();

        [SerializeField] private List<string> secretKey = new();

        [SerializeField] public List<string> Build = new();

        [SerializeField] public List<string> SelectedPlatformOrganization = new();

        [SerializeField] public List<string> SelectedPlatformStudio = new();

        [SerializeField] public List<string> SelectedPlatformGame = new();

        [SerializeField] public List<int> SelectedPlatformGameID = new();

        [SerializeField] public List<int> SelectedOrganization = new();

        [SerializeField] public List<int> SelectedStudio = new();

        [SerializeField] public List<int> SelectedGame = new();

        public string NewVersion = "";
        public string Changes = "";

        public bool SignUpOpen = true;
        public string StudioName = "";
        public string GameName = "";
        public string OrganizationName = "";
        public string OrganizationIdentifier = "";
        public string EmailGA = "";

        [NonSerialized] public string PasswordGA = "";

        [NonSerialized] public string TokenGA = "";

        [NonSerialized] public string ExpireTime = "";

        [NonSerialized] public string LoginStatus = "Not logged in.";

        [NonSerialized] public bool JustSignedUp = false;

        [NonSerialized] public bool HideSignupWarning = false;

        public bool IntroScreen = true;

        [NonSerialized] public List<Organization> Organizations;

        public bool InfoLogEditor = true;
        public bool InfoLogBuild = true;
        public bool VerboseLogBuild;
        public bool UseManualSessionHandling;

        public bool SendExampleGameDataToMyGame;
        //public bool UseBundleVersion = false;

        public bool InternetConnectivity;

        public List<string> CustomDimensions01 = new();
        public List<string> CustomDimensions02 = new();
        public List<string> CustomDimensions03 = new();

        public List<string> ResourceItemTypes = new();
        public List<string> ResourceCurrencies = new();

        public RuntimePlatform LastCreatedGamePlatform;

        public List<RuntimePlatform> Platforms = new();

        //These values are used for the GA_Inspector only
        public enum InspectorStates
        {
            Account,
            Basic,
            Debugging,
            Pref
        }

        public InspectorStates CurrentInspectorState;
        public List<HelpTypes> ClosedHints = new();
        public bool DisplayHints;
        public Vector2 DisplayHintsScrollState;
        public Texture2D Logo;
        public Texture2D UpdateIcon;
        public Texture2D InfoIcon;
        public Texture2D DeleteIcon;
        public Texture2D GameIcon;
        public Texture2D HomeIcon;
        public Texture2D InstrumentIcon;
        public Texture2D QuestionIcon;
        public Texture2D UserIcon;

        public Texture2D AmazonIcon;
        public Texture2D GooglePlayIcon;
        public Texture2D iosIcon;
        public Texture2D macIcon;
        public Texture2D windowsPhoneIcon;

        [NonSerialized] public GUIStyle SignupButton;

        public bool UsePlayerSettingsBuildNumber;
        public bool SubmitErrors = true;
        public bool NativeErrorReporting;
        public int MaxErrorCount = 10;
        public bool SubmitFpsAverage;
        public bool SubmitFpsCritical;
        public bool IncludeGooglePlay = true;
        public int FpsCriticalThreshold = 20;
        public int FpsCirticalSubmitInterval = 1;

        public List<bool> PlatformFoldOut = new();

        public bool CustomDimensions01FoldOut;
        public bool CustomDimensions02FoldOut;
        public bool CustomDimensions03FoldOut;

        public bool ResourceItemTypesFoldOut;
        public bool ResourceCurrenciesFoldOut;

        #endregion

        #region public methods

        /// <summary>
        ///     Sets a custom user ID.
        ///     Make sure each user has a unique user ID. This is useful if you have your own log-in system with unique user IDs.
        ///     NOTE: Only use this method if you have enabled "Custom User ID" on the GA inspector!
        /// </summary>
        /// <param name="customID">
        ///     The custom user ID - this should be unique for each user
        /// </param>
        public void SetCustomUserID(string customID)
        {
            if (customID != string.Empty)
            {
                // Set custom ID native
            }
        }

        public void RemovePlatformAtIndex(int index)
        {
            if (index >= 0 && index < Platforms.Count)
            {
                gameKey.RemoveAt(index);
                secretKey.RemoveAt(index);
                Build.RemoveAt(index);
                SelectedPlatformOrganization.RemoveAt(index);
                SelectedPlatformStudio.RemoveAt(index);
                SelectedPlatformGame.RemoveAt(index);
                SelectedPlatformGameID.RemoveAt(index);
                SelectedOrganization.RemoveAt(index);
                SelectedStudio.RemoveAt(index);
                SelectedGame.RemoveAt(index);
                PlatformFoldOut.RemoveAt(index);
                Platforms.RemoveAt(index);
            }
        }

        public void AddPlatform(RuntimePlatform platform)
        {
            gameKey.Add("");
            secretKey.Add("");
            Build.Add("0.1");
            SelectedPlatformOrganization.Add("");
            SelectedPlatformStudio.Add("");
            SelectedPlatformGame.Add("");
            SelectedPlatformGameID.Add(-1);
            SelectedOrganization.Add(0);
            SelectedStudio.Add(0);
            SelectedGame.Add(0);
            PlatformFoldOut.Add(true);
            Platforms.Add(platform);
        }

        public static readonly RuntimePlatform[] AvailablePlatforms =
        {
            RuntimePlatform.Android,
            RuntimePlatform.IPhonePlayer,
            RuntimePlatform.LinuxPlayer,
            RuntimePlatform.OSXPlayer,
            RuntimePlatform.tvOS,
            RuntimePlatform.WebGLPlayer,
            RuntimePlatform.WindowsPlayer,
            RuntimePlatform.WSAPlayerARM
#if UNITY_2018_1_OR_NEWER

#else
            ,RuntimePlatform.TizenPlayer
#endif
        };

        public string[] GetAvailablePlatforms()
        {
            var result = new List<string>();

            for (var i = 0; i < AvailablePlatforms.Length; ++i)
            {
                var value = AvailablePlatforms[i];

                if (value == RuntimePlatform.IPhonePlayer)
                {
                    if (!Platforms.Contains(RuntimePlatform.tvOS) && !Platforms.Contains(value))
                    {
                        result.Add(value.ToString());
                    }
                    else
                    {
                        if (!Platforms.Contains(value)) result.Add(value.ToString());
                    }
                }
                else if (value == RuntimePlatform.tvOS)
                {
                    if (!Platforms.Contains(RuntimePlatform.IPhonePlayer) && !Platforms.Contains(value))
                    {
                        result.Add(value.ToString());
                    }
                    else
                    {
                        if (!Platforms.Contains(value)) result.Add(value.ToString());
                    }
                }
                else if (value == RuntimePlatform.WSAPlayerARM)
                {
                    if (!Platforms.Contains(value)) result.Add("WSA");
                }
                else
                {
                    if (!Platforms.Contains(value)) result.Add(value.ToString());
                }
            }

            return result.ToArray();
        }

        public bool IsGameKeyValid(int index, string value)
        {
            var valid = true;

            for (var i = 0; i < Platforms.Count; ++i)
                if (index != i)
                    if (value.Equals(gameKey[i]))
                    {
                        valid = false;
                        break;
                    }

            return valid;
        }

        public bool IsSecretKeyValid(int index, string value)
        {
            var valid = true;

            for (var i = 0; i < Platforms.Count; ++i)
                if (index != i)
                    if (value.Equals(secretKey[i]))
                    {
                        valid = false;
                        break;
                    }

            return valid;
        }

        public static void UpdateKeys(int index, string gameKey, string secretKey)
        {
            GameAnalytics.SettingsGA.gameKey[index] = gameKey;
            GameAnalytics.SettingsGA.secretKey[index] = secretKey;
        }

        public void UpdateGameKey(int index, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var valid = IsGameKeyValid(index, value);

                if (valid)
                    gameKey[index] = value;
                else if (gameKey[index].Equals(value)) gameKey[index] = "";
            }
            else
            {
                gameKey[index] = value;
            }
        }

        public void UpdateSecretKey(int index, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var valid = IsSecretKeyValid(index, value);

                if (valid)
                    secretKey[index] = value;
                else if (secretKey[index].Equals(value)) secretKey[index] = "";
            }
            else
            {
                secretKey[index] = value;
            }
        }

        public string GetGameKey(int index)
        {
            return gameKey[index];
        }

        public string GetSecretKey(int index)
        {
            return secretKey[index];
        }

        /// <summary>
        ///     Sets a custom area string. An area is often just a level, but you can set it to whatever makes sense for your game.
        ///     F.x. in a big open world game you will probably need custom areas to identify regions etc.
        ///     By default, if no custom area is set, the Application.loadedLevelName string is used.
        /// </summary>
        /// <param name="customID">
        ///     The custom area.
        /// </param>
        public void SetCustomArea(string customArea)
        {
            // Set custom area native
        }

        public void SetKeys(string gamekey, string secretkey)
        {
            // set keys native
        }

        #endregion
    }

    public class Organization
    {
        public Organization(string name, string id)
        {
            Name = name;
            ID = id;
            Studios = new List<Studio>();
        }

        public string Name { get; }
        public string ID { get; }
        public List<Studio> Studios { get; }

        public static string[] GetOrganizationNames(List<Organization> organizations, bool addFirstEmpty = true)
        {
            if (organizations == null) return new[] { "-" };

            if (addFirstEmpty)
            {
                var names = new string[organizations.Count + 1];
                names[0] = "-";

                var spaceAdd = "";
                for (var i = 0; i < organizations.Count; i++)
                {
                    names[i + 1] = organizations[i].Name + spaceAdd;
                    spaceAdd += " ";
                }

                return names;
            }
            else
            {
                var names = new string[organizations.Count];

                var spaceAdd = "";
                for (var i = 0; i < organizations.Count; i++)
                {
                    names[i] = organizations[i].Name + spaceAdd;
                    spaceAdd += " ";
                }

                return names;
            }
        }
    }

    //[System.Serializable]
    public class Studio
    {
        public Studio(string name, string id, string orgId, List<Game> games)
        {
            Name = name;
            ID = id;
            OrganizationID = orgId;
            Games = games;
        }

        public string Name { get; }

        public string ID { get; }

        public string OrganizationID { get; }

        //[SerializeField]
        public List<Game> Games { get; }

        public static string[] GetStudioNames(List<Studio> studios, bool addFirstEmpty = true)
        {
            if (studios == null) return new[] { "-" };

            if (addFirstEmpty)
            {
                var names = new string[studios.Count + 1];
                names[0] = "-";

                for (var i = 0; i < studios.Count; i++)
                {
                    var j = i + 1;
                    names[j] = j + ". " + studios[i].Name;
                }

                return names;
            }
            else
            {
                var names = new string[studios.Count];

                for (var i = 0; i < studios.Count; i++)
                {
                    var j = i + 1;
                    names[i] = j + ". " + studios[i].Name;
                }

                return names;
            }
        }

        public static string[] GetGameNames(int index, List<Studio> studios)
        {
            if (studios == null || studios[index].Games == null) return new[] { "-" };

            var names = new string[studios[index].Games.Count + 1];
            names[0] = "-";

            for (var i = 0; i < studios[index].Games.Count; i++)
            {
                var j = i + 1;
                names[j] = j + ". " + studios[index].Games[i].Name;
            }

            return names;
        }
    }

    public class Game
    {
        public Game(string name, int id, string gameKey, string secretKey)
        {
            Name = name;
            ID = id;
            GameKey = gameKey;
            SecretKey = secretKey;
        }

        public string Name { get; }

        public int ID { get; }

        public string GameKey { get; }

        public string SecretKey { get; }
    }
}