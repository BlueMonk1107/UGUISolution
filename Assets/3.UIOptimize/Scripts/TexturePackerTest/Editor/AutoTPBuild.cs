using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class AutoTPBuild : Editor
{
    private static readonly string _sourceTexturePath = Application.dataPath + "/Source";
    private static readonly string _tpOutPath = Application.dataPath + "/Altas/";
    private static readonly string _texturePackerExePath = @"E:\TexturePacker\bin\TexturePacker.exe";
    
    [MenuItem("Tools/TPBuild")]
    public static void Build()
    {
        string[] folders = Directory.GetDirectories(_sourceTexturePath);
        string name;
        string sheetPath;
        ProcessStartInfo info;
        foreach (string folder in folders)
        {
            name = Path.GetFileName(folder);
            sheetPath = $"{_tpOutPath}{name}";
            info = GetStartInfo(_texturePackerExePath, GetCommand(sheetPath, ""));
            ExcuteProcess(info);
        }
        
        AssetDatabase.Refresh();
    }
    
    private static string GetCommand(string sheetPath,string folderPath)
    {
        StringBuilder sb = new StringBuilder();
        AddSubCommand(sb, $"--sheet {sheetPath}.png");
        AddSubCommand(sb, $"--data {sheetPath}.tpsheet");
        AddSubCommand(sb, "--format unity-texture2d");
        AddSubCommand(sb, "--trim-mode Polygon");
        AddSubCommand(sb, "--algorithm Polygon");
        AddSubCommand(sb, "--max-size 2048");
        AddSubCommand(sb, "--size-constraints POT");
        AddSubCommand(sb, $"{folderPath}");
        return sb.ToString();
    }
    
    private static void AddSubCommand(StringBuilder sb,string command)
    {
        sb.Append(" ");
        sb.Append(command);
    }

    private static ProcessStartInfo GetStartInfo(string exePath, string command)
    {
        ProcessStartInfo info = new ProcessStartInfo(exePath);
        info.Arguments = command;
        info.ErrorDialog = true;
        info.UseShellExecute = false;
        info.RedirectStandardOutput = true;
        info.RedirectStandardError = true;
        return info;
    }

    private static void ExcuteProcess(ProcessStartInfo info)
    {
        Process process = Process.Start(info);
        
        Debug.Log(process.StandardOutput.ReadToEnd());

        string error = process.StandardError.ReadToEnd();
        if (!string.IsNullOrEmpty(error))
        {
            Debug.LogError(error);
        }
        
        process.Close();
    }
}
