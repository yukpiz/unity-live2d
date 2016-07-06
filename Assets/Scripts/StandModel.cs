using UnityEngine;
using System.Collections;
using live2d;
using live2d.framework;

public class StandModel : MonoBehaviour
{
	public TextAsset mocFile; //モデルファイル
	public Texture2D[] textureFiles; //テクスチャファイル

	private Live2DModelUnity live2DModel;
	private EyeBlinkMotion eyeBlink = new EyeBlinkMotion();

	public void Start()
	{
		Live2D.init (); //Live2Dの初期化
		//mocファイルの読み込み
		live2DModel = Live2DModelUnity.loadModel (mocFile.bytes);

		//テクスチャファイルの読み込み
		int index = 0;
		foreach (Texture2D textureFile in textureFiles) {
			live2DModel.setTexture (index, textureFile);
			index++;
		}
	}

	public void Update()
	{
		float modelWidth = live2DModel.getCanvasWidth (); //モデルのキャンバスの横幅
		Matrix4x4 m1 = Matrix4x4.Ortho(0, modelWidth, modelWidth, 0, -50.0f, 50.0f);
		Matrix4x4 m2 = transform.localToWorldMatrix;
		Matrix4x4 m3 = m2 * m1;

		live2DModel.setMatrix (m3);

		//まばたきの間隔とモーションにかかる時間を設定
		eyeBlink.setInterval (6000);
		eyeBlink.setEyeMotion (100, 100, 100);
		eyeBlink.setParam (live2DModel);

		live2DModel.update ();

	}

	public void OnRenderObject()
	{
		live2DModel.draw ();
	}
}
