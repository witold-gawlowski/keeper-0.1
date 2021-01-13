using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapButtonScript : MonoBehaviour
{
    public Image mapImage;
    public List<Image> blockImages;
    public List<Text> blockMultiplicityCaptions;
    public Text goalAndLimitText;
    public void Setup(LevelManager.MapData data)
    {
        mapImage.sprite = data.mapScript.GetSprite();
        for(int i=0; i<3; i++)
        {
            if (i < data.blockTypes.Count)
            {
                blockImages[i].gameObject.SetActive(true);
                Sprite s = BlockDictionary.instance.GetBlockSprite(data.blockTypes[i]);
                blockImages[i].sprite = s;
                blockMultiplicityCaptions[i].text = "x" +  data.blockMultiplicities[i].ToString();
            }
            else
            {
                blockImages[i].gameObject.SetActive(false);
            }
        }
        goalAndLimitText.text = "Goal: " + data.goal + "\n Limit: " + data.limit;
    }
}
