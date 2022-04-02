using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : Subscriber
{
    [Header("mand")]
    private PlayerController PC;
    private ModeManager MM;
    private PlayerWeapon PW;
    private Camera cam;
    private Dictionary<Enemy,GameObject> dicoEnemyCursors;

    [Header("tweaks")]
    public Color locked_color;
    public Color locking_color;
    public GameObject aim_cursor;


    // Start is called before the first frame update
    void Start()
    {
        MM = GetComponentInParent<ModeManager>();
        cam = MM.main_cam;
        dicoEnemyCursors = new Dictionary<Enemy, GameObject>();

        PC = MM.player.GetComponent<PlayerController>();
        if (!!PC)
            PW = PC.gameObject.GetComponentInChildren<PlayerWeapon>();
        if (PW == null)
            gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        refreshAimed();
        refreshLocked();
        refreshOutOfRangeEnemies();
    }

    // Subscriber contract
    public override void notify(GameObject iGO)
    {
        Enemy e = iGO.GetComponent<Enemy>();
        if (e==null)
            return;
            
        GameObject cursor;
        if (dicoEnemyCursors.TryGetValue(e, out cursor))
        {
            dicoEnemyCursors.Remove(e);
            Destroy(cursor);
        }
    }

    // Methods
    public void refreshOutOfRangeEnemies()
    {
        List<Enemy> toclean = new List<Enemy>();
        foreach( Enemy e in dicoEnemyCursors.Keys)
        {
            if (!e.is_locked_by_player)
            {
                toclean.Add(e);
            }
        }

        foreach(Enemy e in toclean)
        {
            GameObject cursor;
            dicoEnemyCursors.TryGetValue(e, out cursor);
            dicoEnemyCursors.Remove(e);
            Destroy(cursor.gameObject);
        }
    }

    public void refreshAimed()
    {
        Vector2 UIsizes = GetComponent<CanvasScaler>().referenceResolution;


        foreach( Enemy e in PW.in_range_enemies)
        {
            if (dicoEnemyCursors.ContainsKey(e))
                continue;
            
            Vector3 position = cam.WorldToScreenPoint(e.transform.position);
            GameObject cursor = Instantiate(aim_cursor, transform);
            cursor.GetComponent<RectTransform>().anchoredPosition = position - new Vector3(UIsizes.x / 2, UIsizes.y / 2, 0);
            
            dicoEnemyCursors.Add(e, cursor);

            RawImage im = cursor.GetComponent<RawImage>();
            im.color = locking_color;
        }
    }
    public void refreshLocked()
    {
        Vector2 UIsizes = GetComponent<CanvasScaler>().referenceResolution;

        foreach( Enemy e in PW.locked_enemies)
        {
            if (!dicoEnemyCursors.ContainsKey(e))
                continue;

            GameObject cursor;
            
            if ( dicoEnemyCursors.TryGetValue( e, out cursor) ) 
            {
                Vector3 position = cam.WorldToScreenPoint(e.transform.position);
                cursor.GetComponent<RectTransform>().anchoredPosition = position - new Vector3(UIsizes.x / 2, UIsizes.y / 2, 0);

                RawImage im = cursor.GetComponent<RawImage>();
                im.color = locked_color;
            }
        }
    }
}
