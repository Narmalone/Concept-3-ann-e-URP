using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Nouvel Event", order = 0)]
public class Event : ScriptableObject
{
    public delegate void VectorDelegate(Vector3 vectorPosition);

    public delegate void DoSomethingDelegate();

    public event VectorDelegate m_vectorEvent;

    public event DoSomethingDelegate m_somethingEvent;

   public void VectorEvent(Vector3 vectorPosition)
    {
        m_vectorEvent?.Invoke(vectorPosition);
    }

    public void DoSomething()
    {
        m_somethingEvent?.Invoke();
    }
}
