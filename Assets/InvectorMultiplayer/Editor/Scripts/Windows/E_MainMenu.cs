using UnityEditor;
using UnityEngine;

namespace CBGames.Editors
{
    public class E_MainMenu : EditorWindow
    {
        #region Editor Variables
        GUISkin _skin = null;
        #endregion

        [MenuItem("CB Games/Main Menu", false, 200)]
        private static void CB_MainMenu()
        {
            EditorWindow window = GetWindow<E_MainMenu>(true);
            window.maxSize = new Vector2(500, 575);
            window.minSize = window.maxSize;
        }
        private void OnEnable()
        {
            if (!_skin) _skin = E_Helpers.LoadSkin(E_Core.e_guiSkinPath);
            
            //Make window title
            this.titleContent = new GUIContent("Main Menu", null, "Steps to setup multiplayer support.");
        }
        private void OnGUI()
        {
            //Apply the gui skin
            GUI.skin = _skin;

            EditorGUI.DrawRect(new Rect(0, 0, position.width, position.height), E_Colors.e_c_blue_5);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Invector Multiplayer - Main Menu", _skin.label);
            EditorGUILayout.Space();

            //Apply Body Title/Description
            EditorGUILayout.BeginHorizontal(_skin.box, GUILayout.ExpandHeight(false));
            EditorGUILayout.BeginVertical(GUILayout.ExpandHeight(false));
            EditorGUILayout.LabelField("Select An Action", _skin.label);
            EditorGUILayout.LabelField("All of these options open additional windows with additional " +
                "options, except step 1. This is just a helpful way to know in what the recommend order " +
                "to run things are.", _skin.textField);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            
            //Pre-Setup window
            EditorGUILayout.BeginHorizontal(_skin.box, GUILayout.ExpandHeight(false));
            EditorGUILayout.BeginVertical(GUILayout.ExpandHeight(false));
            EditorGUILayout.LabelField("Pre-Setup", _skin.label);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("1. Convert Invector Scripts", _skin.button, GUILayout.Width(180), GUILayout.Height(35)))
            {
                E_ModifyInvector.CB_ModifyInvectorFiles();

            }
            EditorGUILayout.LabelField("Converts invector scripts to work correctly with this package.", _skin.textField);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            // Setup Window
            // --- Convert Invector Scripts
            EditorGUILayout.BeginHorizontal(_skin.box, GUILayout.ExpandHeight(false));
            EditorGUILayout.BeginVertical(GUILayout.ExpandHeight(false));
            EditorGUILayout.LabelField("Setup", _skin.label);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("2. Enable support for all the invector packages you currently have in your project by going " +
                "to: CB Games > Enable Support. Read all the pop-up windows. Check the support status to see if it's enabled by going " +
                "to: CB Games > Check File Status'", _skin.textField);
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);

            //// --- Convert Scripts
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("3. Add Core Objects", _skin.button, GUILayout.Width(180), GUILayout.Height(35)))
            {
                //Open convert invector window
                E_CoreObjects.CB_CoreObjects();

            }
            EditorGUILayout.LabelField("Adds essential gameobjects to your scene to make multiplayer work.", _skin.textField);
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);

            // --- Convert Player
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("4. Convert Player", _skin.button, GUILayout.Width(180), GUILayout.Height(35)))
            {
                //Open convert player window
                E_ConvertPlayer.CB_ConvertPlayerEditorWindow();
            }
            EditorGUILayout.LabelField("Converts the selected player to be multiplayer compatible.", _skin.textField);
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);

            // --- Convert Scene Objects
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("5. Convert Scene", _skin.button, GUILayout.Width(180), GUILayout.Height(35)))
            {
                //Open convert player window
                E_ConvertScene.CB_ConvertScene();
            }
            EditorGUILayout.LabelField("Scan scene for objects and lets you convert them to be multiplayer compatible.", _skin.textField);
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);

            // --- Convert Prefabs
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("6. Convert Prefabs", _skin.button, GUILayout.Width(180), GUILayout.Height(35)))
            {
                //Open convert player window
                E_ConvertPrefabs.CB_ConvertPrefabs();
            }
            EditorGUILayout.LabelField("Scans your entire project for all invector prefabs and attempts to convert to be multiplayer compatible.", _skin.textField);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            // --- Test Scene
            EditorGUILayout.BeginHorizontal(_skin.box, GUILayout.ExpandHeight(false));
            EditorGUILayout.BeginVertical(GUILayout.ExpandHeight(false));
            EditorGUILayout.LabelField("Post-Setup", _skin.label);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("7. Perform Scene Tests", _skin.button, GUILayout.Width(180), GUILayout.Height(35)))
            {
                //Open convert player window
                E_TestScene.CB_TestScene();
            }
            EditorGUILayout.LabelField("Runs a series of automated tests to tell you what is missing/in error. Also option for auto remediation.", _skin.textField);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }
    }
}