using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleCtrl : MonoBehaviour
{
    public float wait = 4f;
    public float innerWait = 0.15f;
    public string title = "Dog VS Cat";
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private IEnumerator Start()
    {
        StartCoroutine(Typing(title));
        yield return null;
    }

    public IEnumerator Typing(string _massage)
    {
        yield return new WaitForSeconds(wait);
        text.text = null;

        for (int i = 0; i < _massage.Length; i++)
        {
            text.text += _massage[i];
            print(_massage[i]);
            yield return new WaitForSeconds(innerWait);
        }
    }

}
