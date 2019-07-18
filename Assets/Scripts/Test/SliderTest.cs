using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeValue());
    }

    private IEnumerator ChangeValue()
    {
        Slider slider = GetComponent<Slider>();
        float process = 0;

        while (process < 1)
        {
            process += 0.1f;
            
            yield return new WaitUntil(() =>
            {
                slider.value = Mathf.SmoothStep(slider.value, process, 0.5f);
                return process - slider.value <= 0.01f;
            });
        }
    }
}
