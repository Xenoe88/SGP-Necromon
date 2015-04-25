using UnityEngine;
using System.Collections;

public class Destructable : MonoBehaviour 
{
    public WorldPiece m_worldPiece;
    public int m_numPieces = 6;

    void Explode()
    {

        Destroy(gameObject);
        GameObject.FindGameObjectWithTag("MusicController").GetComponent<LoadSoundFX>().m_soundFXsources["EnviromentBreaking"].Play();

        Transform t = transform;

        for (int i = 0; i < m_numPieces; i++)
        {
            t.TransformPoint(0, -100, 0);
            WorldPiece temp = (WorldPiece)Instantiate(m_worldPiece, t.position, Quaternion.identity);
            temp.rigidbody2D.AddForce(Vector3.right * Random.Range(-50, 50));
            temp.rigidbody2D.AddForce(Vector3.up * Random.Range(100, 400));
        }
    }
}
