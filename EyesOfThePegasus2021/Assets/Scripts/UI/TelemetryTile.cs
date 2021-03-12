using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelemetryTile : MonoBehaviour
{
    public Image TileImage;
    public Text Text;
    public void Init(Sprite spriteToUse)
    {
        TileImage.sprite = spriteToUse;
    }

    public void UpdateText(string text)
    {
        Text.text = text;
    }
}
