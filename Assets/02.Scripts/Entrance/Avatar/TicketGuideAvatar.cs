using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Text;
using System;

public class TicketGuideAvatar : MonoBehaviour
{
    public AudioClip[] audioClips;
    public GameObject player;
    //public GameObject answer;
    
    Animator anim;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //answer.SetActive(false);
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (audioSource.isPlaying)
        {
            Vector3 tr = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(tr);
        }
    }


    public IEnumerator Guide(int index, Action callback)
    {
        audioSource.clip = audioClips[index];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length + 1);
        //if (index == 1)
        //{
        //    yield return new WaitForSeconds(1.5f);
        //    answer.SetActive(false);
        //}
        index += 1;
        if (index < audioClips.Length)
        {
            StartCoroutine(Guide(index, callback));
            
        } else
        {
            // TODO : 앉아서 키보드 치는 애니메이션 적용
            //anim.SetTrigger("makeNFT");
            index = 0;
            StartCoroutine(makeWalletAndNFT());

            if (callback != null) callback();
        }
    }


    // TODO : 입장권 만드는 애니메이션 넣기 
    public IEnumerator makeWalletAndNFT()
    {
        FileInfo fi = new FileInfo("Assets/07.Json/TicketInfo.json");
        if (fi.Exists)
        {
            var ticketInfoJson = LoadJsonFile<PurchaseTicketModel>("Assets/07.Json", "TicketInfo");
            print(ticketInfoJson);
        } 
        else
        {
            using (UnityWebRequest request = UnityWebRequest.Post(Constant.BASE_URL + "/users/purchase/ticket", ""))
            {
                yield return request.SendWebRequest();

                // TODO: 실패 처리 
                if (request.result != UnityWebRequest.Result.Success)
                {
                    print(request.result);
                }
                else
                {
                    CreateJsonFile("Assets/07.Json", "TicketInfo", request.downloadHandler.text);
                }
            }
        }

    }


    void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }


    T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

}
