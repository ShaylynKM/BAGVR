using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    private bool _isActive;
    private float time = 0;
    private TextMeshProUGUI _text;
    // Start is called before the first frame update
    void Start()
    {
        TutorialManager.Instance.EndTutorial.AddListener(Activate);
        _isActive = false;
        _text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isActive)
        {
            time += Time.deltaTime;
        }
        _text.text = $"Time Survived: {time}";
    }

    public void Activate()
    {
        _isActive = true;
    }
}
