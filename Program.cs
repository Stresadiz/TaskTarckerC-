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

bool activeApp = true;

string[] states = new string[] {
    "todo", "in-progress", "done" };

string[] commands = new string[] { 
    "add", "update", "delete", "list",
    "mark-in-progress", "mark-done" };

/* Main application loop */

while (activeApp) {
    ReadLineInput();

    activeApp = false;
}

string ReadLineInput() { 
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

            Console.WriteLine(msg);
        }
    }


    return input;

}

string ReturnList() { 
 return "";
}
string AddTask(string title) {
    if (true)
    {
        
    }
    return title;
}

string UpdateTask(int id, string newTitle) {
    return "";
}
string SetState() {
    return "";
}
