#if UNITY_EDITOR


using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace BuildAgent
{
    public class BuilderService
    {
       
        static string[] GetScenesFromEditorBuildSettings()
        {
            var projectScenes = EditorBuildSettings.scenes;
            List<string> scenesToBuild = new List<string>();
            for (int i = 0; i < projectScenes.Length; i++)
            {
                if (projectScenes[i].enabled)
                {
                    scenesToBuild.Add(projectScenes[i].path);
                }
            }

            return scenesToBuild.ToArray();
        }
        [MenuItem("Build/Apk")]

        public static void BuildApk()
        {
            var outdir = System.Environment.CurrentDirectory + "/BuildOutPutPath/Android";
            var outputPath = Path.Combine(outdir, Application.productName + ".apk");
            // Обработка папки
            if (!Directory.Exists(outdir)) Directory.CreateDirectory(outdir);
            if (File.Exists(outputPath)) File.Delete(outputPath);


            string[] scenes = GetScenesFromEditorBuildSettings();
            BuildPipeline.BuildPlayer(scenes, outputPath, BuildTarget.Android, BuildOptions.None);
            if (File.Exists(outputPath))
            {
                Debug.Log("Build Success :" + outputPath);
            }
            else
            {
                Debug.LogException(new Exception("Build Fail! Please Check the log! "));
            }
        }
    }
    
}

#endif
