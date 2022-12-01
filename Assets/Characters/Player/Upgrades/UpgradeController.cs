
using UnityEngine;


namespace Assets.Player.Upgrades
{
    [CreateAssetMenu(fileName = nameof(UpgradeController), menuName = "ScriptableObjects/UpgradeController")]
    public class UpgradeController : ScriptableObject
    {
        [SerializeField] private MeleeUpgradeEvent meleeUpgradeEvent;
        [SerializeField] private RangedUpgradeEvent rangedUpgradeEvent;
        [SerializeField] private int startingMeleeDamage;
        [SerializeField] private int startingRangedDamage;
        public int meleeDamage;
        public int rangedDamage;

        public void NewGame()
        {
            meleeDamage = startingMeleeDamage;
            rangedDamage = startingRangedDamage;
        }
        public void ChangeMeleeDamage(int changeAmount)
        {
            meleeDamage = meleeDamage + changeAmount;
            meleeUpgradeEvent.Raise(this, null);
        }
        public void ChangeRangedDamage(int changeAmount)
        {
            rangedDamage = rangedDamage + changeAmount;
            rangedUpgradeEvent.Raise(this, null);
        }
    }
}
