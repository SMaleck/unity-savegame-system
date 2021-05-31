using SavegameSystem.Editor.Constants;
using SavegameSystem.Editor.Utility;
using UnityEditor;
using UnityEngine;

namespace SavegameSystem.Editor.EditorWindows
{
    public class SavegameDecompressorWindow : EditorWindow
    {
        private string _savegameCompressedIn;
        private string _savegameJsonOut;

        [MenuItem(MenuConstants.MenuRoot + "/Savegame Decompressor", priority = MenuConstants.Priority3)]
        private static void CreateDecompressWindow()
        {
            var window = GetWindow<SavegameDecompressorWindow>("Savegame Decompressor");
            window.Initialize();
        }

        private void Initialize()
        {
            _savegameCompressedIn = string.Empty;
            _savegameJsonOut = string.Empty;

            Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Decompress Savegame", EditorStyles.boldLabel);

            _savegameCompressedIn = EditorGUILayout.TextField(
                "Compressed JSON",
                _savegameCompressedIn);

            if (GUILayout.Button("Decompress") &&
                !string.IsNullOrEmpty(_savegameCompressedIn))
            {
                _savegameJsonOut = CompressionUtils.Decompress(_savegameCompressedIn);
            }

            EditorGUILayout.Space(20);
            EditorGUILayout.LabelField("Decompressed JSON");
            EditorGUILayout.SelectableLabel(_savegameJsonOut);
        }
    }
}
