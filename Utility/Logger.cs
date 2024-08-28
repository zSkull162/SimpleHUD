
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
/////////////////////////////////////// This is a custom debug logger I made, because I like to write my logs a specific way.
public static class Logger
{
    public static void Log(string name, string message, LogColor color, bool bold)
    {
        if (!bold)
        {
            switch (color)
            {
                case LogColor.Red:
                    Debug.Log($"<color=#f43f3f>{name}: {message}</color>");
                    break;

                case LogColor.Orange:
                    Debug.Log($"<color=#f58140>{name}: {message}</color>");
                    break;

                case LogColor.Yellow:
                    Debug.Log($"<color=#f5ee40>{name}: {message}</color>");
                    break;

                case LogColor.Lime:
                    Debug.Log($"<color=#40f540>{name}: {message}</color>");
                    break;

                case LogColor.Green:
                    Debug.Log($"<color=#258b25>{name}: {message}</color>");
                    break;

                case LogColor.Aqua:
                    Debug.Log($"<color=#40e9f5>{name}: {message}</color>");
                    break;

                case LogColor.LightBlue:
                    Debug.Log($"<color=#80def7>{name}: {message}</color>");
                    break;

                case LogColor.Blue:
                    Debug.Log($"<color=#4072f5>{name}: {message}</color>");
                    break;

                case LogColor.Purple:
                    Debug.Log($"<color=#9b40f5>{name}: {message}</color>");
                    break;

                case LogColor.Magenta:
                    Debug.Log($"<color=#f540f5>{name}: {message}</color>");
                    break;

                case LogColor.Rose:
                    Debug.Log($"<color=#f54099>{name}: {message}</color>");
                    break;

                case LogColor.White:
                    Debug.Log($"<color=white>{name}: {message}</color>");
                    break;
            }
        }
        else
        {
            switch (color)
            {
                case LogColor.Red:
                    Debug.Log($"<color=#f43f3f><b>{name}: {message}</b></color>");
                    break;

                case LogColor.Orange:
                    Debug.Log($"<color=#f58140><b>{name}: {message}</b></color>");
                    break;

                case LogColor.Yellow:
                    Debug.Log($"<color=#f5ee40><b>{name}: {message}</b></color>");
                    break;

                case LogColor.Lime:
                    Debug.Log($"<color=#40f540><b>{name}: {message}</b></color>");
                    break;

                case LogColor.Green:
                    Debug.Log($"<color=#258b25><b>{name}: {message}</b></color>");
                    break;

                case LogColor.Aqua:
                    Debug.Log($"<color=#40e9f5><b>{name}: {message}</b></color>");
                    break;

                case LogColor.LightBlue:
                    Debug.Log($"<color=#80def7><b>{name}: {message}</b></color>");
                    break;

                case LogColor.Blue:
                    Debug.Log($"<color=#4072f5><b>{name}: {message}</b></color>");
                    break;

                case LogColor.Purple:
                    Debug.Log($"<color=#9b40f5><b>{name}: {message}</b></color>");
                    break;

                case LogColor.Magenta:
                    Debug.Log($"<color=#f540f5><b>{name}: {message}</b></color>");
                    break;

                case LogColor.Rose:
                    Debug.Log($"<color=#f54099><b>{name}: {message}</b></color>");
                    break;

                case LogColor.White:
                    Debug.Log($"<color=white><b>{name}: {message}</b></color>");
                    break;
            }
        }
    }
}