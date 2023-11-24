using System;
using UnityEngine;

public class ParticleManager : Singleton<ParticleManager>
{
    public static void PlayExplodedEffect(Transform parent)
    {
        __StartEffect(ParticleAssets.i.ExplodedEffect, parent, 5.0f);
    }

    public static void PlayDamagedPlayerEffect(Transform parent)
    {
        __StartEffect(ParticleAssets.i.DamagedPlayerEffect, parent, 1.0f);
    }

    public static void PlayHealedPlayerEffect(Transform parent)
    {
        __StartEffect(ParticleAssets.i.HealedPlayerEffect, parent, 1.0f);
    }

    public static void PlayExplodedHeliEffect(Transform parent)
    {
        __StartEffectOrphan(ParticleAssets.i.ExplodedHeliEffect, parent, 5.0f);
    }

    private static void __StartEffect(ParticleSystem effect, Transform parent, float lifetimeSeconds)
    {
        ParticleSystem particle = Instantiate(effect, parent);
        Destroy(particle.gameObject, lifetimeSeconds);
    }

    private static void __StartEffectOrphan(ParticleSystem effect, Transform location, float lifetimeSeconds)
    {
        ParticleSystem particle = Instantiate(effect, location.position, Quaternion.identity);
        Destroy(particle.gameObject, lifetimeSeconds);
    }

};