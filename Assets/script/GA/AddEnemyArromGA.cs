
public class AddEnemyStatusGA : GameAction
{
    public StatusEffectType type;
    public CombatantView me;
    public int Stacks;
    public AddEnemyStatusGA(CombatantView _caster,int _Statcks,StatusEffectType _type){
        me = _caster;
        Stacks = _Statcks;
        type = _type;
    }
}
