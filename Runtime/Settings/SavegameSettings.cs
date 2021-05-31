using System;
using UnityEngine;

namespace SavegameSystem.Settings
{
    public class SavegameSettings : ScriptableObject, ISavegameSettings
    {
        public static string SettingsPath = "Assets/Packages/SavegameSystem/SavegameSystemSettings.asset";

        [SerializeField] private bool _useCompression;
        public bool UseCompression => _useCompression;

        [Header("Default Writer Settings")]
        [Tooltip("DataPath: Root of the project / installation | PersistentDataPath: OS-specific application data path")]
        [SerializeField] private SavegamePath _savegamePath;
        public string Path => GetPath();
        
        [SerializeField] private string _savegameFilename;
        public string Filename => _savegameFilename;
        
        public void Reset()
        {
            _savegamePath = SavegamePath.DataPath;
            _savegameFilename = "savegame.json";
            _useCompression = true;
        }

        private string GetPath()
        {
            switch (_savegamePath)
            {
                case SavegamePath.DataPath:
                    return Application.dataPath;

                case SavegamePath.PersistentDataPath:
                    return Application.persistentDataPath;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
