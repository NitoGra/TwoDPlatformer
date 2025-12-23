namespace Scripts
{
    internal interface IDamageable
    {
        public void TakeDamage(int damage);
    }    
    
    internal interface IHealable
    {
        public bool TryHeal(int heal);
    }
}