using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sans.UI.Menu
{
    public class GameplayMenu : Menu
    {
        [Header("UI References :")]
        [SerializeField] private TMP_Text _scoreText;
        public Button pauseBtn;
        public Button resumeBtn;
        public TextMeshProUGUI countDownText;
        private Coroutine countDown;
        private void OnEnable()
        {
            ScoreManager sm = GameObject.FindWithTag("GameController").GetComponent<ScoreManager>();
            if (sm) UpdateScore(sm.Score);
        }

        private void UpdateScore(int currentScore)
        {
            _scoreText.text = currentScore.ToString();
        }

        ////////////////////
        protected override void Awake()
        {
            base.Awake();
            ScoreManager.OnScoreAdded += UpdateScore;
        }

        private void OnDestroy()
        {
            ScoreManager.OnScoreAdded -= UpdateScore;
        }
        public void OnClickPause()
        {
            Time.timeScale = 0;
            pauseBtn.gameObject.SetActive(false);
            resumeBtn.gameObject.SetActive(true);
        }
        public void OnClickResume()
        {
            if(countDown != null)
            StopCoroutine(countDown);
            countDown = StartCoroutine(StartCountdown());
            pauseBtn.gameObject.SetActive(true);
            resumeBtn.gameObject.SetActive(false);
        }
        public IEnumerator StartCountdown()
        {
            float time = 0;
            countDownText.text = "3";
            while(time< 1)
            {
                countDownText.transform.localScale = Vector3.Lerp(Vector3.one * 0.5f, Vector3.one, time/1);
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            countDownText.text = "2";
            while(time< 2)
            {
                countDownText.transform.localScale = Vector3.Lerp(Vector3.one * 0.5f, Vector3.one, time/2);
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            countDownText.text = "1";
            while(time< 3)
            {
                countDownText.transform.localScale = Vector3.Lerp(Vector3.one * 0.5f, Vector3.one, time/3);
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            countDownText.text = "GO!";
            yield return new WaitForSecondsRealtime(0.25f);
            countDownText.text = "";
            Time.timeScale = 1;

        }
    }
}