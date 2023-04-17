using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlastMat;
    [SerializeField] private float restoreDefaultMattime = 0.2f;

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;
    }

    public float GetRestoreMatTime(){
        return restoreDefaultMattime;
    }

    public IEnumerator FlashRoutine(){
        spriteRenderer.material = whiteFlastMat;
        yield return new WaitForSeconds(restoreDefaultMattime);
        spriteRenderer.material = defaultMat;
    }
}
