using System.IO;
using System.Linq;
using UnityEditor;

public class BatchModeBuild
{
    public static void PerformBuild()
    {
        PrepareFolder("Build");
        var scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();
        BuildPipeline.BuildPlayer(scenes, "Build", BuildTarget.StandaloneWindows, BuildOptions.None);
    }

    private static void PrepareFolder(string path)
    {
        if (Directory.Exists(path))
            Directory.Delete(path);
        
        Directory.CreateDirectory(path);
    }
}
