using UnityEditor;
using UnityEngine;
using System.IO;

public static class InterfaceCreator
{
    private const string defaultInterfaceName = "NewInterface";

    [MenuItem("Assets/Create/Scripting/Interfaces/New C# Interface", false, 80)]
    public static void CreateInterface()
    {
        string path = GetSelectedPathOrFallback();
        string fileName = defaultInterfaceName + ".cs";
        string fullPath = Path.Combine(path, fileName);

        string template =
$@"public interface {defaultInterfaceName}
{{
    
}}";

        // Make sure we don't overwrite existing files
        fullPath = AssetDatabase.GenerateUniqueAssetPath(fullPath);

        File.WriteAllText(fullPath, template);
        AssetDatabase.Refresh();
    }

    private static string GetSelectedPathOrFallback()
    {
        string path = "Assets";

        foreach (Object obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(obj);
            if (File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
            }
            break;
        }
        return path;
    }
}