using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections; // Necesario para usar Corrutinas

public class VideoSceneLoader : MonoBehaviour
{
    [Header("Referencias")]
    public VideoPlayer miVideoPlayer;

    [Header("Configuración")]
    public string escenaACargar;

    private bool yaEstaCargando = false;

    void Start()
    {
        if (miVideoPlayer == null) miVideoPlayer = GetComponent<VideoPlayer>();

        miVideoPlayer.playOnAwake = false;
        miVideoPlayer.prepareCompleted += IniciarVideo;
        miVideoPlayer.loopPointReached += AlTerminarVideo;

        miVideoPlayer.Prepare();
    }

    void IniciarVideo(VideoPlayer vp)
    {
        // En vez de darle al play directo, llamamos a la corrutina
        StartCoroutine(BufferYPlay(vp));
    }

    IEnumerator BufferYPlay(VideoPlayer vp)
    {
        // 1. Le damos al play e INMEDIATAMENTE pausamos
        vp.Play();
        vp.Pause();

        // 2. Esperamos exactamente 2 fotogramas del juego. 
        // Durante este tiempo invisible, Unity tiene tiempo de sobra de cargar la imagen.
        yield return null;
        yield return null;

        // 3. Con la imagen ya lista y renderizada en pantalla, arrancamos
        vp.Play();
    }

    void Update()
    {
        if (miVideoPlayer.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
            {
                CargarSiguienteEscena();
            }
        }
    }

    void AlTerminarVideo(VideoPlayer vp)
    {
        CargarSiguienteEscena();
    }

    void CargarSiguienteEscena()
    {
        if (yaEstaCargando) return;
        yaEstaCargando = true;
        SceneManager.LoadScene(escenaACargar);
    }

    void OnDestroy()
    {
        if (miVideoPlayer != null)
        {
            miVideoPlayer.prepareCompleted -= IniciarVideo;
            miVideoPlayer.loopPointReached -= AlTerminarVideo;
        }
    }
}