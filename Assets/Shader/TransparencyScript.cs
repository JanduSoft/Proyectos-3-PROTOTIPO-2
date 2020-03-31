using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyScript : MonoBehaviour
{
    [SerializeField] List<GameObject> go = new List<GameObject>();
    [SerializeField] float fadeTransparecyTime = 0.2f;
    [SerializeField] float fadeOpaqueTime = 1f;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            foreach( GameObject _go in go)
            {
                SetMaterialTransparent(_go);
                iTween.FadeTo(_go, 0, fadeTransparecyTime);
            }
        }
    }

    private void SetMaterialTransparent(GameObject gameobject)
    {
        foreach (Material m in gameobject.GetComponent<Renderer>().materials)
        {
            m.SetFloat("_Mode", 2);
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            m.SetInt("_ZWrite", 0);
            m.DisableKeyword("_ALPHATEST_ON");
            m.EnableKeyword("_ALPHABLEND_ON");
            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            m.renderQueue = 3000;
        }
    }

    private void SetMaterialOpaque(GameObject gameobject)
    {
        foreach (Material m in gameobject.GetComponent<Renderer>().materials)
        {
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            m.SetInt("_ZWrite", 1);
            m.DisableKeyword("_ALPHATEST_ON");
            m.DisableKeyword("_ALPHABLEND_ON");
            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            m.renderQueue = -1;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            foreach (GameObject _go in go)
            {
                // Set material to opaque
                iTween.FadeTo(_go, 1, fadeOpaqueTime);
                StartCoroutine(setOpaqueInSeconds(fadeOpaqueTime, _go));
            }
        }
    }
    IEnumerator setOpaqueInSeconds(float time, GameObject _go)
    {
        yield return new WaitForSeconds(time);
        SetMaterialOpaque(_go);
    }
}

