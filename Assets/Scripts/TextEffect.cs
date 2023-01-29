using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class TextEffect : MonoBehaviour
{
    public TMP_Text textComponent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;
        //Debug.Log("textInfo==="+textInfo.ToString());    

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];
            //Debug.Log("charInfo===="+charInfo.ToString());


            if(!charInfo.isVisible) continue;

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
            //Debug.Log("verts======"+verts);

            for (int k = 0; k < 4; k++)
            {
                var orig = verts[charInfo.vertexIndex + k];
                verts[charInfo.vertexIndex + k] = orig + new Vector3(0, Mathf.Sin(Time.time*2f + orig.x * 0.01f) * 10f, 0);
                //Debug.Log("orig====" + orig);
                //Debug.Log("verts[charInfo.vertexIndex + k]===" + verts[charInfo.vertexIndex + k]);
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
