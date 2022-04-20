using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [Range(0,1)]
    public float audiosourceVolume;

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
            //Physics.Raycast(transform.position - new Vector3(0, 0.5f * controller.height , 0), Vector3.down, out RaycastHit hitt, 1f, floorLayer);
            //print(hitt.collider);

            if (controller.isGrounded && controller.velocity != Vector3.zero
                && Physics.Raycast(transform.position - new Vector3(0,0.5f*controller.height ,0),Vector3.down,out RaycastHit hit,1f,floorLayer))
            {
               
                if (hit.collider.TryGetComponent<Terrain>(out Terrain terrain))
                {
                    print("terrain");
                    yield return StartCoroutine(PlayerFootStepSoundfromTerrain(terrain,hit.point));

                }
                else if(hit.collider.TryGetComponent<Renderer>(out Renderer renderer))
                {
                    print("renderer");

                    yield return StartCoroutine(PlayerFootStepSoundfromRenderer(renderer));
                }
            }
            yield return null;
        }

    }
    private IEnumerator PlayerFootStepSoundfromTerrain(Terrain terrain,Vector3 Hitpoint)
    {
        Vector3 terrainPosition = Hitpoint - terrain.transform.position;
        Vector3 splatMapPosition = new Vector3(terrainPosition.x / terrain.terrainData.size.x, 0, terrainPosition.z / terrain.terrainData.size.z);

        int x = Mathf.FloorToInt(splatMapPosition.x * terrain.terrainData.alphamapWidth);
        int z = Mathf.FloorToInt(splatMapPosition.z * terrain.terrainData.alphamapHeight);

        float[,,] alphamap = terrain.terrainData.GetAlphamaps(x, z, 1, 1);

        if (!blendTerrainSounds)
        {
            int primaryIndex = 0;
            for (int i = 1; i < alphamap.Length; i++)
            {
                if (alphamap[0, 0, i] > alphamap[0,0, primaryIndex])
                {
                    primaryIndex = i;
                }
            }
            foreach (TextureSound textureSound in textureSounds)
            {
                if (textureSound.albedo == terrain.terrainData.terrainLayers[primaryIndex].diffuseTexture)
                {
                    AudioClip clip = GetclipFromTextureSound(textureSound);
                    audioSource.volume = audiosourceVolume;
                    audioSource.PlayOneShot(clip);
                    yield return new WaitForSeconds(clip.length);
                    break;
                }
            }

        }
        else
        {
            List<AudioClip> clips = new List<AudioClip>();
            int clipindex = 0;
            for (int i = 0; i <alphamap.Length ; i++)
            {
                if ( alphamap[0,0,i]>0)
                {
                    foreach (TextureSound textureSound in textureSounds)
                    {
                        if (textureSound.albedo==terrain.terrainData.terrainLayers[i].diffuseTexture)
                        {
                            AudioClip clip = GetclipFromTextureSound(textureSound);
                            audioSource.volume = audiosourceVolume;
                            audioSource.PlayOneShot(clip,alphamap[0,0,i]);
                            clips.Add(clip);
                            clipindex++;
                            break;
                        }
                    }
                }
            }
            float longestClip = clips.Max(clip => clip.length);

            yield return new WaitForSeconds(longestClip);
        }

    }
    private IEnumerator PlayerFootStepSoundfromRenderer(Renderer renderer)
    {
        foreach (TextureSound texturesound in textureSounds)
        {
            if (texturesound.albedo == renderer.material.GetTexture("_MainTex"))
            {
                print(texturesound);
                AudioClip clip = GetclipFromTextureSound(texturesound);
                audioSource.volume = audiosourceVolume;
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

