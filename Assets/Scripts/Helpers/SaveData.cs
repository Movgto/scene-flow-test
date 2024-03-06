using System.Linq;
using UnityEngine;

public sealed class SaveData : MonoBehaviour {
    public static SaveData Instance {  get; private set; }
    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            Instance = this;
        } else {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
    public void SaveAllData() {
        var objectsSaveData = FindObjectsOfType<MonoBehaviour>().OfType<ISaveData>();
        Debug.Log(objectsSaveData);

        foreach (ISaveData saveData in objectsSaveData) {
            Debug.Log(saveData);
            saveData.Save();
        }
    }
}
