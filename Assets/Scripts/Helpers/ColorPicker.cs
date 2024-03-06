using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour, ISaveData
{
    public Color[] AvailableColors;
    public Button ColorButtonPrefab;
    
    public static Color SelectedColor { get; private set; }
    public Action<Color> onColorChanged;

    List<Button> m_ColorButtons = new List<Button>();

    [Serializable]
    public class ColorData {
        public Color color;
    }
        
    public void Init()
    {

        foreach (var color in AvailableColors)
        {
            var newButton = Instantiate(ColorButtonPrefab, transform);
            newButton.GetComponent<Image>().color = color;
            
            newButton.onClick.AddListener(() =>
            {
                SelectedColor = color;
                foreach (var button in m_ColorButtons)
                {
                    button.interactable = true;
                }

                newButton.interactable = false;
                
                onColorChanged.Invoke(SelectedColor);
            });
            
            m_ColorButtons.Add(newButton);
        }
    }

    public void SelectColor(Color color)
    {
        for (int i = 0; i < AvailableColors.Length; ++i)
        {
            if (AvailableColors[i] == color)
            {
                m_ColorButtons[i].onClick.Invoke();
            }
        }
    }

    public void Save() {
        if (SelectedColor != null) {
            ColorData colorData = new ColorData();
            colorData.color = SelectedColor;
            Debug.Log(colorData.color);
            string json = JsonUtility.ToJson(colorData);

            File.WriteAllText(Application.persistentDataPath + "/saveData.json", json);
        }
    }

    public void Load() {
        string path = Application.persistentDataPath + "/saveData.json";
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);

            ColorData colorData = JsonUtility.FromJson<ColorData>(json);
            SelectedColor = colorData.color;
        }
    }
}
