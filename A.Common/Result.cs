namespace A.Common;

public class Result
{
    public bool IsSuccess { get; set; }
    public List<string> Errors { get; set; }
    public Result(List<string> errors)
    {
        IsSuccess = false;
        Errors = errors;
    }
    public Result()
    {
        IsSuccess=true;
        Errors = [];
    }


}
