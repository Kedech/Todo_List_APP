import { Routes } from '@angular/router';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { LoginComponent } from './components/pages/login/login.component';
import { RegisterComponent } from './components/pages/register/register.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { DashboardComponent } from './components/pages/dashboard/dashboard.component';
import { TaskComponent } from './components/pages/task/task.component';

export const routes: Routes = [
    {
        path: '', 
        component: AdminLayoutComponent,
        children: [
            {path: '', redirectTo: 'dashboard', pathMatch: 'full'},
            {path: 'dashboard', component: DashboardComponent, pathMatch: 'full'},
            {path: 'task', component: TaskComponent, pathMatch: 'full'},
        ]
    },
    {
        path: 'auth', 
        component: AuthLayoutComponent, 
        children:[
            {path: 'login', component: LoginComponent},
            {path: 'register', component: RegisterComponent}
        ]
    },
];
