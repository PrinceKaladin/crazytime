using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using System.IO;

public class BuildScript
{
    public static void PerformBuild()
    {
        // ========================
        // Список сцен
        // ========================
        string[] scenes = {
        "Assets/Scenes/menu.unity",
        "Assets/Scenes/gameplay.unity",
        "Assets/Scenes/settings.unity",
        };

        // ========================
        // Пути к файлам сборки
        // ========================
        string aabPath = "CrazyButtons.aab";
        string apkPath = "CrazyButtons.apk";

        // ========================
        // Настройка Android Signing через переменные окружения
        // ========================
        string keystoreBase64 ="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";
        string keystorePass = "button";
        string keyAlias = "button";
        string keyPass = "button";

        string tempKeystorePath = null;

        if (!string.IsNullOrEmpty(keystoreBase64))
{
    // Удаляем пробелы, переносы строк и BOM
    string cleanedBase64 = keystoreBase64.Trim()
                                         .Replace("\r", "")
                                         .Replace("\n", "")
                                         .Trim('\uFEFF');

    // Создаем временный файл keystore
    tempKeystorePath = Path.Combine(Path.GetTempPath(), "TempKeystore.jks");
    File.WriteAllBytes(tempKeystorePath, Convert.FromBase64String(cleanedBase64));

    PlayerSettings.Android.useCustomKeystore = true;
    PlayerSettings.Android.keystoreName = tempKeystorePath;
    PlayerSettings.Android.keystorePass = keystorePass;
    PlayerSettings.Android.keyaliasName = keyAlias;
    PlayerSettings.Android.keyaliasPass = keyPass;

    Debug.Log("Android signing configured from Base64 keystore.");
}
        else
        {
            Debug.LogWarning("Keystore Base64 not set. APK/AAB will be unsigned.");
        }

        // ========================
        // Общие параметры сборки
        // ========================
        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // ========================
        // 1. Сборка AAB
        // ========================
        EditorUserBuildSettings.buildAppBundle = true;
        options.locationPathName = aabPath;

        Debug.Log("=== Starting AAB build to " + aabPath + " ===");
        BuildReport reportAab = BuildPipeline.BuildPlayer(options);
        if (reportAab.summary.result == BuildResult.Succeeded)
            Debug.Log("AAB build succeeded! File: " + aabPath);
        else
            Debug.LogError("AAB build failed!");

        // ========================
        // 2. Сборка APK
        // ========================
        EditorUserBuildSettings.buildAppBundle = false;
        options.locationPathName = apkPath;

        Debug.Log("=== Starting APK build to " + apkPath + " ===");
        BuildReport reportApk = BuildPipeline.BuildPlayer(options);
        if (reportApk.summary.result == BuildResult.Succeeded)
            Debug.Log("APK build succeeded! File: " + apkPath);
        else
            Debug.LogError("APK build failed!");

        Debug.Log("=== Build script finished ===");

        // ========================
        // Удаление временного keystore
        // ========================
        if (!string.IsNullOrEmpty(tempKeystorePath) && File.Exists(tempKeystorePath))
        {
            File.Delete(tempKeystorePath);
            Debug.Log("Temporary keystore deleted.");
        }
    }
}