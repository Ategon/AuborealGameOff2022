using System.Collections;
using System.Collections.Generic;
using Assets.EventSystem;
using Assets.Player.Thirst;
using UnityEngine;


namespace Assets.Player.Thirst
{
    [CreateAssetMenu(fileName = nameof(ThirstController), menuName = "ScriptableObjects/ThirstController")]
    public class ThirstController : ScriptableObject
    {
        public int currentThirst;
        public int maxThirst;
        [SerializeField] private int startingThirst;
        [SerializeField] private ThirstChangedEvent thirstChangedEvent;
        
        public void ChangeThirst(int changeAmount)
        {
            if (changeAmount < 0)
                currentThirst = Mathf.Max(0, currentThirst + changeAmount);
            if (changeAmount > 0)
                currentThirst = Mathf.Min(maxThirst, currentThirst + changeAmount);
            ThirstChangedEventParameters eventParameters = new ThirstChangedEventParameters(currentThirst, maxThirst);
            thirstChangedEvent.Raise(this, eventParameters);
        }

        public void NewGame()
        {
            currentThirst = startingThirst;
            ThirstChangedEventParameters eventParameters = new ThirstChangedEventParameters(currentThirst, maxThirst);
            thirstChangedEvent.Raise(this, eventParameters);
        }
    }
}
