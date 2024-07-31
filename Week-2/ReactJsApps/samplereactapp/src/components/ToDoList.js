import React, { useState } from 'react';

const ToDoList = () => {
    const [tasks, setTasks] = useState([]);
    const [taskInput, setTaskInput] = useState('');

    const addTask = () => {
        const trimmedTaskInput = taskInput.trim();

        if (trimmedTaskInput === '' || tasks.some(task => task.text === trimmedTaskInput)) {
            return;
        }

        setTasks([...tasks, { text: trimmedTaskInput, completed: false }]);
        setTaskInput('');
    };

    const deleteTask = (index) => {
        setTasks(tasks.filter((_, i) => i !== index));
    };

    const toggleTaskCompletion = (index) => {
        setTasks(
            tasks.map((task, i) =>
                i === index ? { ...task, completed: !task.completed } : task
            )
        );
    };

    return (
        <div>
            <h1>To-Do List</h1>
            <input
                type="text"
                value={taskInput}
                onChange={(e) => setTaskInput(e.target.value)}
            />
            <button onClick={addTask}>Add Task</button>
            <ul id="taskList" style={{ listStyleType: 'disc' }}>
                {tasks.map((task, index) => (
                    <li key={index} style={{ display: 'flex', alignItems: 'center' }}>
                        <span style={{ textDecoration: task.completed ? 'line-through' : 'none', flexGrow: 1 }}>
                            {task.text}
                        </span>
                        <button
                            onClick={() => toggleTaskCompletion(index)}
                            style={{ marginLeft: '10px' }}
                        >
                            {task.completed ? 'Not Completed' : 'Complete'}
                        </button>
                        <button
                            onClick={() => deleteTask(index)}
                            style={{ marginLeft: '10px' }}
                        >
                            Delete
                        </button>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default ToDoList;
