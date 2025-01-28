using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPSScript : MonoBehaviour
{
    // Ссылка на текстовое поле для отображения FPS
    public TextMeshProUGUI fpsText;

    // Таймер для обновления значения FPS
    private float deltaTime = 0.0f;

    void Update()
    {
        // Обновляем таймер
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        // Вычисляем FPS
        int fps = Mathf.RoundToInt(1.0f / deltaTime);

        // Отображаем FPS в текстовом поле, если оно задано
        if (fpsText != null)
        {
            fpsText.text = "FPS: " + fps.ToString();
        }
        else
        {
            // Если текстовое поле не задано, используем стандартный GUI для отображения FPS
            GUIStyle style = new GUIStyle();
            Rect rect = new Rect(10, 10, Screen.width, Screen.height * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = Screen.height * 2 / 100;
            style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
            string text = string.Format("FPS: {0}", fps);
            GUI.Label(rect, text, style);
        }
    }
}   