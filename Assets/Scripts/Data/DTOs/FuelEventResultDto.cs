public class FuelEventResultDto
{
    // DTO는 데이터 운반객체이기 때문에, 게터세터 없이 public변수로 운용합니다.
    public int fuel;
    public string description;

    public FuelEventResultDto()
    {
        fuel = 0;
        description = string.Empty;
    }

    public FuelEventResultDto(int fuel, string description)
    {
        this.fuel = fuel;
        this.description = description;
    }
}
