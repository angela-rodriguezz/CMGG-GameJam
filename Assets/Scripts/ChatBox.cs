using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChatBox : MonoBehaviour
{
    public TextMeshProUGUI barText;
    public TextMeshProUGUI personNameText;

    private int sentenceIndex = -1;
    public StoryScene currentScene;
    public Image imageHolder;
    public static bool finished;

    private IEnumerator lineAppear;


    private State state = State.COMPLETED;

    private enum State
    {
        PLAYING, COMPLETED
    }

    public void PlayScene(StoryScene scene)
    {
        currentScene = scene;
        sentenceIndex = -1;
        PlayNextSentence();
    }

    // Start is called before the first frame update
    public void PlayNextSentence()
    {
        lineAppear = TypeText(currentScene.sentences[++sentenceIndex].text);
        StartCoroutine(lineAppear);
        personNameText.text = currentScene.sentences[sentenceIndex].speaker.speakerName;
        personNameText.color = currentScene.sentences[sentenceIndex].speaker.textColor;
        imageHolder.sprite = currentScene.sentences[sentenceIndex].speaker.characterSprite;
       
        imageHolder.preserveAspect = true;
       
        
    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    public bool IsLastSentence()
    {
        return sentenceIndex + 1 == currentScene.sentences.Count;
    }

    public void FinishSentence()
    {
        barText.text = currentScene.sentences[sentenceIndex].text;
    }



    private IEnumerator TypeText(string text)
    {
        barText.text = "";
        state = State.PLAYING;
        int wordIndex = 0;

        while (state != State.COMPLETED)
        {
            barText.text += text[wordIndex];
            yield return new WaitForSeconds(0.05f);
            if (++wordIndex == text.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }
    }

    void Update()
    {
        if (finished)
        {
            StopCoroutine(lineAppear);
            state = State.COMPLETED;
            finished = false;
        }
    }
}
