using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts;

public class LineDemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string name1 = "test";
        string name2 = "test2";

        LineChart chart = GetComponent<LineChart>();

        chart.title.show = true;
        chart.title.text = "Line Text";

        chart.tooltip.show = true;

        chart.legend.show = true;
        chart.legend.data.Clear();
        chart.legend.data.Add(name1);
        chart.legend.data.Add(name2);

        chart.xAxises[0].show = true;
        chart.xAxises[1].show = false;
        chart.yAxises[0].show = true;
        chart.yAxises[1].show = false;

        chart.xAxises[0].type = Axis.AxisType.Value;
        chart.yAxises[0].type = Axis.AxisType.Category;


        int[] data1 = {10, 20, 30, 10, 50};
        int[] data2 = { 50, 60, 10, 50, 10 };
        chart.xAxises[0].splitNumber = data1.Length;

        chart.RemoveData();

        for (int i = 0; i < data1.Length; i++)
        {
            chart.AddYAxisData(i.ToString());
        }

        chart.AddSerie(name1, SerieType.Line);
        chart.AddSerie(name2, SerieType.Line);

        foreach (int i in data1)
        {
            chart.AddData(name1, i);
        }

        foreach (int i in data2)
        {
            chart.AddData(name2, i);
        }
    }
}
