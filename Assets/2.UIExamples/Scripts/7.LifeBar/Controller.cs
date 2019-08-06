using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private LifeBar _bar;
    // Start is called before the first frame update
    void Start()
    {
        Canvas canvas = FindObjectOfType<Canvas>();

        if (canvas == null)
        {
            Debug.LogError("场景中没有Canvas组件");
            return;
        }
        SpwanLifeBar(canvas);
    }

    private void SpwanLifeBar(Canvas canvas)
    {
        GameObject prefab = Resources.Load<GameObject>("LifeBar");
        _bar = Instantiate(prefab, canvas.transform).AddComponent<LifeBar>();
        List<LifeBarData> data = new List<LifeBarData>();
        data.Add(new LifeBarData(null,Color.green));
        data.Add(new LifeBarData(null, Color.red));
        data.Add(new LifeBarData(null, Color.yellow));
        _bar.Init(transform,350, data);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Move(Vector3.right);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Move(Vector3.left);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Move(Vector3.down);
        }

        if (Input.GetKey(KeyCode.W))
        {
            Move(Vector3.up);
        }

        if (Input.GetMouseButtonDown(0))
        {
            _bar.ChangeLife(-50);
        }

        if (Input.GetMouseButtonDown(1))
        {
            _bar.ChangeLife(50);
        }
    }

    private void Move(Vector3 direction)
    {
        transform.Translate(direction * Time.deltaTime * 5);
    }
}
