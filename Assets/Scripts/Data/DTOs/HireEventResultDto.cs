public class HireEventResultDto
{
    public string crewKey;
    public string description;

    public HireEventResultDto()
    {
        crewKey = string.Empty;
        description = string.Empty;
    }

    public HireEventResultDto(string key, string description)
    {
        this.crewKey = key;
        this.description = description;
    }
}
