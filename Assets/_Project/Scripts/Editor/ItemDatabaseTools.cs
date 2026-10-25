using System.Collections.Generic;
using UnityEditor;

public static class ItemDatabaseTools
{
    [MenuItem("DracoDimension/Rebuild Item Database")]
    public static void RebuildItemDB()
    {
        // === Find Items === //
        // find all Item instance returning their IDs
        string[] foundItemsIDs = AssetDatabase.FindAssets("t:Item");
        List<Item> items = new();

        foreach (string itemID in foundItemsIDs)
        {
            // find path to item id
            string itemPath = AssetDatabase.GUIDToAssetPath(itemID);
            // grab item at path
            Item item = AssetDatabase.LoadAssetAtPath<Item>(itemPath);
            items.Add(item);
        }


        // === Find the DB === //
        // find the ItemDatabase instance returning its ID
        string[] foundItemDatabaseIDs = AssetDatabase.FindAssets("t:ItemDatabase");
        ItemDatabase db;

        // find path to db id
        string dbPath = AssetDatabase.GUIDToAssetPath(foundItemDatabaseIDs[0]); 
        // grab db at path
        db = AssetDatabase.LoadAssetAtPath<ItemDatabase>(dbPath);


        // === Write items into ItemDatabase === //
        SerializedObject itemDatabaseSO = new SerializedObject(db);

        SerializedProperty listProp = itemDatabaseSO.FindProperty("_itemDB");
        listProp.arraySize = items.Count;

        for (int i = 0; i < items.Count; i++)
        {
            listProp.GetArrayElementAtIndex(i).objectReferenceValue = items[i];
        }

        itemDatabaseSO.ApplyModifiedProperties();

        AssetDatabase.SaveAssets();

        UnityEngine.Debug.Log($"Item database rebuilt: {items.Count} items");
    }
}
