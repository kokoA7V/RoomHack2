using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{

	public AsyncOperation async;

	public Text loadingText;
	public Slider loadingBar;

	public static int SL = 0;

	[SerializeField] Fade fade;

	public string[] sceneStr = new string[20]
	{
		"Scene2",
		"TitleScene",
		"HomeScene",
		"TutorialScene",
		"",
		"",
		"",
		"",
		"",
		"",
		"",
		"1stStage",
		"2ndStage",
		"3rdStage",
		"4thStage",
		"5thStage",
		"6thStage",
		"7thStage",
		"8thStage",
		"9thStage",
	}; 

	void Start()
	{
		//読み込み終了パーセントの表示
		loadingText.text = "0%".ToString();

		//読み込み完了バーの表示
		loadingBar.value = 0;

		//1秒かけてFadeOutし、SceneLoadを読み込む
		fade.FadeOut(1f, () => StartCoroutine("SceneLoad"));
	}

	public IEnumerator SceneLoad()
	{
		//読み込みたいシーンが増えたらelseifを使って増やしていく
		async = SceneManager.LoadSceneAsync(sceneStr[SL]);

		//ロード完了してもシーン移行しないようにする
		async.allowSceneActivation = false;

		//読み込み中の処理
		while (async.progress < 0.9f)
		{
			//読み込み終了パーセントの表示
			loadingText.text = (async.progress * 100).ToString("f0") + "%";

			//読み込み完了バーの表示
			loadingBar.value = async.progress;

			yield return new WaitForSeconds(0);
		}
		//読み込み終了パーセントの表示
		loadingText.text = (async.progress * 100).ToString("f0") + "%";

		//読み込み完了バーの表示
		loadingBar.value = async.progress;

		//0.5秒待つ
		yield return new WaitForSeconds(0.5f);

		//読み込み終了パーセントの表示
		loadingText.text = "100%";

		//読み込み完了バーの表示
		loadingBar.value = 1;

		//1秒かけてFadeInし、シーン移行を許可する
		fade.FadeIn(1f, () => async.allowSceneActivation = true);
	}
}