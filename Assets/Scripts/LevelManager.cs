using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class LevelManager : MonoBehaviour
    {
        public void LoadMenu()
        {
            SceneManager.LoadScene ("Menu");
        }
        
        public void LoadLevel1()
        {
            SceneManager.LoadScene ("TestScene");
        }
        
        public void LoadLevel2()
        {
            SceneManager.LoadScene ("TestScene2");
        }
        
        public void LoadLevel3()
        {
            
        }
    }
}