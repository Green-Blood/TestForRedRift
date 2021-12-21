using Hand;
using UnityEngine;

public class ChangeValueButton : MonoBehaviour
{
    [SerializeField] private DragZone dragZone;
    [SerializeField] private int minChangeValue;
    [SerializeField] private int maxChangeValue;
    [SerializeField] private float changeDelay;

    public void Change()
    {
        var changeValue = Random.Range(minChangeValue, maxChangeValue + 1);

        for (var index = 0; index < dragZone.CardsInZone.Count; index++)
        {
            var card = dragZone.CardsInZone[index];
            card.ChangeValueRandomly(changeValue, index * changeDelay);
        }
    }
}