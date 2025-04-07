using UnityEngine;

public class Object_Move : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 왼쪽으로 이동하는 코드
        transform.position = new Vector3(transform.position.x - 5 * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
