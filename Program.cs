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

bool activeApp = true;

string path = "C:\\Users\\Pc\\source\\repos\\TaskTracker\\tasks.json";

string[] states = new string[] {
    "todo", "in-progress", "done" };

string[] commands = new string[] { 
    "add", "list", "update", "delete", 
    "mark-in-progress", "mark-done" };

/* Main application loop */

while (activeApp) {
    ReadLineInput();
    ReadLineInput();
    ReadLineInput();

    activeApp = false;
}

void ReadLineInput() { 
    string input = Console.ReadLine();

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

            Console.WriteLine($"Task correctly added : {msg} - ID: {GetNextId()} ");
        }

        if (command == commands[1])
        {
            string msg = "Listing tasks: \n";
            string listResponse = "";
            try
            {
                if (input.Split(' ').Count() > 1)
                {
                    throw new ArgumentOutOfRangeException("Listing by state not implemented yet.");
                }
                else { 
                    listResponse = ReturnList();
                }
            }
            catch (ArgumentOutOfRangeException exc)
            {
                msg = exc.Message;
                return;

            } finally
            {
                Console.WriteLine(msg);

                if (listResponse != string.Empty)
                {
                    Console.WriteLine(listResponse);
                }
            }
        }
    }
}

string ReturnList() {

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
    else { 
        return "No tasks available.";
    }

    return response;
}

List <TaskTracker.Task> GetActualJson(){
    List<TaskTracker.Task> json = new List<TaskTracker.Task>();

    string readJsonString = File.ReadAllText(path);

    json = System.Text.Json.JsonSerializer.Deserialize<List<TaskTracker.Task>>(readJsonString);

    return json;
}

string AddTask(string title) {
    
    var cleanTitle = title.Trim().Trim('"');

    var newTask = new TaskTracker.Task
    {
        Id = GetNextId(),
        Title = cleanTitle,
        Status = "todo"
    };

    var Tasks = GetActualJson();

    Tasks.Add(newTask);

    string json = System.Text.Json.JsonSerializer.Serialize(Tasks);

    File.WriteAllText(path, json);

    return cleanTitle;
}

string UpdateTask(int id, string newTitle) {
    return "";
}
string SetState() {
    return "";
}

int GetNextId() {

    int response = 0;

    string readJsonString = File.ReadAllText(path);

    if (readJsonString.Length > 0)
    {
        var deserializeObjs = System.Text.Json.JsonSerializer.Deserialize<List<TaskTracker.Task>>(readJsonString);

        if (deserializeObjs != null)
        {
            foreach (var item in deserializeObjs)
            {
                response += 1;
            }
        }
        else
        {
            return 0;
        }

    }

    return response;
}
