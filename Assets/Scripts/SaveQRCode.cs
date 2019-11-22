using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveQRCode : MonoBehaviour
{
	[SerializeField] RawImage image;
	[SerializeField] InputField text;

    public void SaveImage()
	{
		var texture = image.texture as Texture2D;
		var bytes = texture.EncodeToPNG();
		string directoryPath = Application.dataPath + "/" + "QRCodes/";
		if (!Directory.Exists(directoryPath))
		{
			//if it doesn't, create it
			Directory.CreateDirectory(directoryPath);

		}
		string path = directoryPath + text.text + ".png";
		var file = File.Open(path, FileMode.Create);
		var binary = new BinaryWriter(file);
		binary.Write(bytes);
		file.Close();
	}
}
