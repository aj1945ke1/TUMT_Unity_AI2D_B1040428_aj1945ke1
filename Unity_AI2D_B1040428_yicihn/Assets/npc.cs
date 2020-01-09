using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class npc : MonoBehaviour
{
    public enum state
    {
        start, notComplete, complete,end
    }

    public state _state;

    [Header("對話")]
    public string sayStart = "嗨!!!我要蒐集十顆櫻桃!!!";
    public string sayNotComplete = "還沒找到十顆櫻桃嗎!!!";
    public string sayComplete = "感謝找到櫻桃!!!";
    [Range(0.001f, 1.5f)]
    public float speed = 1.5f;
    public AudioClip soundSay;
    [Header("任務相關")]
    public bool complete;
    public int countPlayer;
    public int countFinish = 10;
    [Header("介面")]
    public GameObject objCanvas;
    public Text textSay;
    public GameObject gg;

    private AudioSource aud;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            Say();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            SayClose();
    }

    // Update is called once per frame
    private void Say()
    {
        objCanvas.SetActive(true);
        StopAllCoroutines();


        switch (_state)
        {
            case state.start:
                StartCoroutine(ShowDialog(sayStart));
                _state = state.notComplete;
                break;
            case state.notComplete:
                StartCoroutine(ShowDialog(sayNotComplete));
                break;
            case state.complete:
                StartCoroutine(ShowDialog(sayComplete));
                _state = state.end;
                Invoke("END", 1);
                break;
        }
    }

    private IEnumerator ShowDialog(string say)
    {
        textSay.text = "";

        for (int i = 0; i < say.Length; i++)
        {
            textSay.text += say[i].ToString();
            aud.PlayOneShot(soundSay, 0.6f);
            yield return new WaitForSeconds(speed);
        }
    }

    /// <summary>
    /// 關閉對話
    /// </summary>
    private void SayClose()
    {
        StopAllCoroutines();
        objCanvas.SetActive(false);
    }

    /// <summary>
    /// 玩家取得道具
    /// </summary>
    public void PlayerGet()
    {
        countPlayer++;
    }
    
    public void Complete()
    {
        _state = state.complete;
    }
    
    public void END()
    {
        gg.SetActive(true);
    }
}
