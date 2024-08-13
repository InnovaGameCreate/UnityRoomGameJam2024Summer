
using System.Collections.Generic;

public static class SceneDictionary
{
    public static Dictionary<SceneType, string> TypeOfName = new()
    {
        [SceneType.Unknown] = "",
        [SceneType.Title] = "�吼_Title",
        [SceneType.HowToPlay] = "�吼_Describe",
        [SceneType.Credit] = "�吼_Credit",
        [SceneType.InGame] = "�吼Test",
        [SceneType.Result] = "�吼_�V�[���J��Test"
    };
}

