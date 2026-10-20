using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Unit)), CanEditMultipleObjects]
public class TypeEditor : Editor
{

    public SerializedProperty
        Class_Prop,
        Damage_Prop,
        AttackRange_Prop,
        Speed_Prop,
        AttackSpeed_Prop,

        Projectile_Prop,
        ShootPos_Prop,

        IncHp_Prop,
        IncDam_Prop,
        DecAttSp_Prop,
        Sound_Prop;
        //controllable_Prop;

    void OnEnable()
    {
        // Setup the SerializedProperties
        Class_Prop = serializedObject.FindProperty("Class");
        Damage_Prop = serializedObject.FindProperty("Damage");
        AttackRange_Prop = serializedObject.FindProperty("AttackRange");
        Speed_Prop = serializedObject.FindProperty("Speed");
        AttackSpeed_Prop = serializedObject.FindProperty("AttackSpeed");

        Projectile_Prop = serializedObject.FindProperty("Projectile");
        ShootPos_Prop = serializedObject.FindProperty("ShootPos");

        IncHp_Prop = serializedObject.FindProperty("IncHp");
        IncDam_Prop = serializedObject.FindProperty("IncDam");
        DecAttSp_Prop = serializedObject.FindProperty("DecAttSp");
        Sound_Prop = serializedObject.FindProperty("attackSound");
        //controllable_Prop = serializedObject.FindProperty("controllable");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(Class_Prop);

        Unit.Type st = (Unit.Type)Class_Prop.enumValueIndex;

        switch (st)
        {
            case Unit.Type.Shooter:
                EditorGUILayout.ObjectField(Projectile_Prop);
                EditorGUILayout.ObjectField(ShootPos_Prop);
                break;
                
            case Unit.Type.Fighter:
                EditorGUILayout.ObjectField(Sound_Prop);
                break;
                
        }

        EditorGUILayout.IntSlider(Damage_Prop, 0, 100, new GUIContent("Damage"));
        EditorGUILayout.Slider(AttackRange_Prop, 0, 100, new GUIContent("AttackRange"));
        EditorGUILayout.Slider(AttackSpeed_Prop, 0, 100, new GUIContent("AttackSpeed"));
        EditorGUILayout.Slider(Speed_Prop, 0, 100, new GUIContent("Speed"));

        EditorGUILayout.IntSlider(IncHp_Prop, 0, 2000, new GUIContent("IncHp"));
        EditorGUILayout.IntSlider(IncDam_Prop, 0, 100, new GUIContent("IncDam"));
        EditorGUILayout.Slider(DecAttSp_Prop, 0, 100, new GUIContent("DecAttSp"));

        serializedObject.ApplyModifiedProperties();
    }
}