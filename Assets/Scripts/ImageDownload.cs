using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

public sealed class ImageDownload
{
    private readonly float _height;
    private readonly float _width;

    public ImageDownload(float height, float width)
    {
        _height = height;
        _width = width;
    }

    public IEnumerator DownloadImageCoroutine(Action<Texture2D> onImageDownload)
    {
        using var request = UnityWebRequestTexture.GetTexture(
            $"https://picsum.photos/" +
            $"{_height.ToString(CultureInfo.InvariantCulture)}/{_width.ToString(CultureInfo.InvariantCulture)}");
        yield return request.SendWebRequest();

        if (IsError(request)) Debug.Log(request.error);
        else if (IsSuccess(request)) TellDownloadFinished(onImageDownload, request);
    }

    private static bool IsSuccess(UnityWebRequest request) => request.result == UnityWebRequest.Result.Success;

    private static bool IsError(UnityWebRequest uwr) =>
        uwr.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError
            or UnityWebRequest.Result.DataProcessingError;

    private static void TellDownloadFinished(Action<Texture2D> onImageDownload, UnityWebRequest request)
    {
        var image = DownloadHandlerTexture.GetContent(request);
        onImageDownload?.Invoke(image);
    }
}