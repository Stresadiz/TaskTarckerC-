# ✅ Task Tracker CLI

Una aplicación de línea de comandos para gestionar tus tareas de forma rápida y eficiente.

---

## 🚀 Descripción

**Task Tracker** es una herramienta CLI que permite crear, actualizar, eliminar y listar tareas, así como marcar su estado. Ideal para mantener tus pendientes organizados desde la terminal.

---

## 🛠️ Comandos disponibles

### 📝 Agregar una nueva tarea

- `task-cli add "Buy groceries"`
- `Task added successfully (ID: 1)`

### 📝 Actualizar o borrar una tarea
- `task-cli update 1 "Buy groceries and cook dinner"`
- `task-cli delete 1`

### 📝 Marcar una tarea en progreso o completada
- `task-cli mark-in-progress 1`
- `task-cli mark-done 1`

### 📝 Obtener listado completo
- `task-cli list`

### 📝 Obtener listado por estado
- `task-cli list done`
- `task-cli list todo`
- `task-cli list in-progress`