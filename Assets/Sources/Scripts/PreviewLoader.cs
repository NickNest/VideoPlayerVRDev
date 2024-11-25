using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.IO;
using UnityEngine.Networking;
using DG.Tweening;

public class PreviewLoader : MonoBehaviour
{
    [SerializeField] private int _prewiewLoadWaiting = 1;
    [SerializeField] private LoadingCircle _loadingCircle;

    [SerializeField] private VideoSO _videoSO;

    private string _cacheDirectory = "Cache";
    private string _localPath;
    private RawImage _previewImage;

    private void Start()
    {
        _previewImage = GetComponent<RawImage>();
        _previewImage.color = new Color(255, 255, 255, 0);
        LoadPrewiew().Forget();
    }

    private async UniTaskVoid LoadPrewiew()
    {
        _localPath = GetCachePath(_videoSO.PreviewURLPath);
        _previewImage.texture = await LoadOrDownloadTexture(_videoSO.PreviewURLPath, _localPath);
        ShowPreview();
    }

    private string GetCachePath(string url)
    {
        string fileName = Path.GetFileName(url);
        return Path.Combine(Application.persistentDataPath, _cacheDirectory, fileName);
    }

    private async UniTask<Texture2D> LoadOrDownloadTexture(string url, string localPath)
    {
        if (File.Exists(localPath))
        {
            Debug.Log($"Loading Image from cache: {localPath}");
            return await LoadTextureFromFile(localPath);
        }

        Debug.Log($"Loading Image from url: {url}");

        _loadingCircle.gameObject.SetActive(true);
        _loadingCircle.StartRotation();

        var texture = await DownloadTexture(url);

        SaveTextureToFile(texture, localPath);
        return texture;
    }

    private async UniTask<Texture2D> LoadTextureFromFile(string filePath)
    {
        byte[] fileData = await UniTask.Run(() => File.ReadAllBytes(filePath));
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);
        return texture;
    }

    private async UniTask<Texture2D> DownloadTexture(string url)
    {
        using UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        await request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Loading error: {request.error}");
            return null;
        }
        await UniTask.WaitForSeconds(_prewiewLoadWaiting);

        _loadingCircle.StopRotation();
        _loadingCircle.gameObject.SetActive(false);

        return DownloadHandlerTexture.GetContent(request);
    }

    private void SaveTextureToFile(Texture2D texture, string filePath)
    {
        string directory = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        byte[] textureData = texture.EncodeToPNG();
        File.WriteAllBytes(filePath, textureData);

        Debug.Log($"Save Image to cache: {filePath}");
    }

    private void ShowPreview()
    {
        _previewImage.DOFade(1f, 0.5f);
    }
}
