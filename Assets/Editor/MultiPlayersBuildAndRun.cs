using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MultiPlayersBuildAndRun
{
    [MenuItem("Tools/여러 개 실행/2개")]
    static void Win64build2()
    {
        Win64Build(2);
    }

    [MenuItem("Tools/여러 개 실행/3개")]
    static void Win64build3()
    {
        Win64Build(3);
    }

    [MenuItem("Tools/여러 개 실행/4개")]
    static void Win64build4()
    {
        Win64Build(4);
    }

    static void Win64Build(int PlayerCount)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);

        for (int i = 1; i <= PlayerCount; i++)
        {
            BuildPipeline.BuildPlayer(GetScenePaths(),
                "Builds/Win64/" + GetProjectName() + i.ToString() + "/" + GetProjectName() + i.ToString() + ".exe",
                BuildTarget.StandaloneWindows64, BuildOptions.AutoRunPlayer
                );
        }
    }
        
    static string GetProjectName()
    {
        string[] str = Application.dataPath.Split('/');

        return str[str.Length - 2];
    }

    static string[] GetScenePaths()
    {
        string[] Scenes = new string[EditorBuildSettings.scenes.Length];

        for (int i = 0; i < Scenes.Length; i++)
        {
            Scenes[i] = EditorBuildSettings.scenes[i].path;
        }

        return Scenes;
    }
}