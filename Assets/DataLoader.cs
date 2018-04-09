using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public string[] _items;

	void Start ()
    {
        StartCoroutine(LoadData());		
	}
	
	void Update ()
    {
		
	}

    private IEnumerator LoadData()
    {
        WWW itemsData = new WWW("http://localhost/youtube_test/itemData.php");
        yield return itemsData;
        string s = itemsData.text;
        Debug.Log(s);
        _items = s.Split(';');

        Debug.Log(GetDataValue(_items[0], "name:"));
    }

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
        {
            value = value.Remove(value.IndexOf("|"));
        }
        return value;
    }

}
