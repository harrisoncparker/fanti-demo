using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShopItem))]
public class ShopItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ShopItem shopItem = (ShopItem)target;
        
        DrawDefaultInspector();

        // Validate required components
        var missingComponents = ValidateRequiredComponents(shopItem);
        foreach (var message in missingComponents)
        {
            EditorGUILayout.HelpBox(message, MessageType.Error);
        }
    }

    private string[] ValidateRequiredComponents(ShopItem shopItem)
    {
        var messages = new System.Collections.Generic.List<string>();

        // Check serialized fields using SerializedProperty
        SerializedProperty itemImageProp = serializedObject.FindProperty("_itemImage");
        SerializedProperty priceTextProp = serializedObject.FindProperty("_priceText");
        SerializedProperty quantityTextProp = serializedObject.FindProperty("_quantityText");
        SerializedProperty itemDataProp = serializedObject.FindProperty("_itemData");

        if (itemImageProp.objectReferenceValue == null)
            messages.Add("Item Image reference is required!");
        if (priceTextProp.objectReferenceValue == null)
            messages.Add("Price Text Display reference is required!");
        if (quantityTextProp.objectReferenceValue == null)
            messages.Add("Quantity Text Display reference is required!");
        if (itemDataProp.objectReferenceValue == null)
            messages.Add("Shop Item Data reference is required!");

        return messages.ToArray();
    }
} 