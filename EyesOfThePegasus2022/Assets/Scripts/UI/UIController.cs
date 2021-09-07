using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text[] uiTexts = new Text[25];//typeof(TelemetryData).GetFields().Length];

    // Start is called before the first frame update
    void Start()
    {
        TelemetryData test = new TelemetryData();
        UpdateTexts(test);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTexts(TelemetryData telemetryData)
    {
        Debug.Log(uiTexts.Length);
        // for(int i=0;i<uiTexts.Length;i++) //tried getting data from string, but can't get it's value..
        // {
        //     Debug.Log(i);
        //     Text g = uiTexts[i];
        //     string testName = g.gameObject.name;
        //     var value = telemetryData.GetType().GetField(testName.Substring(5));
        //     if(value == null)
        //     {
        //         Debug.Log("null text");
        //         continue;
        //     }
        //     Debug.Log(value);  //find a way to test values
        //     g.text = testName + " : " + value.ToString();
        // }
        uiTexts[0].text = uiTexts[0].gameObject.name.Substring(5) + " : " + telemetryData._id;
        uiTexts[1].text = uiTexts[1].gameObject.name.Substring(5) + " : " + telemetryData.time;
        uiTexts[2].text = uiTexts[2].gameObject.name.Substring(5) + " : " + telemetryData.timer;
        uiTexts[3].text = uiTexts[3].gameObject.name.Substring(5) + " : " + telemetryData.started_at;
        uiTexts[4].text = uiTexts[4].gameObject.name.Substring(5) + " : " + telemetryData.heart_bpm;
        uiTexts[5].text = uiTexts[5].gameObject.name.Substring(5) + " : " + telemetryData.p_suit;
        uiTexts[6].text = uiTexts[6].gameObject.name.Substring(5) + " : " + telemetryData.t_sub;
        uiTexts[7].text = uiTexts[7].gameObject.name.Substring(5) + " : " + telemetryData.v_fan;
        uiTexts[8].text = uiTexts[8].gameObject.name.Substring(5) + " : " + telemetryData.p_o2;
        uiTexts[9].text = uiTexts[9].gameObject.name.Substring(5) + " : " + telemetryData.rate_o2;
        uiTexts[10].text = uiTexts[10].gameObject.name.Substring(5) + " : " + telemetryData.batteryPercent;
        uiTexts[11].text = uiTexts[11].gameObject.name.Substring(5) + " : " + telemetryData.battery_out;
        uiTexts[12].text = uiTexts[12].gameObject.name.Substring(5) + " : " + telemetryData.cap_battery;
        uiTexts[13].text = uiTexts[13].gameObject.name.Substring(5) + " : " + telemetryData.t_battery;
        uiTexts[14].text = uiTexts[14].gameObject.name.Substring(5) + " : " + telemetryData.p_h2o_g;
        uiTexts[15].text = uiTexts[15].gameObject.name.Substring(5) + " : " + telemetryData.p_h2o_l;
        uiTexts[16].text = uiTexts[16].gameObject.name.Substring(5) + " : " + telemetryData.p_sop;
        uiTexts[17].text = uiTexts[17].gameObject.name.Substring(5) + " : " + telemetryData.rate_sop;
        uiTexts[18].text = uiTexts[18].gameObject.name.Substring(5) + " : " + telemetryData.t_oxygenPrimary;
        uiTexts[19].text = uiTexts[19].gameObject.name.Substring(5) + " : " + telemetryData.t_oxygenSec;
        uiTexts[20].text = uiTexts[20].gameObject.name.Substring(5) + " : " + telemetryData.ox_primary;
        uiTexts[21].text = uiTexts[21].gameObject.name.Substring(5) + " : " + telemetryData.ox_secondary;
        uiTexts[22].text = uiTexts[22].gameObject.name.Substring(5) + " : " + telemetryData.t_oxygen;
        uiTexts[23].text = uiTexts[23].gameObject.name.Substring(5) + " : " + telemetryData.cap_water;
        uiTexts[24].text = uiTexts[24].gameObject.name.Substring(5) + " : " + telemetryData.t_water;
        uiTexts[25].text = uiTexts[25].gameObject.name.Substring(5) + " : " + telemetryData.__v;
    }
}
