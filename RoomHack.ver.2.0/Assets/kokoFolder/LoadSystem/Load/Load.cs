using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{

	private AsyncOperation async;

	public Text loadingText;
	public Slider loadingBar;

	public static int SL = 0;

	[SerializeField] Fade fade;

	void Start()
	{
		//�ǂݍ��ݏI���p�[�Z���g�̕\��
		loadingText.text = "0%".ToString();

		//�ǂݍ��݊����o�[�̕\��
		loadingBar.value = 0;

		//1�b������FadeOut���ASceneLoad��ǂݍ���
		fade.FadeOut(1f, () => StartCoroutine("SceneLoad"));
	}

	public IEnumerator SceneLoad()
	{
		//SL�œǂݍ��݂����V�[������ʂ���
		if (SL == 0)
		{
			//����Scene2��ǂݍ���
			async = SceneManager.LoadSceneAsync("Scene2");
		}

		//�ǂݍ��݂����V�[������������elseif���g���đ��₵�Ă���
		else if (SL == 1)
		{
			//async = SceneManager.LoadSceneAsync("");
		}

		//���[�h�������Ă��V�[���ڍs���Ȃ��悤�ɂ���
		async.allowSceneActivation = false;

		//�ǂݍ��ݒ��̏���
		while (async.progress < 0.9f)
		{
			//�ǂݍ��ݏI���p�[�Z���g�̕\��
			loadingText.text = (async.progress * 100).ToString("f0") + "%";

			//�ǂݍ��݊����o�[�̕\��
			loadingBar.value = async.progress;

			yield return new WaitForSeconds(0);
		}
		//�ǂݍ��ݏI���p�[�Z���g�̕\��
		loadingText.text = (async.progress * 100).ToString("f0") + "%";

		//�ǂݍ��݊����o�[�̕\��
		loadingBar.value = async.progress;

		//0.5�b�҂�
		yield return new WaitForSeconds(0.5f);

		//�ǂݍ��ݏI���p�[�Z���g�̕\��
		loadingText.text = "100%";

		//�ǂݍ��݊����o�[�̕\��
		loadingBar.value = 1;

		//1�b������FadeIn���A�V�[���ڍs��������
		fade.FadeIn(1f, () => async.allowSceneActivation = true);
	}
}