using UnityEditor;
using UnityEngine;

namespace Assets.EventSystem
{
#if UNITY_EDITOR
    [CustomEditor(typeof(BaseEvent<EventParameters>), editorForChildClasses: true)]
    public class RaiseEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            BaseEvent<EventParameters> gameEvent = (BaseEvent<EventParameters>)target;
            if (GUILayout.Button("Raise()"))
            {
                gameEvent.Raise(this, null);
            }
        }
    }
#endif
}