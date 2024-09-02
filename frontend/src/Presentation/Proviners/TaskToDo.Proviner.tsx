import React, { createContext, useContext, ReactNode } from 'react';
import TaskToDoUseCase from "../../Application/UseCases/TaskToDo.UseCase";
import TaskToDoAdapter from "../../Infra/Adapters/TaskToDo/TaskToDo.Adapter";

const TaskToDoContext = createContext<TaskToDoUseCase | undefined>(undefined);

export const TaskToDoProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const jwtAdapter = new TaskToDoAdapter();
    const userUseCase = new TaskToDoUseCase(jwtAdapter);

    return (
        <TaskToDoContext.Provider value={userUseCase}>
            {children}
        </TaskToDoContext.Provider>
    );
};

export const useTaskToDo = () => {
    const context = useContext(TaskToDoContext);
    if (context === undefined) {
        throw new Error('');
    }
    return context;
};
