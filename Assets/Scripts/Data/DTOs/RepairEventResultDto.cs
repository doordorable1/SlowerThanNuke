public class RepairEventResultDto
{
    public float repairAmount;
    public string description;

    public RepairEventResultDto()
    {
        repairAmount = 0;
        description = string.Empty;
    }

    public RepairEventResultDto(float repairAmount, string description)
    {
        this.repairAmount = repairAmount;
        this.description = description;
    }
}
