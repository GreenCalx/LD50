using UnityEngine;

public class Rocket : Module
{
    public bool is_traveling = false;
    public float travel_time = 20f;
    public int capacity = 0;
    public int n_villager = 0;
    public float last_take_off_time = 0f;
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        BaseStart();
    }

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();

        capacity = 10 * level;

        switch (level)
        {
            case 0:
                break;

            default:
                if (!is_traveling && !is_building && n_villager >= capacity)
                    Take_off();
                else if (is_traveling && (Time.time - last_take_off_time > travel_time))
                    Land();

                break;
        }
    }

    new public void Start_build()
    {
        if (!is_traveling)
            BaseStart_build();
    }

    public void Take_off()
    {
        last_take_off_time = Time.time;
        is_traveling = true;
        GetComponent<MeshFilter>().mesh = models[0];
    }

    public void Land()
    {
        score += n_villager;
        n_villager = 0;
        is_traveling = false;
        GetComponent<MeshFilter>().mesh = models[level];
    }

    public bool Embark()
    {
        if (!is_traveling && !is_building && n_villager < capacity)
        {
            n_villager++;
            return true;
        }
        else
            return false;
    }
}
