using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
   [SerializeField] LayerMask yukseklikLayer;
   [SerializeField] float yukseklik;
   [SerializeField] float yukselmeHizi;
    void LateUpdate()
    {
        bool isHitted = Physics.Raycast(transform.position,Vector3.down,out RaycastHit hit,Mathf.Infinity,yukseklikLayer);

        Vector3 t = transform.position - hit.point;
        
        

        if (t.y < yukseklik - 1f)
        {
            transform.Translate(Vector3.up * Time.deltaTime * yukselmeHizi);
        }
        else if (t.y > yukseklik + 1f)
        {
            transform.Translate(Vector3.down * Time.deltaTime * yukselmeHizi);
        }

        transform.Translate(new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")));
    }
}
