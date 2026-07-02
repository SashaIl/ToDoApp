export interface Task {
    id: string;
    name: string;
    description: string;
    createdDate: string;
    category: string;
}



export interface CreateTaskDto {
    name: string;
    description: string;
    category: string;
}

export interface UpdateTaskDto {
    name: string;
    description: string;
    category: string;
}

export interface DeleteTaskDto {
    id: string;
}