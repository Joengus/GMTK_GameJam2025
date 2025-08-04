using Godot;

using System.Collections.Generic;
[GlobalClass]
public partial class TaskLibraryResource : Resource
{
    [Export] public TaskResource[] dailyTaskList;
    [Export] public TaskResource[] hiddenTaskList;


    public void resetDailyTaskCompletions()
    {
        foreach (TaskResource task in dailyTaskList)
        {
            task.IsComplete = false;
        }
    }

    public List<TaskResource> GetRandomIncompleteTasks(int numToRetrieve)
    {
        List<TaskResource> toDoList = new List<TaskResource>();
        var rng = new RandomNumberGenerator();
        for (int i = 0; i < numToRetrieve; i++)
        {
            rng.Randomize();
            int randomNumber = rng.RandiRange(0, dailyTaskList.Length - 1);
            if (!toDoList.Contains(dailyTaskList[randomNumber]))
            {
                toDoList.Add(dailyTaskList[randomNumber]);
            }
            else
            {
                i--;
            }
        }
        return toDoList;
    }
}
