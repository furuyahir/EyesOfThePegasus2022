using UnityEngine;

public struct TelemetryData
{
    public string _id;
    public double time;
    public string timer;
    public string started_at;
    public int heart_bpm;
    public double p_suit;
    public double t_sub;
    public int v_fan;
    public double p_o2;
    public double rate_o2;
    public float batteryPercent;
    public int battery_out;
    public int cap_battery;
    public string t_battery;
    public double p_h2o_g;
    public double p_h2o_l;
    public int p_sop;
    public double rate_sop;
    public float t_oxygenPrimary;
    public int t_oxygenSec;
    public int ox_primary;
    public int ox_secondary;
    public string t_oxygen;
    public float cap_water;
    public string t_water;
    public double __v;

    public TelemetryData(string json)
    {
        _id = null;
        time = 0;
        timer = null;
        started_at = null;
        heart_bpm = 0;
        p_suit = 0;
        t_sub = 0;
        v_fan = 0;
        p_o2 = 0;
        rate_o2 = 0;
        batteryPercent = 0;
        battery_out = 0;
        cap_battery = 0;
        t_battery = null;
        p_h2o_g = 0;
        p_h2o_l = 0;
        p_sop = 0;
        rate_sop = 0;
        t_oxygenPrimary = 0;
        t_oxygenSec = 0;
        ox_primary = 0;
        ox_secondary = 0;
        t_oxygen = null;
        cap_water = 0;
        t_water = null;
        __v = 0;
    }

    public override string ToString()
    {
        return
            $"{nameof(_id)}: {_id}, {nameof(time)}: {time}, {nameof(timer)}: {timer}, " +
            $"{nameof(started_at)}: {started_at}, {nameof(heart_bpm)}: {heart_bpm}, " +
            $"{nameof(p_suit)}: {p_suit}, {nameof(t_sub)}: {t_sub}, {nameof(v_fan)}: {v_fan}, " +
            $"{nameof(p_o2)}: {p_o2}, {nameof(rate_o2)}: {rate_o2}, " +
            $"{nameof(batteryPercent)}: {batteryPercent}, {nameof(battery_out)}: {battery_out}, " +
            $"{nameof(cap_battery)}: {cap_battery}, {nameof(t_battery)}: {t_battery}, " +
            $"{nameof(p_h2o_g)}: {p_h2o_g}, {nameof(p_h2o_l)}: {p_h2o_l}, " +
            $"{nameof(p_sop)}: {p_sop}, " + $"{nameof(rate_sop)}: {rate_sop}, " +
            $"{nameof(t_oxygenPrimary)}: {t_oxygenPrimary}, " + 
            $"{nameof(t_oxygenSec)}: {t_oxygenSec}, {nameof(ox_primary)}: {ox_primary}, " +
            $"{nameof(ox_secondary)}: {ox_secondary}, {nameof(t_oxygen)}: {t_oxygen}, " +
            $"{nameof(cap_water)}: {cap_water}, {nameof(t_water)}: {t_water}, {nameof(__v)}: {__v}";
    }

    public static TelemetryData FromJson(string json)
    {
        return JsonUtility.FromJson<TelemetryData>(json);
    }
    
}
