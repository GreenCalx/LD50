using UnityEngine;

public class Rocket : Module
{
    public bool is_traveling = false;
    public bool started_landing = false;
    public float travel_time = 20f;
    public int capacity = 0;
    public int n_villager = 0;
    public float last_take_off_time = 0f;
    public float take_off_speed = 0.3f;

    private Vector3 base_position;

    public AudioClip sound_take_off;
    public AudioClip sound_land;

    // Start is called before the first frame update
    void Start()
    {
        BaseStart();

        base_position = transform.position;
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
                else if (is_traveling)
                {
                    if (!started_landing && (Time.time - last_take_off_time) > (travel_time - 4.0f))
                    {
                        started_landing = true;
                        sound_player.clip = sound_land;
                        sound_player.Play();
                        transform.position = base_position + new Vector3(0, 4.0f * take_off_speed, 0);
                    }
                    else if ((Time.time - last_take_off_time) > travel_time)
                        Land();
                    else if (started_landing)
                        transform.Translate(take_off_speed * Time.deltaTime * transform.up);
                    else
                        transform.Translate(-take_off_speed * Time.deltaTime * transform.up);
                }


                break;
        }
    }

    new public bool Start_build()
    {
        if (!is_traveling)
            return BaseStart_build();
        else
            return false;
    }

    public bool isReady()
    {
        return ( ( level > 0 ) && (!is_traveling) && ( !is_building ) );
    }

    public void Take_off()
    {
        last_take_off_time = Time.time;
        is_traveling = true;

        sound_player.clip = sound_take_off;
        sound_player.Play();
    }

    public void Land()
    {
        n_villager = 0;
        is_traveling = false;
        started_landing = false;
        transform.position = base_position;
    }

    public bool Embark()
    {
        if (!is_traveling && !is_building && n_villager < capacity)
        {
            GameOver.score++;
            n_villager++;
            return true;
        }
        else
            return false;
    }
}
