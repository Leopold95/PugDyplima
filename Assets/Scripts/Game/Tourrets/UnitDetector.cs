using UnityEngine;

public class UnitDetector : MonoBehaviour
{
    [SerializeField] string _unitTag;

    private void OnTriggerEnter(Collider other)
    {
        //print(other.tag);
        if (other.CompareTag(_unitTag))
            transform.parent.SendMessage("OnUnitDetected", other.gameObject, SendMessageOptions.DontRequireReceiver);
    }
}
