export interface ToDo {
    id: number,
    activitiesNo: string;
    name: string;
    description?: string | null;
    dueDate?: Date | null;
    priority: number;
    status: number;
    createdBy?: string | null;
    createdAt: Date;
    updatedBy?: string | null;
    updatedAt?: Date | null;
    isDeleted: boolean;
}
