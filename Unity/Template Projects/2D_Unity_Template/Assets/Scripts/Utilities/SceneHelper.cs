using UnityEngine.SceneManagement;

public static class SceneHelper {
    // ----------------------------------------------------------------
    //  Provided by Brett Taylor to automatically reload scene on save when in Play Mode
    //  (https://drive.google.com/drive/folders/140fGiWSgVx6scMnMLtKYbEGQXG-0cqJU)
    // ----------------------------------------------------------------
    static public void ReloadScene() { OpenScene(SceneManager.GetActiveScene().name); }
    static public void OpenScene(string sceneName) { SceneManager.LoadScene(sceneName); }
}