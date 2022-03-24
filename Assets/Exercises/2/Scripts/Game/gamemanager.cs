using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class gamemanager : MonoBehaviour
    {
        [Header("Audio")]
        public AudioClip audioClip;
        [SerializeField] private AudioSource _audioSource;

        [Header("Player")]
        [SerializeField] private float playerLife;

        private bool _isOnGround;

        public void ReproducirAudio()
        {
            _audioSource.PlayOneShot(audioClip);
        }

        public void OnReceiveDamage(float damage)
        {
            playerLife -= damage;

            if (playerLife <= 0)
            {
                //TODO Death
                Debug.Log("The player is Death");
            }
        }

        private bool _isOnPause;

        public void Pause(bool state)
        {
            _isOnPause = state;
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void Jump()
        {
            if (_isOnGround)
            {
                //TODO Jump
            }
        }
    }
}