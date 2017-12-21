using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingFloor : MonoBehaviour {

    private ParticleSystem particleSystem;
    [SerializeField] GameObject floorElementPrefab;
    private int particleAmount;
    private Vector3 floorSize;
    private float widthQuad;
    private ParticleSystem.Particle[] ParticleList;
    private float offset;
    private float rowCount;

    void particleComposition()
    {
        ParticleList = new ParticleSystem.Particle[particleSystem.particleCount];
        particleSystem.GetParticles(ParticleList);
        var row = -widthQuad/2;
        var col = -widthQuad / 2;
        var loop = 0;
        for (int z = 0; z < particleAmount; z++)
        {
            ParticleList[z].position = new Vector3(row, col,0);
            col += offset;
            ++loop;
            if(col==widthQuad+offset)
            {
                col = -widthQuad / 2;
                row += offset;
                Debug.Log(row);
            }
        }
        particleSystem.SetParticles(ParticleList, particleAmount);
    }


	void Start () {
        particleSystem = this.GetComponent<ParticleSystem>();
        particleSystem.Emit(100000);
        particleAmount = particleSystem.particleCount;
        floorSize = particleSystem.shape.box;
        widthQuad = floorSize.x;
        offset = widthQuad*10 / particleAmount;
        rowCount = Mathf.Sqrt(particleAmount);
        particleComposition();
	}
	


	void Update () {
		
	}
}
