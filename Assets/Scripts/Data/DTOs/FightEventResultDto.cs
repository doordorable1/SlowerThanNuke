using System.Collections.Generic;


public class FightEventResultDto
{
    public List<string> fightedCrewKeys;
    public float damage;
    public string description;

    public FightEventResultDto()
    {
        fightedCrewKeys = new List<string>();
        damage = 0;
        description = string.Empty;
    }

    public FightEventResultDto(List<string> fightedCrewKeys, float damage, string description)
    {
        this.fightedCrewKeys = fightedCrewKeys;
        this.damage = damage;
        this.description = description;
    }
}
