using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FootStepSound : MonoBehaviour
{
    [SerializeField]
    private LayerMask floorLayer;
    [SerializeField]
    private TextureSound[] textureSounds;
    [SerializeField]
    private bool blendTerrainSounds;

    private CharacterController controller;
    private AudioSource audioSource;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        StartCoroutine(CheckGround());
    }

   private IEnumerator CheckGround()
    {
        while (true)
        {
            if (controller.isGrounded && controller.velocity !=Vector3.zero&& 
                Physics.Raycast(transform.position - new Vector3(0,0.5f*controller.height+0.5f*controller.radius,0),Vector3.down,out RaycastHit hit,1f,floorLayer))
            {
                if (hit.collider.TryGetComponent<Terrain>(out Terrain terrain))
                {
                    yield return StartCoroutine(PlayerFootStepSoundfromTerrain(terrain));

                }
                else if(hit.collider.TryGetComponent<Renderer>(out Renderer renderer))
                {
                    yield return StartCoroutine(PlayerFootStepSoundfromRenderer(renderer));
                }
            }
        }
    }
    private IEnumerator PlayerFootStepSoundfromTerrain(Terrain terrain)
    {
        yield return null;
    }
    private IEnumerator PlayerFootStepSoundfromRenderer(Renderer renderer)
    {
        foreach (TextureSound texturesound in textureSounds)
        {
            if (texturesound.albedo == renderer.material.GetTexture("_MainTex"))
            {
                AudioClip clip = GetclipFromTextureSound(texturesound);

                audioSource.PlayOneShot(clip);
                yield return new WaitForSeconds(clip.length);
                break;
            }
        }
    }
    private AudioClip GetclipFromTextureSound(TextureSound TextureSound)
    {
        int clipIndex = Random.Range(0, TextureSound.clips.Length);
        return TextureSound.clips[clipIndex];

    }

    [System.Serializable]
    private class TextureSound
    {
        public Texture albedo;
        public AudioClip[] clips;
    }
}

