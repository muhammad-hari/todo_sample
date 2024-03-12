import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/demo/api/product';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { ProductService } from 'src/app/demo/service/product.service';
import { Priority } from './models/priority.model';
import { TaskStatus } from './models/status.model';
import { ToDoService } from 'src/app/demo/service/todo.service';
import { ToDo } from 'src/app/demo/api/todo';

@Component({
    templateUrl: './crud.component.html',
    providers: [MessageService, ToDoService]
})
export class CrudComponent implements OnInit {

    productDialog: boolean = false;

    deleteProductDialog: boolean = false;

    deleteProductsDialog: boolean = false;

    submitted: boolean = false;

    cols: any[] = [];

    statuses: any[] = [];

    rowsPerPageOptions = [5, 10, 20];


    prioritiesList: Priority[] = [];
    statusList: TaskStatus[] = [];

    selectedPriority: number;
    selectedStatus: number;
    duedate: any;

    tasks: ToDo[] = [];
    selectedTasks: ToDo[] = [];
    task: ToDo = {
        id: 0,
        activitiesNo: '',
        name: '',
        priority: 0,
        status: 0,
        createdAt: undefined,
        isDeleted: false
    };


    constructor(private todoService: ToDoService, private productService: ProductService, private messageService: MessageService) { }

    ngOnInit() {
       
        this.onLoadData();

        this.cols = [
            { field: 'activitiesNo', header: 'Activities No' },
            { field: 'name', header: 'Name' },
            { field: 'description', header: 'Description' },
            { field: 'dueDate', header: 'Due Date' },
            { field: 'priority', header: 'priority' },
            { field: 'status', header: 'status' }
        ];

        this.prioritiesList = [
            { label: 'None', value: 0 },
            { label: 'Critical', value: 1 },
            { label: 'High', value: 2 },
            { label: 'Medium', value: 3 },
            { label: 'Low', value: 4 },
        ];

        this.statusList = [
            { label: 'ToDo', value: 0 },
            { label: 'InProgress', value: 1 },
            { label: 'Pending', value: 2 },
            { label: 'OnHold', value: 3 },
            { label: 'Completed', value: 4 },
        ];
    }

    onMapStatus(statusId: number){
        return this.statusList.find(x => x.value == statusId).label;
    }

    onMapPriority(priorityId: number){
        return this.prioritiesList.find(x => x.value == priorityId).label;
    }

    onLoadData(){
        this.todoService.getAllTasks().then(data => {
            this.tasks = data;
        });
    }

    openNew() {
        this.task = {
            id: 0,
            activitiesNo: '',
            name: '',
            priority: 0,
            status: 0,
            createdAt: undefined,
            isDeleted: false
        };
        this.submitted = false;
        this.productDialog = true;
    }

    deleteSelectedProducts() {
        this.deleteProductsDialog = true;
    }

    editProduct(task: ToDo) {
        this.task = { ...task };
        this.selectedPriority = task.priority;
        this.selectedStatus = task.status;
        this.productDialog = true;
    }

    deleteProduct(task: ToDo) {
        this.deleteProductDialog = true;
        this.task = { ...task };
    }

    confirmDeleteSelected() {
        this.deleteProductsDialog = false;
        this.tasks = this.tasks.filter(val => !this.selectedTasks.includes(val));
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Task Deleted', life: 3000 });
        this.selectedTasks = [];
    }

    confirmDelete() {
        this.deleteProductDialog = false;
        this.tasks = this.tasks.filter(val => val.id !== this.task.id);
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Tasks Deleted', life: 3000 });
        this.task = {
            id: 0,
            activitiesNo: '',
            name: '',
            priority: 0,
            status: 0,
            createdAt: undefined,
            isDeleted: false
        };
    }

    hideDialog() {
        this.productDialog = false;
        this.submitted = false;
    }

    saveProduct() {
        this.submitted = true;
        console.log('tasks', this.task);

        if(this.task){
            // sample payload

            const taskPriorty = this.task.priority as any;
            const taskStatus = this.task.status as any;

            const payload = {
                "activitiesNo": this.task.activitiesNo,
                "name": this.task.name,
                "description":this.task.description,
                "dueDate": new Date(this.task.dueDate),
                "priority": taskPriorty.value,
                "status": taskStatus.value,
                "createdBy": "system", // current user
                "createdAt": new Date(),
                "updatedBy": null,
                "updatedAt": null,
                "isDeleted": false
              };


            this.todoService.createTask(payload).then(res => {
                this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Task Created', life: 3000 });
            }).catch(err => {
                this.messageService.add({ severity: 'failed', summary: 'Failed', detail: 'Task Failed to create', life: 3000 });

            })

            this.onLoadData();
            this.tasks = [...this.tasks];
            this.productDialog = false;
            this.task = {
                id: 0,
                activitiesNo: '',
                name: '',
                priority: 0,
                status: 0,
                createdAt: undefined,
                isDeleted: false
            };
        }
    }

    findIndexById(id: number): number {
        let index = -1;
        for (let i = 0; i < this.tasks.length; i++) {
            if (this.tasks[i].id === id) {
                index = i;
                break;
            }
        }

        return index;
    }

    createId(): string {
        let id = '';
        const chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
        for (let i = 0; i < 5; i++) {
            id += chars.charAt(Math.floor(Math.random() * chars.length));
        }
        return id;
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }
}
