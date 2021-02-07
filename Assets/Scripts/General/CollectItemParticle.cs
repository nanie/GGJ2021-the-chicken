using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class CollectItemParticle : MonoBehaviour
{
    public Sprite test;

    ParticleSystem _particle;

    void Start()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    public void ShowItem(Vector2 position, Sprite icon)
    {
        transform.position = position;
        StartCoroutine(ShowParticle(icon));
    }
    IEnumerator ShowParticle(Sprite icon)
    {
        yield return null;
        ParticleSystem.MainModule psMain = _particle.main;
        ParticleSystem.TextureSheetAnimationModule txMod = _particle.textureSheetAnimation;
        txMod.SetSprite(0, icon);
        _particle.Emit(1);
        yield return new WaitForSeconds(psMain.duration);
    }
}
