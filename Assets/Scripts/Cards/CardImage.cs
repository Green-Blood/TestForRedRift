using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public sealed class CardImage : MonoBehaviour
    {
        [SerializeField] private Image cardImage;


        private ImageDownload _imageDownload;

        private void Awake()
        {
            _imageDownload = new ImageDownload(cardImage.mainTexture.width, cardImage.mainTexture.height);
            StartDownloadingImage();
        }

        private void StartDownloadingImage() => StartCoroutine(_imageDownload.DownloadImageCoroutine(SetImage));


        private void SetImage(Texture2D downloadedImage)
        {
            cardImage.sprite = Sprite.Create(downloadedImage,
                new Rect(0, 0, cardImage.mainTexture.width, cardImage.mainTexture.height), new Vector2());
        }
    }
}