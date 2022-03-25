using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Text;

public class TicketGuideAvatar : MonoBehaviour
{
    public AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (OVRInput.GetDown(OVRInput.Button.Two))
        //{
        //    StartCoroutine("Guide", 0);
        //}

    }

    // TODO : 대화 속도 조절하기
    public IEnumerator Guide(int index)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[index];
        audioSource.Play();
        yield return new WaitForSeconds(7f);
        index += 1;
        if (index < audioClips.Length)
        {
            StartCoroutine("Guide", index);
        } else
        {
            index = 0;
            StartCoroutine(makeWalletAndNFT());
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
