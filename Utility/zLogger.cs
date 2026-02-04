
using UnityEngine;

public enum LogColor
{
    Red,
    Orange,
    Yellow,
    Lime,
    Green,
    Aqua,
    LightBlue,
    Blue,
    Purple,
    Magenta,
    Rose,
    Pink,
    White
}

public static class zLogger
{
    private static string GetHexCode(LogColor color) {
        switch (color) {
            case LogColor.Red: return "#f43f3f";
            case LogColor.Orange: return "#f58140";
            case LogColor.Yellow: return "#f5ee40";
            case LogColor.Lime: return "#40f540";
            case LogColor.Green: return "#258b25";
            case LogColor.Aqua: return "#40e9f5";
            case LogColor.LightBlue: return "#80def7";
            case LogColor.Blue: return "#4072f5";
            case LogColor.Purple: return "#9b40f5";
            case LogColor.Magenta: return "#f540f5";
            case LogColor.Rose: return "#f54099";
            case LogColor.Pink: return "#ff99cc";
            case LogColor.White: return "white";
            default: return "white";
        }
    }

    public static void Log(string name, string message, LogColor color, bool bold) {
        string hex = GetHexCode(color);
        string boldTagOpen = bold ? "<b>" : "";
        string boldTagClose = bold ? "</b>" : "";
        Debug.Log($"<color={hex}>{boldTagOpen}{name}: {message}{boldTagClose}</color>");
    }

    public static void Log(string name, string message, LogColor color) {
        string hex = GetHexCode(color);
        Debug.Log($"<color={hex}>{name}: {message}</color>");
    }

    public static void LogError(string name, string message) {
        string hex = GetHexCode(LogColor.Red);
        Debug.LogError($"<color={hex}>{name}: {message}</color>");
    }

    public static void LogWarning(string name, string message) {
        string hex = GetHexCode(LogColor.Yellow);
        Debug.LogWarning($"<color={hex}>{name}: {message}</color>");
    }
}