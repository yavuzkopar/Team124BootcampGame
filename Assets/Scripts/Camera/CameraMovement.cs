using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
   [SerializeField] LayerMask yukseklikLayer;
   [SerializeField] float yukseklik;
   [SerializeField] float yukselmeHizi;
   [SerializeField] float rotateSpeed;
    void LateUpdate()
    {
        bool isHitted = Physics.Raycast(transform.position,Vector3.down,out RaycastHit hit,Mathf.Infinity,yukseklikLayer);

        Vector3 t = transform.position - hit.point;
        
        if(!isHitted)
        {
             transform.Translate(Vector3.up * Time.deltaTime * yukselmeHizi * 5);
        }

        if (t.y < yukseklik - 1f )
        {
            transform.Translate(Vector3.up * Time.deltaTime * yukselmeHizi);
        }
        else if (t.y > yukseklik + 1f)
        {
            transform.Translate(Vector3.down * Time.deltaTime * yukselmeHizi);
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -100, 100), transform.position.y, Mathf.Clamp(transform.position.z, -100, 100));

        transform.Translate(new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")));
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up,rotateSpeed * Time.deltaTime);
        }
         if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up,-rotateSpeed * Time.deltaTime);
        }
    }
}
