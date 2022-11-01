using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using System;

namespace Ategon.Tools.Yuai
{
    public class Yuai : MonoBehaviour
    {
        [SerializeField] private Settings settings;
        [SerializeField] private List<SoundGroup> soundGroups;
        [SerializeField] private List<View> views;
        [SerializeField] private Cursor cursor;

        #region Unity Functions
        private void Awake()
        {
            foreach (View view in views)
            {
                GameObject instantiated = new GameObject();
                instantiated.transform.SetParent(transform);
                instantiated.name = view.name;

                Canvas canvas = instantiated.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.pixelPerfect = true;

                CanvasScaler canvasScaler = instantiated.AddComponent<CanvasScaler>();

                GraphicRaycaster graphicsRaycaster = instantiated.AddComponent<GraphicRaycaster>();

                SendMessage(Debugs.Generation, "Generated new View");

                foreach (Group group in view.groups)
                {
                    GameObject instantiatedGroup = new GameObject();
                    instantiatedGroup.transform.SetParent(instantiated.transform);
                    instantiatedGroup.name = group.name;

                    SendMessage(Debugs.Generation, "Generated new Group");

                    foreach (Part part in group.parts)
                    {
                        GameObject instantiatedPart = new GameObject();
                        instantiatedPart.transform.SetParent(instantiatedGroup.transform);

                        SendMessage(Debugs.Generation, "Generated new Part");
                    }
                }
            }
        }
        #endregion

        #region Logging
        private void SendError(string text)
        {
            Debug.LogError($"[Yuai]: {text}");
        }

        private void SendWarning(string text)
        {
            Debug.LogWarning($"[Yuai]: {text}");
        }

        private void SendMessage(Debugs debug, string text)
        {
            if ((debug & this.settings.debug) != 0)
            {
                Debug.Log($"[Yuai]: {text}");
            }
        }
        #endregion
    }

    [CustomEditor(typeof(Yuai))]
    public class GameTimerEditor : Editor
    {
        private SerializedProperty debug;
        private SerializedProperty sounds;
        private SerializedProperty views;
        private SerializedProperty cursor;

        protected static bool showSettings = true;
        protected static bool showViews = true;
        protected static bool showCursors = true;

        void OnEnable()
        {
            debug = serializedObject.FindProperty("settings");
            sounds = serializedObject.FindProperty("soundGroups");
            views = serializedObject.FindProperty("views");
            cursor = serializedObject.FindProperty("cursor");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUIStyle descriptionStyle = new GUIStyle();
            descriptionStyle.normal.textColor = Color.HSVToRGB(0, 0, 0.5f);
            descriptionStyle.wordWrap = true;

            GUIStyle headerStyle = new GUIStyle();
            headerStyle.normal.textColor = Color.HSVToRGB(0, 0, 0.8f);
            headerStyle.fontSize = 20;

            var myLayout = new GUILayoutOption[] {
                  GUILayout.Height(20)
            };

            EditorGUILayout.LabelField("Yuai is a Unity UI editor that aims to speed up the UI creation process under time constraints through putting together prebuilt parts.", descriptionStyle);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Hover over something to get more info on that part of the tool.", descriptionStyle);
            EditorGUILayout.Space();

            GUI.contentColor = Color.HSVToRGB(330f / 360f, 0.5f, 0.95f);
            EditorGUILayout.PropertyField(debug);

            GUI.contentColor = Color.HSVToRGB(0f / 360f, 0.5f, 0.95f);
            EditorGUILayout.PropertyField(views);

            GUI.contentColor = Color.HSVToRGB(30f / 360f, 0.5f, 0.95f);
            EditorGUILayout.PropertyField(cursor);

            GUI.contentColor = Color.HSVToRGB(60f / 360f, 0.5f, 0.95f);
            EditorGUILayout.PropertyField(sounds);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Disable All Debugs"))
            {
                Debug.Log("Clicked the button");
            }
            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }
    }

    [System.Serializable]
    public class Settings
    {
        public Debugs debug;
        public TMP_FontAsset font;
        public float fontSize;
        public float buttonWidth;
        public float buttonHeight;
    }

    [System.Serializable]
    public class Cursor
    {
        [SerializeField] private List<CursorImage> images;
    }

    [System.Serializable]
    public class CursorImage
    {
        [SerializeField] private Image image;
        [SerializeField] private CursorEffects effects;
    }

    [System.Serializable]
    [Flags]
    public enum Debugs
    {
        Nothing = 0,
        Generation = 1,
        Interaction = 2,
        Inspector = 4,
    }

    [System.Serializable]
    [Flags] public enum CursorEffects
    {
        Rotation = 1,
        Noise = 2,
        Size = 4,
    }

    [System.Serializable]
    public enum CursorRole
    {
        None,
        Game,
        UI,
        Clickable
    }

    [System.Serializable]
    public enum ButtonRole
    {
        SceneSwap,
        GroupSwap
    }

    [System.Serializable]
    public class View
    {
        public string name;
        public List<Group> groups;
        public int scene;
    }

    [System.Serializable]
    public class Group
    {
        public string name;
        public List<Part> parts;
        public AnchorPosition anchor;
        public Vector2 offset;
        public bool defaultEnabled;
    }

    [System.Serializable]
    public class Part
    {
        public PartType partType;
        public Vector2 offset;
        public int fontSize = 1;

        public ButtonRole buttonRole;
    }

    [System.Serializable]
    public class SoundGroup
    {
        [SerializeField] private string name;
        [SerializeField] private List<UISound> sounds;
    }

    [System.Serializable]
    public class UISound
    {
        [SerializeField] private AudioClip sound;
        [SerializeField] private SoundRole role;
    }

    [System.Serializable]
    public enum AnchorPosition
    {
        UpperLeft,
        UpperCenter,
        UpperRight,
        CenterLeft,
        TrueCenter,
        CenterRight,
        LowerLeft,
        LowerCenter,
        LowerRight
    }

    [System.Serializable]
    public enum PartType
    {
        Text,
        Button,
        Image
    }

    [System.Serializable]
    [Flags] public enum SoundRole
    {
        None        = 0,
        Hover       = 1,
        Enable      = 2,
        Disable     = 4,
        Error       = 8,
        Set         = 16,
        OpenPanel   = 32,
        ClosePanel  = 64,
        Change      = 128,
        Back        = 256,
        Forward     = 512,
    }

    [CustomPropertyDrawer(typeof(View))]
    public class ViewDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float bufferAmount = (position.width - 3) / 2;

            GUI.contentColor = Color.HSVToRGB(70f / 360f, 0.5f, 0.8f);
            EditorGUI.PropertyField(new Rect(position.x, position.y, bufferAmount, position.height), property.FindPropertyRelative("name"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(position.x + bufferAmount + 3, position.y, bufferAmount, position.height), property.FindPropertyRelative("scene"), GUIContent.none);

            EditorGUI.indentLevel++;

            GUI.contentColor = Color.HSVToRGB(80f / 360f, 0.5f, 0.8f);
            if (property.FindPropertyRelative("name").stringValue.Length > 0)
            {
                EditorGUILayout.PropertyField(property.FindPropertyRelative("groups"), new GUIContent($"{property.FindPropertyRelative("name").stringValue} Groups"));
            }
            EditorGUI.indentLevel--;

            EditorGUI.EndProperty();
        }
    }

    [CustomPropertyDrawer(typeof(Group))]
    public class GroupDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float bufferAmount = (position.width - 9) / 4;

            GUI.contentColor = Color.HSVToRGB(70f / 360f, 0.5f, 0.8f);
            EditorGUI.PropertyField(new Rect(position.x, position.y, bufferAmount, position.height), property.FindPropertyRelative("name"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(position.x + bufferAmount + 3, position.y, bufferAmount, position.height), property.FindPropertyRelative("anchor"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(position.x + bufferAmount * 2 + 6, position.y, bufferAmount, position.height), property.FindPropertyRelative("offset"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(position.x + bufferAmount * 3 + 9, position.y, bufferAmount, position.height), property.FindPropertyRelative("defaultEnabled"), GUIContent.none);

            EditorGUI.indentLevel++;
            EditorGUI.indentLevel++;

            GUI.contentColor = Color.HSVToRGB(80f / 360f, 0.5f, 0.8f);
            if (property.FindPropertyRelative("name").stringValue.Length > 0)
            {
                EditorGUILayout.PropertyField(property.FindPropertyRelative("parts"), new GUIContent($"{property.FindPropertyRelative("name").stringValue} Parts"));
            }
            EditorGUI.indentLevel--;
            EditorGUI.indentLevel--;

            EditorGUI.EndProperty();
        }
    }

    [CustomPropertyDrawer(typeof(SoundGroup))]
    public class SoundGroupEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float bufferAmount = (position.width) / 1;

            GUI.contentColor = Color.HSVToRGB(70f / 360f, 0.5f, 0.8f);
            EditorGUI.PropertyField(new Rect(position.x, position.y, bufferAmount, position.height), property.FindPropertyRelative("name"), GUIContent.none);

            EditorGUI.indentLevel++;

            GUI.contentColor = Color.HSVToRGB(80f / 360f, 0.5f, 0.8f);
            if (property.FindPropertyRelative("name").stringValue.Length > 0)
            {
                EditorGUILayout.PropertyField(property.FindPropertyRelative("sounds"), new GUIContent($"{property.FindPropertyRelative("name").stringValue} Audio"));
            }
            EditorGUI.indentLevel--;

            EditorGUI.EndProperty();
        }
    }

    [CustomPropertyDrawer(typeof(UISound))]
    public class UISoundEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float bufferAmount = (position.width - 3) / 2;

            GUI.contentColor = Color.HSVToRGB(90f / 360f, 0.5f, 0.8f);
            EditorGUI.PropertyField(new Rect(position.x, position.y, bufferAmount, position.height), property.FindPropertyRelative("role"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(position.x + bufferAmount + 3, position.y, bufferAmount, position.height), property.FindPropertyRelative("sound"), GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
}