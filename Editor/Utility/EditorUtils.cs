using SavegameSystem.Settings;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SavegameSystem.Editor.Utility
{
    public static class EditorUtils
    {
        /// <summary>
        /// Returns TRUE if asset was found and loaded
        /// Returns FALSE if asset had to be created
        /// </summary>
        /// <typeparam name="TAsset"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="asset"></param>
        /// <returns></returns>
        public static bool LoadOrCreateAsset<TAsset>(string filePath, out TAsset asset) where TAsset : ScriptableObject
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            if (!TryLoadAsset<TAsset>(filePath, out asset))
            {
                asset = ScriptableObject.CreateInstance<TAsset>();
                AssetDatabase.CreateAsset((ScriptableObject)asset, filePath);

                return false;
            }

            return true;
        }

        public static bool TryLoadAsset<TAsset>(string filePath, out TAsset asset) where TAsset : ScriptableObject
        {
            asset = (TAsset)AssetDatabase.LoadAssetAtPath(filePath, typeof(TAsset));
            return asset != null;
        }

        public static void SaveAndRefresh()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static string GetLocalSavegamePath()
        {
            var settings = LoadOrCreateSettingsObject();
            return Path.Combine(settings.Path, settings.Filename);
        }

        public static SavegameSettings LoadOrCreateSettingsObject()
        {
            if (!EditorUtils.LoadOrCreateAsset<SavegameSettings>(
                SavegameSettings.SettingsPath,
                out var settings))
            {
                settings.Reset();
            }

            return settings;
        }
    }
}
