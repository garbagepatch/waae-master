using UnityEngine;
using UnityEditor;
using CBGames.Player;
using CBGames.Editors;

namespace CBGames.Inspector
{
    [CustomEditor(typeof(SyncPlayer), true)]
    public class SyncPlayerInspector : BaseEditor
    {
        #region Serialized Properties
        SerializedProperty sendPosRot;
        SerializedProperty _syncAnimations;
        SerializedProperty _positionLerpRate;
        SerializedProperty _rotationLerpRate;
        SerializedProperty noneLocalTag;
        SerializedProperty _nonAuthoritativeLayer;
        SerializedProperty teamName;
        SerializedProperty _ragdolled;
        #endregion

        protected override void OnEnable()
        {
            #region Properties
            sendPosRot = serializedObject.FindProperty("sendPosRot");
            _syncAnimations = serializedObject.FindProperty("_syncAnimations");
            _positionLerpRate = serializedObject.FindProperty("_positionLerpRate");
            _rotationLerpRate = serializedObject.FindProperty("_rotationLerpRate");
            noneLocalTag = serializedObject.FindProperty("noneLocalTag");
            _nonAuthoritativeLayer = serializedObject.FindProperty("_nonAuthoritativeLayer");
            teamName = serializedObject.FindProperty("teamName");
            _ragdolled = serializedObject.FindProperty("_ragdolled");
            #endregion

            base.OnEnable();
        }

        public override void OnInspectorGUI()
        {
            #region Core
            base.OnInspectorGUI();
            SyncPlayer sp = (SyncPlayer)target;
            DrawTitleBar(
                "Network Sync Player",
                "Component that belongs on each player. Will send events over the network like animations, damage, etc.",
                E_Core.h_playerIcon
            );
            #endregion

            //Properties 
            GUILayout.BeginHorizontal(_skin.customStyles[1]);
            GUILayout.BeginVertical();

            if (Application.isPlaying)
            {
                GUILayout.Label("DEBUG ACTIONS", _skin.textField);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Net Enable Ragdoll"))
                {
                    sp.GetType().GetMethod("SetActiveRagdoll", E_Helpers.allBindings).Invoke(sp, new object[1] { true });
                }
                if (GUILayout.Button("Net Disable Ragdoll"))
                {
                    sp.GetType().GetMethod("SetActiveRagdoll", E_Helpers.allBindings).Invoke(sp, new object[1] { false });
                }
                GUILayout.EndHorizontal();
                GUI.enabled = false;
                EditorGUILayout.PropertyField(_ragdolled);
                GUI.enabled = true;
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label(EditorGUIUtility.FindTexture("animationkeyframe"), GUILayout.ExpandWidth(false));
            GUILayout.BeginVertical();
            GUILayout.Space(9);
            GUILayout.Label("Sync Animations Settings", _skin.textField);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal(_skin.window);
            EditorGUILayout.PropertyField(_syncAnimations, new GUIContent("Sync Animations"));
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            GUILayout.BeginHorizontal(_skin.customStyles[1]);
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Label(EditorGUIUtility.FindTexture("d_UnityEditor.AnimationWindow"), GUILayout.ExpandWidth(false));
            GUILayout.BeginVertical();
            GUILayout.Space(9);
            GUILayout.Label("Sync Position/Rotation Settings", _skin.GetStyle("TextField"));
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal(_skin.window);
            GUILayout.BeginVertical();
            EditorGUILayout.PropertyField(sendPosRot, new GUIContent("Sync Position & Rotation"));
            EditorGUILayout.Slider(_positionLerpRate, 0, 25, new GUIContent("Position Move Speed"));
            EditorGUILayout.Slider(_rotationLerpRate, 0, 25, new GUIContent("Rotation Move Speed"));
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            GUILayout.BeginHorizontal(_skin.customStyles[1]);
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Label(EditorGUIUtility.FindTexture("TerrainInspector.TerrainToolSettings"), GUILayout.ExpandWidth(false));
            GUILayout.BeginVertical();
            GUILayout.Space(9);
            GUILayout.Label("None Owner Settings", _skin.GetStyle("TextField"));
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal(_skin.window);
            GUILayout.BeginVertical();
            EditorGUILayout.PropertyField(noneLocalTag, new GUIContent("None Owner Tag"));
            EditorGUILayout.PropertyField(_nonAuthoritativeLayer, new GUIContent("None Owner Layer"));
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            GUILayout.BeginHorizontal(_skin.customStyles[1]);
            GUILayout.Label(EditorGUIUtility.FindTexture("d_editcollision_16"), GUILayout.ExpandWidth(false));
            GUILayout.BeginVertical();
            GUILayout.Space(9);
            GUILayout.Label("Team Settings", _skin.textField);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal(_skin.window);
            GUILayout.BeginVertical();
            GUI.enabled = false;
            EditorGUILayout.PropertyField(teamName);
            GUI.enabled = true;
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            EndInspectorGUI(typeof(SyncPlayer));
        }
    }
}