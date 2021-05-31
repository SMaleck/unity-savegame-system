using SavegameSystem.Editor.Constants;
using SavegameSystem.Editor.Utility;
using SavegameSystem.Settings;
using System.IO;
using UnityEditor;

namespace SavegameSystem.Editor.EditorMenus
{
    public static class SavegameSystemMenus
    {
        [MenuItem(MenuConstants.MenuRoot + "/Open Settings", priority = MenuConstants.Priority1)]
        public static void OpenSettings()
        {
            if (!File.Exists(SavegameSettings.SettingsPath))
            {
                EditorUtils.LoadOrCreateSettingsObject();
            }

            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(SavegameSettings.SettingsPath);
        }

        [MenuItem(MenuConstants.MenuRoot + "/Create Settings", priority = MenuConstants.Priority1)]
        public static void CreateSettings()
        {
            EditorUtils.LoadOrCreateSettingsObject();
            EditorUtils.SaveAndRefresh();

            OpenSettings();
        }

        [MenuItem(MenuConstants.MenuRoot + "/Reset Settings", priority = MenuConstants.Priority1)]
        public static void ResetSettings()
        {
            var settings = EditorUtils.LoadOrCreateSettingsObject();
            settings.Reset();

            EditorUtils.SaveAndRefresh();

            OpenSettings();
        }

        [MenuItem(MenuConstants.MenuRoot + "/Show Savegame in Explorer", priority = MenuConstants.Priority2)]
        private static void GoToSavegame()
        {
            var savegameFilePath = EditorUtils.GetLocalSavegamePath();

            if (File.Exists(savegameFilePath))
            {
                EditorUtility.RevealInFinder(savegameFilePath);
                return;
            }

            var directory = Path.GetDirectoryName(savegameFilePath);
            EditorUtility.RevealInFinder(directory);
        }

        [MenuItem(MenuConstants.MenuRoot + "/Delete Savegame", priority = MenuConstants.Priority2)]
        private static void ClearSavegame()
        {
            var savegamePath = EditorUtils.GetLocalSavegamePath();
            File.Delete(savegamePath);
        }

        [MenuItem(MenuConstants.MenuRoot + "/Decompress Local Savegame (in-place)", priority = MenuConstants.Priority3)]
        public static void DecompressLocal()
        {
            var savegamePath = EditorUtils.GetLocalSavegamePath();
            UnityEngine.Debug.Log($"Decompressing Savegame at: {savegamePath}");

            var compressedJson = File.ReadAllText(savegamePath);
            var decompressedJson = CompressionUtils.Decompress(compressedJson);

            File.WriteAllText(savegamePath, decompressedJson);
            UnityEngine.Debug.Log($"SUCCESS!");

            GoToSavegame();
        }
    }
}
