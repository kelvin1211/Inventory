using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomIn : MonoBehaviour
{
    public void zoomIn()
    {
        GameManager.instance. Fade();
        GameManager.instance. DownArrow.gameObject.SetActive(true);
        StartCoroutine(zoomin());
    }

    public IEnumerator zoomin()
    {
        yield return new WaitForSeconds(0.3f);
        //Room00.localScale = new Vector3(5, 5, 1);
        //Debug.Log("GMroomType===" + roomTypeObj.name + "==localscale==" + roomTypeObj.transform.localScale.x);
        GameManager.instance.roomTypeObj.transform.localScale = new Vector3(5, 5, 1);
    }
}
