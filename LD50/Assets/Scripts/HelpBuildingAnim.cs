using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpBuildingAnim : MonoBehaviour
{
    Help helpRef;
    bool is_animating;

    public Transform start;
    public Transform end;

    public Vector3 initScale;
    private float startAnimTime;
    private bool revert;
    public int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions
    int elapsedFrames = 0;

    // Start is called before the first frame update
    void Start()
    {
        helpRef = GetComponentInParent<Help>();
        is_animating = false;
        revert = false;
        initScale = transform.localScale;
        transform.position = start.position;
        startAnimTime = 0f;
        hide();
    }

    void hide()
    {
        transform.localScale = new Vector3(0,0,0);
    }

    void show()
    {
        transform.localScale = initScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (helpRef.is_building)
        {
            animate();
        } else if (is_animating) {
            stopAnimate();
        }
    }

    private void animate()
    {
        if (!is_animating)
        {
            show();
            transform.position = start.position;
            is_animating = true;
            startAnimTime = Time.time;
        }

        float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
        if (!revert)
            transform.position = Vector3.Lerp( start.position, end.position, interpolationRatio);
        else
            transform.position = Vector3.Lerp( end.position, start.position, interpolationRatio);
        elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);

        if ((transform.position.y > start.position.y)||(transform.position.y < end.position.y))
        {
            revert =! revert;
        }
    }
    private void stopAnimate()
    {
        hide();
        revert = false;
        is_animating = false;
        transform.position = start.position;
    }
}
