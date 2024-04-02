using System.Linq;
using UnityEditor;

public class PlayerManagerEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerManager pm = (PlayerManager)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");

        if (pm.stateMachine == null) return;

        if (pm.stateMachine.CurrentState != null)
            EditorGUILayout.LabelField("current State: ", pm.stateMachine.CurrentState.ToString());

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Available States");

        if (showFoldout)
        {
            if (pm.stateMachine.dictionaryState != null)
            {
                var keys = pm.stateMachine.dictionaryState.Keys.ToArray();
                var vals = pm.stateMachine.dictionaryState.Values.ToArray();

                for (int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], vals[i]));
                }
            }
        }
    }
}
