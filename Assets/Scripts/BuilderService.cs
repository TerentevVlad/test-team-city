#if UNITY_EDITOR


using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BuildAgent
{
    public class BuilderService
    {
        private static readonly string OutputProjectsFolder = "../../Build";
    
        static void BuildAndroid()
        {
            BuildTarget target = BuildTarget.Android;
            BuildOptions options = BuildOptions.CompressWithLz4;
            EditorUserBuildSettings.SwitchActiveBuildTarget(target);
            // PlayerSettings.applicationIdentifier = Environment.GetEnvironmentVariable("AppBundle");
            // PlayerSettings.Android.keystoreName = Environment.GetEnvironmentVariable("KeystoreName");
            // PlayerSettings.Android.keystorePass = Environment.GetEnvironmentVariable("KeystorePassword");
            // PlayerSettings.Android.keyaliasName = Environment.GetEnvironmentVariable("KeyAlias");
            // PlayerSettings.Android.keyaliasPass = Environment.GetEnvironmentVariable("KeyPassword");

            BuildPipeline.BuildPlayer(GetScenesFromEditorBuildSettings(), string.Format("{0}/{1}.apk" , OutputProjectsFolder, PlayerSettings.applicationIdentifier), target, options);
        }
    
        static string[] GetScenesFromEditorBuildSettings()
        {
            var projectScenes = EditorBuildSettings.scenes;
            List<string> scenesToBuild = new List<string>();
            for (int i = 0; i < projectScenes.Length; i++)
            {
                if (projectScenes[i].enabled) {
                    scenesToBuild.Add(projectScenes[i].path);
                }
            }
            return scenesToBuild.ToArray();
        }
    }
}

#endif
