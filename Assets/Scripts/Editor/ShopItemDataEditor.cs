using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShopItemData), true)]
public class ShopItemDataEditor : Editor
{
    private SerializedProperty _priceProperty;
    private SerializedProperty _shopSpriteProperty;

    private void OnEnable()
    {
        _priceProperty = serializedObject.FindProperty("_price");
        _shopSpriteProperty = serializedObject.FindProperty("_sprite");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();
        
        if (EditorGUI.EndChangeCheck())
        {
            ValidateItem();
        }

        // Show validation status
        if (_priceProperty != null && _priceProperty.intValue < 0)
        {
            EditorGUILayout.HelpBox("Price cannot be negative!", MessageType.Error);
        }
        if (_shopSpriteProperty != null && _shopSpriteProperty.objectReferenceValue == null)
        {
            EditorGUILayout.HelpBox("Shop sprite is required!", MessageType.Error);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void ValidateItem()
    {
        ShopItemData item = (ShopItemData)target;
        if (item != null && string.IsNullOrEmpty(item.name))
        {
            item.name = "New Shop Item";
            EditorUtility.SetDirty(target);
        }
    }
} 