using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using OpenAI;

public class ImageDetectionManager : MonoBehaviour
{
    [SerializeField] private Texture2D trueImg;
    [SerializeField] private int textureSize = 128;

    void Start()
    {
        TakeScreenshotAndCompare();
    }

    public void TakeScreenshotAndCompare()
    {
        StartCoroutine(CompareScreenShotToReference());
    }

    private IEnumerator CompareScreenShotToReference()
    {
        yield return new WaitForEndOfFrame();
        Texture2D screenShot = ScreenCapture.CaptureScreenshotAsTexture(1);
        
        string testStr = GenerateStringFromImage(screenShot);
        string trueStr = GenerateStringFromImage(trueImg);

        CompareStrings(testStr, trueStr);
    }

    private string GenerateStringFromImage(Texture2D tex)
    {
        tex = ResizeTexture(tex, textureSize, textureSize);
        byte[] pngBytes = tex.EncodeToPNG();
        Color[] pix = tex.GetPixels();
        string resultString = "";
        string row = "";

        for (int i = 0; i < pix.Length; ++i)
        {
            if (pix[i].grayscale > 0.5)
            {
                row += "x";
            }
            else
            {
                row += "*";
            }

            if (i % textureSize == textureSize - 1)
            {
                row += "\n";
                resultString = row + resultString;
                row = "";
            }
        }

        return resultString;
    }


    private float CompareStrings(string str1, string str2)
    {
        int similarCount = 0;
        for (int i = 0; i < str1.Length; ++i)
        {
            if (str1[i] == str2[i])
            {
                similarCount++;
            }
        }
        
        Debug.Log("Similarity score: " + (float)similarCount / (float)str1.Length);
        
        return (float)similarCount / (float)str1.Length;
    }
    
    private Texture2D ResizeTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        RenderTexture rt = RenderTexture.GetTemporary(targetWidth, targetHeight);
        
        Graphics.Blit(source, rt);
        
        RenderTexture previousActive = RenderTexture.active;
        RenderTexture.active = rt;
        
        Texture2D result = new Texture2D(targetWidth, targetHeight);
        
        result.ReadPixels(new Rect(0, 0, targetWidth, targetHeight), 0, 0);
        result.Apply();
 
        RenderTexture.active = previousActive;
        RenderTexture.ReleaseTemporary(rt);

        return result;
    }
    
    
    
    
    /* DEPRECATED */
    private OpenAIApi openai = new OpenAIApi("");

    private void SendImageToOpenAI(string brightnessString)
    {
        Debug.Log("SendImageToOpenAI");
        
        var contentString = @"{
            ""model"": ""gpt-4o"",
            ""messages"": [{
                ""role"": ""user"",
                ""content"": [
                    {""type"": ""text"", ""text"": """ + "How much does this text resemble the word hello when parsed as a 128x128 image of x's and *'s: " + brightnessString + @"""},
                ]
            }]
        }";

        //string contentString = JsonConvert.SerializeObject(content);
    
        var messages = new List<ChatMessage>();
        messages.Add(new ChatMessage()
        {
            Role = "user",
            Content = contentString
        });

        PostRequestToOpenAI(messages);
    }

    private async void PostRequestToOpenAI(List<ChatMessage> jsonBody)
    {
        Debug.Log("PostRequestToOpenAI");
        
        var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
        {
            Model = "gpt-4.1-mini",
            Messages = jsonBody
        });

        if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
        {
            var message = completionResponse.Choices[0].Message;
            Debug.Log("Response: " + message.Content);
        }
        else
        {
            Debug.Log("Poopoo");
        }
    }
}
