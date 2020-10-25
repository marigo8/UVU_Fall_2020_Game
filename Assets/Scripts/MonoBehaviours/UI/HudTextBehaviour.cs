using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HudTextBehaviour : MonoBehaviour
{
    public ScriptableData[] datas;
    
    private Text textObj;

    private void Start()
    {
        textObj = GetComponent<Text>();
    }

    private void Update()
    {
        var text = datas.Aggregate("", (current, data) => current + (data.GetString() + "\n"));
        textObj.text = text;
    }
}
