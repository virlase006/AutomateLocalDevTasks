namespace MLocalRun
{
    public interface IScriptExecutor
    {
        int ExecuteScript(string command);
        int GetResult();
    }
}