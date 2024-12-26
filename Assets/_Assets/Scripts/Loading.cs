using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loading
{
    //Класс для логики загрузки

    public enum Scene
    {
        MainMenu,
        Game,
        Loading
    }
    private static Scene _targetScene;

    public static void Load(Scene _targetScene)
    {
        Loading._targetScene = _targetScene;

        SceneManager.LoadScene(Scene.Loading.ToString());

    }

    public static void LoadingCallback()
    {
        SceneManager.LoadScene(_targetScene.ToString());
    }


}
