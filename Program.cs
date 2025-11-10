//# Adding a new task
//task - cli add "Buy groceries"
//# Output: Task added successfully (ID: 1)

//# Updating and deleting tasks
//task-cli update 1 "Buy groceries and cook dinner"
//task-cli delete 1

//# Marking a task as in progress or done
//task-cli mark-in-progress 1
//task-cli mark-done 1

//# Listing all tasks
//task-cli list

//# Listing tasks by status
//task-cli list done
//task-cli list todo
//task-cli list in-progress

using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

bool activeApp = true;

string path = "C:\\Users\\Pc\\source\\repos\\TaskTracker\\tasks.json";

string[] states = new string[] {
    "todo", "in-progress", "done" };

string[] commands = new string[] { 
    "add", "list", "update", "delete", "close", 
    "mark-in-progress", "mark-done" };

/* Main application loop */

while (activeApp) {
    ReadLineInput();
}

void ReadLineInput()
{
    string? input = Console.ReadLine();

    if (input != null)
    {
        input = input.Trim();
        string command = input.Split(' ')[0].ToLower();

        if (commands.Contains(command))
        {
            if (command == commands[0])
            {
                string title = input.Substring(command.Length).Trim();
                string msg = "";

                try
                {
                    if (string.IsNullOrWhiteSpace(title))
                    {
                        throw new ArgumentNullException(nameof(title), "Task title cannot be empty.");
                    }
                    else if (!title.StartsWith('"') && !title.EndsWith('"'))
                    {
                        throw new ArgumentNullException(nameof(title), "Task title cannot start or ends without quotation marks");
                    }
                    else
                    {
                        msg = AddTask(title);
                    }
                }
                catch (ArgumentNullException argEx)
                {
                    msg = argEx.Message;
                }

                Console.WriteLine($"Task correctly added : {msg}");
            }

            if (command == commands[1])
            {
                string msg = "Listing tasks:";
                string listResponse = "";
                try
                {
                    if (input.Split(' ').Count() > 1)
                    {
                        throw new ArgumentOutOfRangeException("Listing by state not implemented yet.");
                    }
                    else
                    {
                        listResponse = ReturnList();
                    }
                }
                catch (ArgumentOutOfRangeException exc)
                {
                    msg = exc.Message;
                    return;

                }
                finally
                {
                    Console.WriteLine(msg);

                    if (listResponse != string.Empty)
                    {
                        Console.WriteLine(listResponse);
                    }
                }
            }

            if (command == commands[2])
            {
                Console.WriteLine("Update task not implemented yet.");
            }

            if (command == commands[3])
            {
                string msg = "";

                List<string> args = new List<string>();

                Array.ForEach(input.Split(' '), element => args.Add(element));

                try
                {
                    if (args.Count > 1)
                    {
                        string secondArg = args[1];

                        if (args.Count > 2)
                        {
                            throw new ArgumentOutOfRangeException("Too many arguments provided for deletion.");
                        }
                        else {
                            if (int.TryParse(secondArg, out int res))
                            {
                                int taskId = res;

                                msg = DeleteTask(taskId);
                            }
                            else { 
                                throw new ArgumentException("Task ID must be a valid integer.");
                            }
                        }
                    }
                    else { 
                        throw new ArgumentNullException("Task ID is required for deletion.");
                    }
                }
                catch (Exception exc)
                {
                    msg = exc.Message;

                }

                Console.WriteLine(msg);
            }

            if (command == commands[4])
            {
                Console.WriteLine("Closing app...");
                activeApp = false;
            }

            if (command == commands[5]) {

                string msg = "";

                try
                {
                    if ((int.TryParse(input.Split(' ')[1], out int id)))
                    {
                        msg = MarkTask(id, states[1]);
                    }
                    else
                    {
                        throw new ArgumentException("Task ID must be a valid integer.");
                    }
                }
                catch (Exception exc)
                {
                    msg = exc.Message;
                }

                Console.WriteLine(msg);
            }

            if (command == commands[6])
            {
                string msg = "";

                try
                {
                    if ((int.TryParse(input.Split(' ')[1], out int id)))
                    {
                        msg = MarkTask(id, states[2]);
                    }
                    else
                    {
                        throw new ArgumentException("Task ID must be a valid integer.");
                    }
                }
                catch (Exception exc)
                {
                    msg = exc.Message;
                }

                Console.WriteLine(msg);
            }
        }
        else
        {
            Console.WriteLine("Comando no existente");
        }
    }

    string ReturnList()
    {

        string response = string.Empty;

        string readJsonString = File.ReadAllText(path);

        List<TaskTracker.Task> deserializeObjs = GetActualJson();

        if (deserializeObjs != null)
        {
            foreach (var item in deserializeObjs)
            {
                response += $" - {item.Title} - {item.Id} \n";
            }
        }
        else
        {
            return "No tasks available.";
        }

        return response;
    }

    List<TaskTracker.Task> GetActualJson()
    {
        List<TaskTracker.Task>? json = new List<TaskTracker.Task>();

        string readJsonString = File.ReadAllText(path);

        json = System.Text.Json.JsonSerializer.Deserialize<List<TaskTracker.Task>>(readJsonString);

        return json ?? new List<TaskTracker.Task>();
    }

    string AddTask(string title)
    {

        var cleanTitle = title.Trim().Trim('"');

        var newTask = new TaskTracker.Task
        {
            Id = GetNextId(),
            Title = cleanTitle,
            Status = "todo"
        };

        var Tasks = GetActualJson();

        AddTaskToJson(Tasks, newTask);

        return $"{newTask.Title} - {newTask.Id}";
    }

    void AddTaskToJson(List<TaskTracker.Task> Tasks, TaskTracker.Task task)
    {
        Tasks.Add(task);
        string json = System.Text.Json.JsonSerializer.Serialize(Tasks);
        File.WriteAllText(path, json);
    }

    string UpdateTask(int id, string newTitle)
    {
        return "";
    }
    
    string SetState()
    {
        return "";
    }

    string DeleteTask(int id)
    {

        var Tasks = GetActualJson();

        var taskToRemove = Tasks.Find(t => t.Id == id);

        if (taskToRemove != null) { 
            Tasks.Remove(taskToRemove);
        }

        string json = System.Text.Json.JsonSerializer.Serialize(Tasks);

        File.WriteAllText(path, json);

        return $"Se elimina Task: {id}";
    }

    string MarkTask(int id, string state)
    {
        string msg = string.Empty;

        var Tasks = GetActualJson();

        var TaskToUpdate = Tasks.Find(t => t.Id == id);

        if (TaskToUpdate != null)
        {
            var TaskAux = Tasks.Find(t => t.Id == id);
            
            TaskToUpdate.Status = state;

            Tasks.Remove(TaskAux);

            AddTaskToJson(Tasks, TaskToUpdate);

            msg = $"Task {id} marked as {state}";
        }
        else
        {
            msg = $"Task with ID {id} not found.";
        }

        return msg;
    }

    int GetNextId()
    {

        int response = 0;

        string readJsonString = File.ReadAllText(path);

        if (readJsonString.Length > 0)
        {
            var deserializeObjs = System.Text.Json.JsonSerializer.Deserialize<List<TaskTracker.Task>>(readJsonString);

            if (deserializeObjs != null)
            {
                var id = deserializeObjs.Max(t => t.Id);
                response = id + 1;
            }
            else
            {
                return 0;
            }

        }

        return response;
    }
}
