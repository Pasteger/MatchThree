using UnityEngine;

public class MeteoriteBehaviour : MonoBehaviour
{
    private bool _fall;
    
    private void Update()
    {
        _fall = !CheckExistingStone();
        
        if (_fall)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, ZPositionTypes.Meteorite);
        }

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    private bool CheckExistingStone()
    {
        var vectorCast = transform.position;
        vectorCast.z = ZPositionTypes.Stone;
        
        return Physics.Raycast(new Ray(vectorCast, Vector3.down), 1);
    }

    public bool IsFelled()
    {
        return _fall;
    }
}
